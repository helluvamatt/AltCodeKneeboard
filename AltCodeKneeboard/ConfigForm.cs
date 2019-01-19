using AltCodeKneeboard.Controls;
using AltCodeKneeboard.Hotkeys;
using AltCodeKneeboard.Kneeboard;
using AltCodeKneeboard.Models;
using AltCodeKneeboard.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using R = AltCodeKneeboard.Properties.Resources;
using Settings = AltCodeKneeboard.Properties.Settings;

namespace AltCodeKneeboard
{
    internal partial class ConfigForm : Form
    {
        private readonly HotkeyManager _HotkeyManager;
        private readonly KneeboardManager _KneeboardManager;
        private readonly AltCodeData _AltCodeData;
        private readonly PlacementViewModel _PlacementViewModel;
        private readonly AppearanceViewModel _AppearanceViewModel;

        private readonly AppearancePreviewControl appearancePreview;

        private readonly List<Favorite> _Favorites;

        public ConfigForm(HotkeyManager hotkeyManager, KneeboardManager kneeboardManager, AltCodeData altCodeData)
        {
            InitializeComponent();

            _HotkeyManager = hotkeyManager;
            _KneeboardManager = kneeboardManager;
            _AltCodeData = altCodeData;
            _PlacementViewModel = new PlacementViewModel();

            // Tab pages
            tabPageEntryBindingSource.DataSource = new List<TabPageEntry>()
            {
                new TabPageEntry(R.keyboard, R.Hotkeys),
                new TabPageEntry(R.star, R.Favorites),
                new TabPageEntry(R.palette, R.Appearance),
                new TabPageEntry(R.placement, R.Dimensions)
            };

            // Hotkeys
            hotkeyListPanel.Controls.Clear();
            foreach (var kvp in _HotkeyManager.GetHooks())
            {
                var item = new HotkeyListItem(kvp.Key, kvp.Value);
                item.KeyComboEditingChanged += (sender, args) => _HotkeyManager.Enabled = !item.KeyComboEditing && Settings.Default.HotkeysEnabled;
                hotkeyListPanel.Controls.Add(item);
            }

            // Favorites
            _Favorites = new List<Favorite>();
            foreach (var code in _AltCodeData.AltCodes.AltCodes)
            {
                var f = new Favorite(code);
                f.PropertyChanged += Favorite_PropertyChanged;
                _Favorites.Add(f);
            }
            gridFavorites.RowCount = _Favorites.Count;

            // Favorites: Visible Groups
            var groups = new List<GroupVisible>();
            var hiddenGroups = Settings.Default.HiddenGroups ?? new int[0];
            foreach (var group in _AltCodeData.Groups.Groups)
            {
                var gv = new GroupVisible(group, !hiddenGroups.Contains(group.ID));
                gv.PropertyChanged += GroupVisible_PropertyChanged;
                groups.Add(gv);
            }
            groupVisibleBindingSource.DataSource = groups;

            cmbStyleSort.DataSource = FormUtils.GetEnumList(typeof(SortMode));
            cmbStyleSort.DataBindings.Add(nameof(ComboBox.SelectedValue), Settings.Default, nameof(Settings.SortMode), false, DataSourceUpdateMode.OnPropertyChanged);

            // Appearance
            // TODO Allow theme selection from XML/other serialization
            _AppearanceViewModel = new AppearanceViewModel();
            propertyGridTheme.SelectedObject = _KneeboardManager.Theme;
            propertyGridTheme.DataBindings.Add(nameof(PropertyGrid.PropertySort), _AppearanceViewModel, nameof(AppearanceViewModel.SortMode), false, DataSourceUpdateMode.OnPropertyChanged);
            toolStripButtonAppearanceThemeSortAlpha.DataBindings.Add(nameof(ToolStripButton.Checked), _AppearanceViewModel, nameof(AppearanceViewModel.SortAlpha), false, DataSourceUpdateMode.OnPropertyChanged);
            toolStripButtonAppearanceThemeSortByCategory.DataBindings.Add(nameof(ToolStripButton.Checked), _AppearanceViewModel, nameof(AppearanceViewModel.SortCategory), false, DataSourceUpdateMode.OnPropertyChanged);
            appearancePreview = new AppearancePreviewControl(new KneeboardLayout(_AltCodeData, Settings.Default.Favorites, Settings.Default.HiddenGroups), _KneeboardManager.Theme);
            appearancePreview.BackgroundImage = R.TransparencyBackground;
            appearancePreview.BackgroundImageLayout = ImageLayout.Tile;
            appearancePreview.Dock = DockStyle.Fill;
            tableLayoutPanelAppearance.Controls.Add(appearancePreview, 1, 0);
            tableLayoutPanelAppearance.SetRowSpan(appearancePreview, 2);

            // Placement
            numPlacementX.DataBindings.Add(nameof(NumericUpDown.Value), _PlacementViewModel, nameof(PlacementViewModel.X), false, DataSourceUpdateMode.OnPropertyChanged);
            numPlacementY.DataBindings.Add(nameof(NumericUpDown.Value), _PlacementViewModel, nameof(PlacementViewModel.Y), false, DataSourceUpdateMode.OnPropertyChanged);
            numPlacementWidth.DataBindings.Add(nameof(NumericUpDown.Value), _PlacementViewModel, nameof(PlacementViewModel.Width), false, DataSourceUpdateMode.OnPropertyChanged);
            numPlacementHeight.DataBindings.Add(nameof(NumericUpDown.Value), _PlacementViewModel, nameof(PlacementViewModel.Height), false, DataSourceUpdateMode.OnPropertyChanged);
        }

