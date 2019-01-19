namespace AltCodeKneeboard
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.tabSwitcher = new AltCodeKneeboard.Controls.ListBoxEx();
            this.tabPageEntryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panelSwitcher = new AltCodeKneeboard.Controls.PanelSwitcher();
            this.tabPageHotkeys = new System.Windows.Forms.TabPage();
            this.chkHotkeysEnabled = new System.Windows.Forms.CheckBox();
            this.hotkeyListPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPageFavorites = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelFavorites = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxSort = new System.Windows.Forms.GroupBox();
            this.cmbStyleSort = new System.Windows.Forms.ComboBox();
            this.groupBoxFavorites = new System.Windows.Forms.GroupBox();
            this.gridFavorites = new System.Windows.Forms.DataGridView();
            this._IsFavoriteColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this._CharacterColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._DescriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnFavoritesResetFavorites = new System.Windows.Forms.Button();
            this.groupBoxShowGroups = new System.Windows.Forms.GroupBox();
            this.gridVisibleGroups = new System.Windows.Forms.DataGridView();
            this.groupsVisibleCheckedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.textDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupVisibleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnFavoritesResetGroups = new System.Windows.Forms.Button();
            this.tabPageAppearance = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelAppearance = new System.Windows.Forms.TableLayoutPanel();
            this.propertyGridTheme = new System.Windows.Forms.PropertyGrid();
            this.appearanceThemeToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAppearanceThemeNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAppearanceThemeOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAppearanceThemeSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAppearanceThemeSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabelAppearanceThemeName = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAppearanceThemeSortByCategory = new AltCodeKneeboard.Controls.BindableToolStripButton();
            this.toolStripButtonAppearanceThemeSortAlpha = new AltCodeKneeboard.Controls.BindableToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAppearanceThemeReset = new System.Windows.Forms.ToolStripButton();
            this.tabPagePlacement = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelPlacement1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblHeight = new System.Windows.Forms.Label();
            this.numPlacementHeight = new System.Windows.Forms.NumericUpDown();
            this.cmbPlacementMonitor = new System.Windows.Forms.ComboBox();
            this.placementMonitorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.numPlacementWidth = new System.Windows.Forms.NumericUpDown();
            this.lblMonitor = new System.Windows.Forms.Label();
            this.numPlacementY = new System.Windows.Forms.NumericUpDown();
            this.numPlacementX = new System.Windows.Forms.NumericUpDown();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblPlacementY = new System.Windows.Forms.Label();
            this.lblPlacementX = new System.Windows.Forms.Label();
            this.tableLayoutPanelPlacement2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnPlacementCenter = new System.Windows.Forms.Button();
            this.btnPlacementDockBottom = new System.Windows.Forms.Button();
            this.btnPlacementDockTop = new System.Windows.Forms.Button();
            this.btnPlacementDockRight = new System.Windows.Forms.Button();
            this.btnPlacementDockLeft = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnShowHide = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.tabPageEntryBindingSource)).BeginInit();
            this.panelSwitcher.SuspendLayout();
            this.tabPageHotkeys.SuspendLayout();
            this.tabPageFavorites.SuspendLayout();
            this.tableLayoutPanelFavorites.SuspendLayout();
            this.groupBoxSort.SuspendLayout();
            this.groupBoxFavorites.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridFavorites)).BeginInit();
            this.groupBoxShowGroups.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridVisibleGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupVisibleBindingSource)).BeginInit();
            this.tabPageAppearance.SuspendLayout();
            this.tableLayoutPanelAppearance.SuspendLayout();
            this.appearanceThemeToolStrip.SuspendLayout();
            this.tabPagePlacement.SuspendLayout();
            this.tableLayoutPanelPlacement1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPlacementHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.placementMonitorBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPlacementWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPlacementY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPlacementX)).BeginInit();
            this.tableLayoutPanelPlacement2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabSwitcher
            // 
            resources.ApplyResources(this.tabSwitcher, "tabSwitcher");
            this.tabSwitcher.DataSource = this.tabPageEntryBindingSource;
            this.tabSwitcher.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.tabSwitcher.FormattingEnabled = true;
            this.tabSwitcher.Name = "tabSwitcher";
            this.tabSwitcher.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.DrawTabSwitcherItem);
            // 
            // tabPageEntryBindingSource
            // 
            this.tabPageEntryBindingSource.DataSource = typeof(AltCodeKneeboard.Models.TabPageEntry);
            this.tabPageEntryBindingSource.PositionChanged += new System.EventHandler(this.TabPageEntryBindingSource_PositionChanged);
            // 
            // panelSwitcher
            // 
            resources.ApplyResources(this.panelSwitcher, "panelSwitcher");
            this.panelSwitcher.Controls.Add(this.tabPageHotkeys);
            this.panelSwitcher.Controls.Add(this.tabPageFavorites);
            this.panelSwitcher.Controls.Add(this.tabPageAppearance);
            this.panelSwitcher.Controls.Add(this.tabPagePlacement);
            this.panelSwitcher.Name = "panelSwitcher";
            this.panelSwitcher.SelectedIndex = 0;
            this.panelSwitcher.Selected += new System.Windows.Forms.TabControlEventHandler(this.OnPanelSwitcherSelected);
            // 
            // tabPageHotkeys
            // 
            this.tabPageHotkeys.Controls.Add(this.chkHotkeysEnabled);
            this.tabPageHotkeys.Controls.Add(this.hotkeyListPanel);
            resources.ApplyResources(this.tabPageHotkeys, "tabPageHotkeys");
            this.tabPageHotkeys.Name = "tabPageHotkeys";
            this.tabPageHotkeys.UseVisualStyleBackColor = true;
            // 
            // chkHotkeysEnabled
            // 
            resources.ApplyResources(this.chkHotkeysEnabled, "chkHotkeysEnabled");
            this.chkHotkeysEnabled.Checked = global::AltCodeKneeboard.Properties.Settings.Default.HotkeysEnabled;
            this.chkHotkeysEnabled.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::AltCodeKneeboard.Properties.Settings.Default, "HotkeysEnabled", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkHotkeysEnabled.Name = "chkHotkeysEnabled";
            this.chkHotkeysEnabled.UseVisualStyleBackColor = true;
            // 
            // hotkeyListPanel
            // 
            resources.ApplyResources(this.hotkeyListPanel, "hotkeyListPanel");
            this.hotkeyListPanel.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", global::AltCodeKneeboard.Properties.Settings.Default, "HotkeysEnabled", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.hotkeyListPanel.Enabled = global::AltCodeKneeboard.Properties.Settings.Default.HotkeysEnabled;
            this.hotkeyListPanel.Name = "hotkeyListPanel";
            // 
            // tabPageFavorites
            // 
            this.tabPageFavorites.Controls.Add(this.tableLayoutPanelFavorites);
            resources.ApplyResources(this.tabPageFavorites, "tabPageFavorites");
            this.tabPageFavorites.Name = "tabPageFavorites";
            this.tabPageFavorites.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelFavorites
            // 
            resources.ApplyResources(this.tableLayoutPanelFavorites, "tableLayoutPanelFavorites");
            this.tableLayoutPanelFavorites.Controls.Add(this.groupBoxSort, 0, 0);
            this.tableLayoutPanelFavorites.Controls.Add(this.groupBoxFavorites, 0, 1);
            this.tableLayoutPanelFavorites.Controls.Add(this.groupBoxShowGroups, 1, 1);
            this.tableLayoutPanelFavorites.Name = "tableLayoutPanelFavorites";
            // 
            // groupBoxSort
            // 
            resources.ApplyResources(this.groupBoxSort, "groupBoxSort");
            this.tableLayoutPanelFavorites.SetColumnSpan(this.groupBoxSort, 2);
            this.groupBoxSort.Controls.Add(this.cmbStyleSort);
            this.groupBoxSort.Name = "groupBoxSort";
            this.groupBoxSort.TabStop = false;
            // 
            // cmbStyleSort
            // 
            resources.ApplyResources(this.cmbStyleSort, "cmbStyleSort");
            this.cmbStyleSort.DisplayMember = "Text";
            this.cmbStyleSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStyleSort.FormattingEnabled = true;
            this.cmbStyleSort.Name = "cmbStyleSort";
            this.cmbStyleSort.ValueMember = "Value";
            // 
            // groupBoxFavorites
            // 
            this.groupBoxFavorites.Controls.Add(this.gridFavorites);
            this.groupBoxFavorites.Controls.Add(this.btnFavoritesResetFavorites);
            resources.ApplyResources(this.groupBoxFavorites, "groupBoxFavorites");
            this.groupBoxFavorites.Name = "groupBoxFavorites";
            this.groupBoxFavorites.TabStop = false;
            // 
            // gridFavorites
            // 
            this.gridFavorites.AllowUserToAddRows = false;
            this.gridFavorites.AllowUserToDeleteRows = false;
            this.gridFavorites.AllowUserToResizeColumns = false;
            this.gridFavorites.AllowUserToResizeRows = false;
            resources.ApplyResources(this.gridFavorites, "gridFavorites");
            this.gridFavorites.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridFavorites.BackgroundColor = System.Drawing.SystemColors.Control;
            this.gridFavorites.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gridFavorites.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFavorites.ColumnHeadersVisible = false;
            this.gridFavorites.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._IsFavoriteColumn,
            this._CharacterColumn,
            this._DescriptionColumn});
            this.gridFavorites.Name = "gridFavorites";
            this.gridFavorites.RowHeadersVisible = false;
            this.gridFavorites.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridFavorites.VirtualMode = true;
            this.gridFavorites.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.gridFavorites_CellValueNeeded);
            this.gridFavorites.CellValuePushed += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.gridFavorites_CellValuePushed);
            this.gridFavorites.CurrentCellDirtyStateChanged += new System.EventHandler(this.gridFavorites_CurrentCellDirtyStateChanged);
            // 
            // _IsFavoriteColumn
            // 
            this._IsFavoriteColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            resources.ApplyResources(this._IsFavoriteColumn, "_IsFavoriteColumn");
            this._IsFavoriteColumn.Name = "_IsFavoriteColumn";
            // 
            // _CharacterColumn
            // 
            this._CharacterColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            resources.ApplyResources(this._CharacterColumn, "_CharacterColumn");
            this._CharacterColumn.Name = "_CharacterColumn";
            this._CharacterColumn.ReadOnly = true;
            // 
            // _DescriptionColumn
            // 
            this._DescriptionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this._DescriptionColumn, "_DescriptionColumn");
            this._DescriptionColumn.Name = "_DescriptionColumn";
            this._DescriptionColumn.ReadOnly = true;
            // 
            // btnFavoritesResetFavorites
            // 
            resources.ApplyResources(this.btnFavoritesResetFavorites, "btnFavoritesResetFavorites");
            this.btnFavoritesResetFavorites.Name = "btnFavoritesResetFavorites";
            this.btnFavoritesResetFavorites.UseVisualStyleBackColor = true;
            this.btnFavoritesResetFavorites.Click += new System.EventHandler(this.btnFavoritesResetFavorites_Click);
            // 
            // groupBoxShowGroups
            // 
            this.groupBoxShowGroups.Controls.Add(this.gridVisibleGroups);
            this.groupBoxShowGroups.Controls.Add(this.btnFavoritesResetGroups);
            resources.ApplyResources(this.groupBoxShowGroups, "groupBoxShowGroups");
            this.groupBoxShowGroups.Name = "groupBoxShowGroups";
            this.groupBoxShowGroups.TabStop = false;
            // 
            // gridVisibleGroups
            // 
            this.gridVisibleGroups.AllowUserToAddRows = false;
            this.gridVisibleGroups.AllowUserToDeleteRows = false;
            this.gridVisibleGroups.AllowUserToResizeColumns = false;
            this.gridVisibleGroups.AllowUserToResizeRows = false;
            resources.ApplyResources(this.gridVisibleGroups, "gridVisibleGroups");
            this.gridVisibleGroups.AutoGenerateColumns = false;
            this.gridVisibleGroups.BackgroundColor = System.Drawing.SystemColors.Control;
            this.gridVisibleGroups.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gridVisibleGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridVisibleGroups.ColumnHeadersVisible = false;
            this.gridVisibleGroups.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.groupsVisibleCheckedDataGridViewCheckBoxColumn,
            this.textDataGridViewTextBoxColumn});
            this.gridVisibleGroups.DataSource = this.groupVisibleBindingSource;
            this.gridVisibleGroups.Name = "gridVisibleGroups";
            this.gridVisibleGroups.RowHeadersVisible = false;
            this.gridVisibleGroups.RowTemplate.Height = 20;
            this.gridVisibleGroups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridVisibleGroups.CurrentCellDirtyStateChanged += new System.EventHandler(this.gridVisibleGroups_CurrentCellDirtyStateChanged);
            // 
            // groupsVisibleCheckedDataGridViewCheckBoxColumn
            // 
            this.groupsVisibleCheckedDataGridViewCheckBoxColumn.DataPropertyName = "Checked";
            resources.ApplyResources(this.groupsVisibleCheckedDataGridViewCheckBoxColumn, "groupsVisibleCheckedDataGridViewCheckBoxColumn");
            this.groupsVisibleCheckedDataGridViewCheckBoxColumn.Name = "groupsVisibleCheckedDataGridViewCheckBoxColumn";
            // 
            // textDataGridViewTextBoxColumn
            // 
            this.textDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.textDataGridViewTextBoxColumn.DataPropertyName = "Text";
            resources.ApplyResources(this.textDataGridViewTextBoxColumn, "textDataGridViewTextBoxColumn");
            this.textDataGridViewTextBoxColumn.Name = "textDataGridViewTextBoxColumn";
            this.textDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // groupVisibleBindingSource
            // 
            this.groupVisibleBindingSource.DataSource = typeof(AltCodeKneeboard.Models.GroupVisible);
            // 
            // btnFavoritesResetGroups
            // 
            resources.ApplyResources(this.btnFavoritesResetGroups, "btnFavoritesResetGroups");
            this.btnFavoritesResetGroups.Name = "btnFavoritesResetGroups";
            this.btnFavoritesResetGroups.UseVisualStyleBackColor = true;
            this.btnFavoritesResetGroups.Click += new System.EventHandler(this.btnFavoritesResetGroups_Click);
            // 
            // tabPageAppearance
            // 
            this.tabPageAppearance.Controls.Add(this.tableLayoutPanelAppearance);
            resources.ApplyResources(this.tabPageAppearance, "tabPageAppearance");
            this.tabPageAppearance.Name = "tabPageAppearance";
            this.tabPageAppearance.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelAppearance
            // 
            resources.ApplyResources(this.tableLayoutPanelAppearance, "tableLayoutPanelAppearance");
            this.tableLayoutPanelAppearance.Controls.Add(this.propertyGridTheme, 0, 1);
            this.tableLayoutPanelAppearance.Controls.Add(this.appearanceThemeToolStrip, 0, 0);
            this.tableLayoutPanelAppearance.Name = "tableLayoutPanelAppearance";
            // 
            // propertyGridTheme
            // 
            resources.ApplyResources(this.propertyGridTheme, "propertyGridTheme");
            this.propertyGridTheme.Name = "propertyGridTheme";
            this.propertyGridTheme.ToolbarVisible = false;
            // 
            // appearanceThemeToolStrip
            // 
            resources.ApplyResources(this.appearanceThemeToolStrip, "appearanceThemeToolStrip");
            this.appearanceThemeToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.appearanceThemeToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAppearanceThemeNew,
            this.toolStripButtonAppearanceThemeOpen,
            this.toolStripButtonAppearanceThemeSave,
            this.toolStripButtonAppearanceThemeSaveAs,
            this.toolStripSeparator1,
            this.toolStripLabelAppearanceThemeName,
            this.toolStripSeparator2,
            this.toolStripButtonAppearanceThemeSortByCategory,
            this.toolStripButtonAppearanceThemeSortAlpha,
            this.toolStripSeparator3,
            this.toolStripButtonAppearanceThemeReset});
            this.appearanceThemeToolStrip.Name = "appearanceThemeToolStrip";
            this.appearanceThemeToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            // 
            // toolStripButtonAppearanceThemeNew
            // 
            this.toolStripButtonAppearanceThemeNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAppearanceThemeNew.Image = global::AltCodeKneeboard.Properties.Resources._new;
            resources.ApplyResources(this.toolStripButtonAppearanceThemeNew, "toolStripButtonAppearanceThemeNew");
            this.toolStripButtonAppearanceThemeNew.Name = "toolStripButtonAppearanceThemeNew";
            this.toolStripButtonAppearanceThemeNew.Click += new System.EventHandler(this.toolStripButtonAppearanceThemeNew_Click);
            // 
            // toolStripButtonAppearanceThemeOpen
            // 
            this.toolStripButtonAppearanceThemeOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAppearanceThemeOpen.Image = global::AltCodeKneeboard.Properties.Resources.open;
            resources.ApplyResources(this.toolStripButtonAppearanceThemeOpen, "toolStripButtonAppearanceThemeOpen");
            this.toolStripButtonAppearanceThemeOpen.Name = "toolStripButtonAppearanceThemeOpen";
            this.toolStripButtonAppearanceThemeOpen.Click += new System.EventHandler(this.toolStripButtonAppearanceThemeOpen_Click);
            // 
            // toolStripButtonAppearanceThemeSave
            // 
            this.toolStripButtonAppearanceThemeSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAppearanceThemeSave.Image = global::AltCodeKneeboard.Properties.Resources.save;
            resources.ApplyResources(this.toolStripButtonAppearanceThemeSave, "toolStripButtonAppearanceThemeSave");
            this.toolStripButtonAppearanceThemeSave.Name = "toolStripButtonAppearanceThemeSave";
            this.toolStripButtonAppearanceThemeSave.Click += new System.EventHandler(this.toolStripButtonAppearanceThemeSave_Click);
            // 
            // toolStripButtonAppearanceThemeSaveAs
            // 
            this.toolStripButtonAppearanceThemeSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAppearanceThemeSaveAs.Image = global::AltCodeKneeboard.Properties.Resources.save_as;
            resources.ApplyResources(this.toolStripButtonAppearanceThemeSaveAs, "toolStripButtonAppearanceThemeSaveAs");
            this.toolStripButtonAppearanceThemeSaveAs.Name = "toolStripButtonAppearanceThemeSaveAs";
            this.toolStripButtonAppearanceThemeSaveAs.Click += new System.EventHandler(this.toolStripButtonAppearanceThemeSaveAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripLabelAppearanceThemeName
            // 
            this.toolStripLabelAppearanceThemeName.Name = "toolStripLabelAppearanceThemeName";
            resources.ApplyResources(this.toolStripLabelAppearanceThemeName, "toolStripLabelAppearanceThemeName");
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // toolStripButtonAppearanceThemeSortByCategory
            // 
            this.toolStripButtonAppearanceThemeSortByCategory.CheckOnClick = true;
            this.toolStripButtonAppearanceThemeSortByCategory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAppearanceThemeSortByCategory.Image = global::AltCodeKneeboard.Properties.Resources.category;
            resources.ApplyResources(this.toolStripButtonAppearanceThemeSortByCategory, "toolStripButtonAppearanceThemeSortByCategory");
            this.toolStripButtonAppearanceThemeSortByCategory.Name = "toolStripButtonAppearanceThemeSortByCategory";
            // 
            // toolStripButtonAppearanceThemeSortAlpha
            // 
            this.toolStripButtonAppearanceThemeSortAlpha.CheckOnClick = true;
            this.toolStripButtonAppearanceThemeSortAlpha.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAppearanceThemeSortAlpha.Image = global::AltCodeKneeboard.Properties.Resources.sort_asc_az;
            resources.ApplyResources(this.toolStripButtonAppearanceThemeSortAlpha, "toolStripButtonAppearanceThemeSortAlpha");
            this.toolStripButtonAppearanceThemeSortAlpha.Name = "toolStripButtonAppearanceThemeSortAlpha";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // toolStripButtonAppearanceThemeReset
            // 
            this.toolStripButtonAppearanceThemeReset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAppearanceThemeReset.Image = global::AltCodeKneeboard.Properties.Resources.clear_formatting;
            resources.ApplyResources(this.toolStripButtonAppearanceThemeReset, "toolStripButtonAppearanceThemeReset");
            this.toolStripButtonAppearanceThemeReset.Name = "toolStripButtonAppearanceThemeReset";
            this.toolStripButtonAppearanceThemeReset.Click += new System.EventHandler(this.toolStripButtonAppearanceThemeReset_Click);
            // 
            // tabPagePlacement
            // 
            this.tabPagePlacement.Controls.Add(this.tableLayoutPanelPlacement1);
            this.tabPagePlacement.Controls.Add(this.tableLayoutPanelPlacement2);
            resources.ApplyResources(this.tabPagePlacement, "tabPagePlacement");
            this.tabPagePlacement.Name = "tabPagePlacement";
            this.tabPagePlacement.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelPlacement1
            // 
            resources.ApplyResources(this.tableLayoutPanelPlacement1, "tableLayoutPanelPlacement1");
            this.tableLayoutPanelPlacement1.Controls.Add(this.lblHeight, 0, 4);
            this.tableLayoutPanelPlacement1.Controls.Add(this.numPlacementHeight, 1, 4);
            this.tableLayoutPanelPlacement1.Controls.Add(this.cmbPlacementMonitor, 1, 0);
            this.tableLayoutPanelPlacement1.Controls.Add(this.numPlacementWidth, 1, 3);
            this.tableLayoutPanelPlacement1.Controls.Add(this.lblMonitor, 0, 0);
            this.tableLayoutPanelPlacement1.Controls.Add(this.numPlacementY, 1, 2);
            this.tableLayoutPanelPlacement1.Controls.Add(this.numPlacementX, 1, 1);
            this.tableLayoutPanelPlacement1.Controls.Add(this.lblWidth, 0, 3);
            this.tableLayoutPanelPlacement1.Controls.Add(this.lblPlacementY, 0, 2);
            this.tableLayoutPanelPlacement1.Controls.Add(this.lblPlacementX, 0, 1);
            this.tableLayoutPanelPlacement1.Name = "tableLayoutPanelPlacement1";
            // 
            // lblHeight
            // 
            resources.ApplyResources(this.lblHeight, "lblHeight");
            this.lblHeight.Name = "lblHeight";
            // 
            // numPlacementHeight
            // 
            resources.ApplyResources(this.numPlacementHeight, "numPlacementHeight");
            this.numPlacementHeight.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numPlacementHeight.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.numPlacementHeight.Name = "numPlacementHeight";
            // 
            // cmbPlacementMonitor
            // 
            resources.ApplyResources(this.cmbPlacementMonitor, "cmbPlacementMonitor");
            this.cmbPlacementMonitor.DataSource = this.placementMonitorBindingSource;
            this.cmbPlacementMonitor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlacementMonitor.FormattingEnabled = true;
            this.cmbPlacementMonitor.Name = "cmbPlacementMonitor";
            // 
            // placementMonitorBindingSource
            // 
            this.placementMonitorBindingSource.DataSource = typeof(AltCodeKneeboard.Models.PlacementMonitor);
            // 
            // numPlacementWidth
            // 
            resources.ApplyResources(this.numPlacementWidth, "numPlacementWidth");
            this.numPlacementWidth.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numPlacementWidth.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.numPlacementWidth.Name = "numPlacementWidth";
            // 
            // lblMonitor
            // 
            resources.ApplyResources(this.lblMonitor, "lblMonitor");
            this.lblMonitor.Name = "lblMonitor";
            // 
            // numPlacementY
            // 
            resources.ApplyResources(this.numPlacementY, "numPlacementY");
            this.numPlacementY.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numPlacementY.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.numPlacementY.Name = "numPlacementY";
            // 
            // numPlacementX
            // 
            resources.ApplyResources(this.numPlacementX, "numPlacementX");
            this.numPlacementX.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numPlacementX.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.numPlacementX.Name = "numPlacementX";
            // 
            // lblWidth
            // 
            resources.ApplyResources(this.lblWidth, "lblWidth");
            this.lblWidth.Name = "lblWidth";
            // 
            // lblPlacementY
            // 
            resources.ApplyResources(this.lblPlacementY, "lblPlacementY");
            this.lblPlacementY.Name = "lblPlacementY";
            // 
            // lblPlacementX
            // 
            resources.ApplyResources(this.lblPlacementX, "lblPlacementX");
            this.lblPlacementX.Name = "lblPlacementX";
            // 
            // tableLayoutPanelPlacement2
            // 
            resources.ApplyResources(this.tableLayoutPanelPlacement2, "tableLayoutPanelPlacement2");
            this.tableLayoutPanelPlacement2.Controls.Add(this.btnPlacementCenter, 1, 1);
            this.tableLayoutPanelPlacement2.Controls.Add(this.btnPlacementDockBottom, 1, 2);
            this.tableLayoutPanelPlacement2.Controls.Add(this.btnPlacementDockTop, 1, 0);
            this.tableLayoutPanelPlacement2.Controls.Add(this.btnPlacementDockRight, 2, 1);
            this.tableLayoutPanelPlacement2.Controls.Add(this.btnPlacementDockLeft, 0, 1);
            this.tableLayoutPanelPlacement2.Name = "tableLayoutPanelPlacement2";
            // 
            // btnPlacementCenter
            // 
            resources.ApplyResources(this.btnPlacementCenter, "btnPlacementCenter");
            this.btnPlacementCenter.Name = "btnPlacementCenter";
            this.btnPlacementCenter.UseVisualStyleBackColor = true;
            this.btnPlacementCenter.Click += new System.EventHandler(this.btnPlacementCenter_Click);
            // 
            // btnPlacementDockBottom
            // 
            resources.ApplyResources(this.btnPlacementDockBottom, "btnPlacementDockBottom");
            this.btnPlacementDockBottom.Name = "btnPlacementDockBottom";
            this.btnPlacementDockBottom.UseVisualStyleBackColor = true;
            this.btnPlacementDockBottom.Click += new System.EventHandler(this.btnPlacementDockBottom_Click);
            // 
            // btnPlacementDockTop
            // 
            resources.ApplyResources(this.btnPlacementDockTop, "btnPlacementDockTop");
            this.btnPlacementDockTop.Name = "btnPlacementDockTop";
            this.btnPlacementDockTop.UseVisualStyleBackColor = true;
            this.btnPlacementDockTop.Click += new System.EventHandler(this.btnPlacementDockTop_Click);
            // 
            // btnPlacementDockRight
            // 
            resources.ApplyResources(this.btnPlacementDockRight, "btnPlacementDockRight");
            this.btnPlacementDockRight.Name = "btnPlacementDockRight";
            this.btnPlacementDockRight.UseVisualStyleBackColor = true;
            this.btnPlacementDockRight.Click += new System.EventHandler(this.btnPlacementDockRight_Click);
            // 
            // btnPlacementDockLeft
            // 
            resources.ApplyResources(this.btnPlacementDockLeft, "btnPlacementDockLeft");
            this.btnPlacementDockLeft.Name = "btnPlacementDockLeft";
            this.btnPlacementDockLeft.UseVisualStyleBackColor = true;
            this.btnPlacementDockLeft.Click += new System.EventHandler(this.btnPlacementDockLeft_Click);
            // 
            // btnExit
            // 
            resources.ApplyResources(this.btnExit, "btnExit");
            this.btnExit.Name = "btnExit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.ExitButtonClick);
            // 
            // btnShowHide
            // 
            resources.ApplyResources(this.btnShowHide, "btnShowHide");
            this.btnShowHide.Name = "btnShowHide";
            this.btnShowHide.UseVisualStyleBackColor = true;
            this.btnShowHide.Click += new System.EventHandler(this.ShowHideButton_Click);
            // 
            // colorDialog
            // 
            this.colorDialog.AnyColor = true;
            this.colorDialog.FullOpen = true;
            // 
            // openFileDialog
            // 
            resources.ApplyResources(this.openFileDialog, "openFileDialog");
            this.openFileDialog.RestoreDirectory = true;
            // 
            // saveFileDialog
            // 
            resources.ApplyResources(this.saveFileDialog, "saveFileDialog");
            this.saveFileDialog.RestoreDirectory = true;
            // 
            // ConfigForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnShowHide);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.tabSwitcher);
            this.Controls.Add(this.panelSwitcher);
            this.Icon = global::AltCodeKneeboard.Properties.Resources.appicon;
            this.Name = "ConfigForm";
            ((System.ComponentModel.ISupportInitialize)(this.tabPageEntryBindingSource)).EndInit();
            this.panelSwitcher.ResumeLayout(false);
            this.tabPageHotkeys.ResumeLayout(false);
            this.tabPageHotkeys.PerformLayout();
            this.tabPageFavorites.ResumeLayout(false);
            this.tableLayoutPanelFavorites.ResumeLayout(false);
            this.groupBoxSort.ResumeLayout(false);
            this.groupBoxFavorites.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridFavorites)).EndInit();
            this.groupBoxShowGroups.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridVisibleGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupVisibleBindingSource)).EndInit();
            this.tabPageAppearance.ResumeLayout(false);
            this.tableLayoutPanelAppearance.ResumeLayout(false);
            this.tableLayoutPanelAppearance.PerformLayout();
            this.appearanceThemeToolStrip.ResumeLayout(false);
            this.appearanceThemeToolStrip.PerformLayout();
            this.tabPagePlacement.ResumeLayout(false);
            this.tableLayoutPanelPlacement1.ResumeLayout(false);
            this.tableLayoutPanelPlacement1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPlacementHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.placementMonitorBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPlacementWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPlacementY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPlacementX)).EndInit();
            this.tableLayoutPanelPlacement2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPageFavorites;
        private System.Windows.Forms.TabPage tabPageHotkeys;
        private AltCodeKneeboard.Controls.PanelSwitcher panelSwitcher;
        private AltCodeKneeboard.Controls.ListBoxEx tabSwitcher;
        private System.Windows.Forms.BindingSource tabPageEntryBindingSource;
        private System.Windows.Forms.FlowLayoutPanel hotkeyListPanel;
        private System.Windows.Forms.CheckBox chkHotkeysEnabled;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TabPage tabPageAppearance;
        private System.Windows.Forms.TabPage tabPagePlacement;
        private System.Windows.Forms.Label lblMonitor;
        private System.Windows.Forms.ComboBox cmbPlacementMonitor;
        private System.Windows.Forms.BindingSource placementMonitorBindingSource;
        private System.Windows.Forms.Button btnPlacementDockBottom;
        private System.Windows.Forms.Button btnPlacementCenter;
        private System.Windows.Forms.Button btnPlacementDockRight;
        private System.Windows.Forms.Button btnPlacementDockLeft;
        private System.Windows.Forms.Button btnPlacementDockTop;
        private System.Windows.Forms.Button btnShowHide;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFavorites;
        private System.Windows.Forms.GroupBox groupBoxFavorites;
        private System.Windows.Forms.GroupBox groupBoxShowGroups;
        private System.Windows.Forms.Button btnFavoritesResetFavorites;
        private System.Windows.Forms.Button btnFavoritesResetGroups;
        private System.Windows.Forms.BindingSource groupVisibleBindingSource;
        private System.Windows.Forms.DataGridView gridVisibleGroups;
        private System.Windows.Forms.DataGridViewCheckBoxColumn groupsVisibleCheckedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn textDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridView gridFavorites;
        private System.Windows.Forms.DataGridViewCheckBoxColumn _IsFavoriteColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn _CharacterColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn _DescriptionColumn;
        private System.Windows.Forms.GroupBox groupBoxSort;
        private System.Windows.Forms.ComboBox cmbStyleSort;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelAppearance;
        private System.Windows.Forms.PropertyGrid propertyGridTheme;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelPlacement1;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.NumericUpDown numPlacementHeight;
        private System.Windows.Forms.NumericUpDown numPlacementWidth;
        private System.Windows.Forms.NumericUpDown numPlacementY;
        private System.Windows.Forms.NumericUpDown numPlacementX;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblPlacementY;
        private System.Windows.Forms.Label lblPlacementX;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelPlacement2;
        private System.Windows.Forms.ToolStrip appearanceThemeToolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonAppearanceThemeNew;
        private System.Windows.Forms.ToolStripButton toolStripButtonAppearanceThemeOpen;
        private System.Windows.Forms.ToolStripButton toolStripButtonAppearanceThemeSave;
        private System.Windows.Forms.ToolStripButton toolStripButtonAppearanceThemeSaveAs;
        private System.Windows.Forms.ToolStripLabel toolStripLabelAppearanceThemeName;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private AltCodeKneeboard.Controls.BindableToolStripButton toolStripButtonAppearanceThemeSortByCategory;
        private AltCodeKneeboard.Controls.BindableToolStripButton toolStripButtonAppearanceThemeSortAlpha;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonAppearanceThemeReset;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}