        #region Form event handlers

        private void DrawTabSwitcherItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index > -1 && e.Index < tabPageEntryBindingSource.Count)
            {
                var item = (TabPageEntry)tabPageEntryBindingSource[e.Index];
                var iconBounds = new Rectangle(e.Bounds.X + (e.Bounds.Height - 32) / 2, e.Bounds.Y + (e.Bounds.Height - 32) / 2, 32, 32);
                e.Graphics.DrawSvg(item.Icon, e.ForeColor, iconBounds);
                var textBounds = e.Graphics.MeasureString(item.Text, e.Font);
                e.Graphics.DrawString(item.Text, e.Font, new SolidBrush(e.ForeColor), e.Bounds.X + e.Bounds.Height, e.Bounds.Y + (e.Bounds.Height - textBounds.Height) / 2);
            }
            e.DrawFocusRectangle();
        }

        private void TabPageEntryBindingSource_PositionChanged(object sender, EventArgs e)
        {
            panelSwitcher.SelectedIndex = tabPageEntryBindingSource.Position;
        }

        private void ShowHideButton_Click(object sender, EventArgs e)
        {
            _KneeboardManager.ToggleKneeboard();
        }

        private void ExitButtonClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnPanelSwitcherSelected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabPagePlacement)
            {
                _KneeboardManager.IsPlacementMode = true;

                var screenList = new List<PlacementMonitor>();
                for (int i = 0; i < Screen.AllScreens.Length; i++)
                {
                    screenList.Add(new PlacementMonitor(i, Screen.AllScreens[i]));
                }
                placementMonitorBindingSource.DataSource = screenList;
                placementMonitorBindingSource.Position = Screen.AllScreens.ToList().IndexOf(_KneeboardManager.CurrentScreen);
            }
            else
            {
                _KneeboardManager.IsPlacementMode = false;
            }
        }

        #endregion

        #region Favorites tab event handlers

        private bool _ResettingFavorites = false;
        private bool _ResettingVisibleGroups = false;

        private void Favorite_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_ResettingFavorites) return;
            var f = sender as Favorite;
            if (f == null) return;
            var favorites = new HashSet<int>(Settings.Default.Favorites ?? new int[0]);
            if (f.IsFavorite) favorites.Add(f.Code.Unicode);
            else favorites.Remove(f.Code.Unicode);
            Settings.Default.Favorites = favorites.ToArray();
        }

        private void GroupVisible_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_ResettingVisibleGroups) return;
            var gv = sender as GroupVisible;
            if (gv == null) return;
            var hiddenGroups = new HashSet<int>(Settings.Default.HiddenGroups ?? new int[0]);
            if (gv.Checked) hiddenGroups.Remove(gv.Group.ID);
            else hiddenGroups.Add(gv.Group.ID);
            Settings.Default.HiddenGroups = hiddenGroups.ToArray();
        }

        private void btnFavoritesResetFavorites_Click(object sender, EventArgs e)
        {
            _ResettingFavorites = true;
            foreach (var entry in _Favorites)
            {
                entry.IsFavorite = false;
            }
            gridFavorites.Refresh();
            Settings.Default.Favorites = new int[0];
            _ResettingFavorites = false;
        }

        private void btnFavoritesResetGroups_Click(object sender, EventArgs e)
        {
            _ResettingVisibleGroups = true;
            foreach (var entry in groupVisibleBindingSource.Cast<GroupVisible>())
            {
                entry.Checked = true;
            }
            gridVisibleGroups.Refresh();
            Settings.Default.HiddenGroups = new int[0];
            _ResettingVisibleGroups = false;
        }

        private void gridVisibleGroups_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (gridVisibleGroups.IsCurrentCellDirty)
            {
                gridVisibleGroups.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void gridFavorites_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (gridFavorites.IsCurrentCellDirty)
            {
                gridFavorites.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void gridFavorites_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex > (_Favorites.Count - 1)) return;
            var favorite = _Favorites[e.RowIndex];
            if (gridFavorites.Columns[e.ColumnIndex] == _IsFavoriteColumn)
            {
                e.Value = favorite.IsFavorite;
            }
            else if (gridFavorites.Columns[e.ColumnIndex] == _CharacterColumn)
            {
                e.Value = favorite.Character;
            }
            else if (gridFavorites.Columns[e.ColumnIndex] == _DescriptionColumn)
            {
                e.Value = favorite.Description;
            }
        }

        private void gridFavorites_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex > (_Favorites.Count - 1)) return;
            var favorite = _Favorites[e.RowIndex];
            if (gridFavorites.Columns[e.ColumnIndex] == _IsFavoriteColumn)
            {
                favorite.IsFavorite = (bool)e.Value;
            }
        }

        #endregion

        #region Placement tab event handlers

        private void btnPlacementDockBottom_Click(object sender, EventArgs e)
        {
            var screen = placementMonitorBindingSource.Current as PlacementMonitor;
            if (screen != null)
            {
                var y = screen.Bounds.Y + (screen.Bounds.Height - _KneeboardManager.CurrentBounds.Height);
                _KneeboardManager.SetKneeboardBounds(screen.Bounds.X, y, screen.Bounds.Width, _KneeboardManager.CurrentBounds.Height);
            }
        }

        private void btnPlacementDockTop_Click(object sender, EventArgs e)
        {
            var screen = placementMonitorBindingSource.Current as PlacementMonitor;
            if (screen != null)
            {
                _KneeboardManager.SetKneeboardBounds(screen.Bounds.X, screen.Bounds.Y, screen.Bounds.Width, _KneeboardManager.CurrentBounds.Height);
            }
        }

        private void btnPlacementDockLeft_Click(object sender, EventArgs e)
        {
            var screen = placementMonitorBindingSource.Current as PlacementMonitor;
            if (screen != null)
            {
                _KneeboardManager.SetKneeboardBounds(screen.Bounds.X, screen.Bounds.Y, _KneeboardManager.CurrentBounds.Width, screen.Bounds.Height);
            }
        }

        private void btnPlacementDockRight_Click(object sender, EventArgs e)
        {
            var screen = placementMonitorBindingSource.Current as PlacementMonitor;
            if (screen != null)
            {
                var x = screen.Bounds.X + (screen.Bounds.Width - _KneeboardManager.CurrentBounds.Width);
                _KneeboardManager.SetKneeboardBounds(x, screen.Bounds.Y, _KneeboardManager.CurrentBounds.Width, screen.Bounds.Height);
            }
        }

        private void btnPlacementCenter_Click(object sender, EventArgs e)
        {
            var screen = placementMonitorBindingSource.Current as PlacementMonitor;
            if (screen != null)
            {
                var w = screen.Bounds.Width / 2;
                var h = screen.Bounds.Height / 2;
                var x = screen.Bounds.X + screen.Bounds.Width / 4;
                var y = screen.Bounds.Y + screen.Bounds.Height / 4;
                _KneeboardManager.SetKneeboardBounds(x, y, w, h);
            }
        }

        #endregion

        #region Appearance tab

        private async void toolStripButtonAppearanceThemeNew_Click(object sender, EventArgs e)
        {
            if (await CheckDirtyAsync()) propertyGridTheme.SelectedObject = _KneeboardManager.Theme = new KneeboardTheme();
        }

        private async void toolStripButtonAppearanceThemeOpen_Click(object sender, EventArgs e)
        {
            if (await CheckDirtyAsync())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        propertyGridTheme.SelectedObject = _KneeboardManager.Theme = await ThemeFileManager.LoadAsync(openFileDialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, R.Error, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void toolStripButtonAppearanceThemeSave_Click(object sender, EventArgs e)
        {
            await DoSaveAsync();
        }

        private async void toolStripButtonAppearanceThemeSaveAs_Click(object sender, EventArgs e)
        {
            await DoSaveAsAsync();
        }

        private void toolStripButtonAppearanceThemeReset_Click(object sender, EventArgs e)
        {
            _KneeboardManager.Theme.Reset();
        }

        private async Task<bool> CheckDirtyAsync()
        {
            if (_KneeboardManager.Theme.IsDirty)
            {
                var result = MessageBox.Show(this, R.UnsavedChangesCss, R.AreYouSure, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.No) return true;
                else if (result == DialogResult.Yes)
                {
                    await DoSaveAsync();
                    return true;
                }
                else return false;
            }
            return true;
        }

        private async Task DoSaveAsync()
        {
            if (string.IsNullOrEmpty(_KneeboardManager.Theme.Filename))
            {
                await DoSaveAsAsync();
            }
            else
            {
                await DoSaveAsync(_KneeboardManager.Theme.Filename);
            }
        }

        private async Task DoSaveAsync(string filename)
        {
            try
            {
                await ThemeFileManager.SaveAsync(filename, _KneeboardManager.Theme);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, R.Error, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task DoSaveAsAsync()
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                await DoSaveAsync(saveFileDialog.FileName);
            }
        }

        #endregion
    }
}
