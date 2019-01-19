using AltCodeKneeboard.Hotkeys;
using AltCodeKneeboard.Models;
using AltCodeKneeboard.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using R = AltCodeKneeboard.Properties.Resources;
using Settings = AltCodeKneeboard.Properties.Settings;

namespace AltCodeKneeboard
{
    internal class KneeboardApplication : ApplicationContext
    {
        private readonly NotifyIcon _TrayIcon;
        private readonly HotkeyManager _HotkeyManager;
        private readonly KneeboardManager _KneeboardManager;
        private readonly AltCodeData _AltCodes;

        public const string HK_MOMENTARY = "Momentary";
        public const string HK_TOGGLE = "Toggle";

        public KneeboardApplication(bool showConfig, AltCodeData altCodes)
        {
            _AltCodes = altCodes;

            _HotkeyManager = new HotkeyManager();
            _HotkeyManager.Enabled = Settings.Default.HotkeysEnabled;

            _KneeboardManager = new KneeboardManager(_AltCodes);

            var momentaryKC = KeyCombo.FromStorage(Settings.Default.MomentaryShowKeyCombo);
            var toggleKC = KeyCombo.FromStorage(Settings.Default.ToggleShowKeyCombo);
            _HotkeyManager.AddHook(HK_MOMENTARY, R.Hotkey_Momentary_Name, R.Hotkey_Momentary_Desc, Settings.Default.MomentaryShowEnabled, momentaryKC, _KneeboardManager.ShowKneeboard, _KneeboardManager.HideKneeboard, MomentaryShowHook_PropertyChanged);
            _HotkeyManager.AddHook(HK_TOGGLE, R.Hotkey_Toggle_Name, R.Hotkey_Toggle_Desc, Settings.Default.ToggleShowEnabled, toggleKC, _KneeboardManager.ToggleKneeboard, null, ToggleShowHook_PropertyChanged);

            _TrayIcon = new NotifyIcon();
            _TrayIcon.Icon = R.appicon;
            _TrayIcon.Text = R.AppTitle;
            _TrayIcon.DoubleClick += OnShowConfig;
            _TrayIcon.ContextMenuStrip = new ContextMenuStrip();
            _TrayIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem(R.Configure, FormUtils.RenderSvg(R.keyboard, SystemColors.ControlText, new Size(16, 16)), OnShowConfig));
            _TrayIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            _TrayIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem(R.ToggleKneeboard, null, (sender, args) => _KneeboardManager.ToggleKneeboard()));
            _TrayIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem(R.ShowCharMap, null, OnShowCharMap));
            _TrayIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            _TrayIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem(R.Exit, FormUtils.RenderSvg(R.close, SystemColors.ControlText, new Size(16,16)), (sender, e) => ExitThread()));
            _TrayIcon.ContextMenuStrip.Items[0].Font = new Font(_TrayIcon.ContextMenuStrip.Items[0].Font, FontStyle.Bold);
            _TrayIcon.Visible = true;

            Settings.Default.PropertyChanged += Settings_PropertyChanged;

            if (showConfig) OnShowConfig(this, EventArgs.Empty);
        }

        private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (nameof(Settings.HotkeysEnabled) == e.PropertyName)
            {
                _HotkeyManager.Enabled = Settings.Default.HotkeysEnabled;
            }
        }

        private void MomentaryShowHook_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (nameof(GlobalHotkey.Enabled) == e.PropertyName)
            {
                Settings.Default.MomentaryShowEnabled = _HotkeyManager.GetHook(HK_MOMENTARY).Enabled;
            }
            if (nameof(GlobalHotkey.Key) == e.PropertyName)
            {
                Settings.Default.MomentaryShowKeyCombo = _HotkeyManager.GetHook(HK_MOMENTARY).Key.ToStorage();
            }
        }

        private void ToggleShowHook_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (nameof(GlobalHotkey.Enabled) == e.PropertyName)
            {
                Settings.Default.ToggleShowEnabled = _HotkeyManager.GetHook(HK_TOGGLE).Enabled;
            }
            if (nameof(GlobalHotkey.Key) == e.PropertyName)
            {
                Settings.Default.ToggleShowKeyCombo = _HotkeyManager.GetHook(HK_TOGGLE).Key.ToStorage();
            }
        }

        private void OnShowConfig(object sender, EventArgs e)
        {
            bool created = false;
            if (MainForm == null)
            {
                MainForm = new ConfigForm(_HotkeyManager, _KneeboardManager, _AltCodes);
                MainForm.FormClosing += MainForm_FormClosing;
                MainForm.Resize += MainForm_Resize;
                created = true;
            }

            MainForm.Show();
            MainForm.Activate();

            if (MainForm.WindowState == FormWindowState.Minimized)
            {
                MainForm.WindowState = FormWindowState.Normal;
            }
            else if (!created)
            {
                MainForm.BringToFront(true);
            }
        }

        private void OnShowCharMap(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("charmap.exe");
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == MainForm.WindowState)
            {
                _TrayIcon.ShowBalloonTip(500, R.AppTitle, R.MinimizedToTray, ToolTipIcon.Info);
                MainForm.Hide();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                _TrayIcon.ShowBalloonTip(500, R.AppTitle, R.MinimizedToTray, ToolTipIcon.Info);
                MainForm.Hide();
                e.Cancel = true;
            }
        }

        #region ApplicationContext overrides

        protected override void OnMainFormClosed(object sender, EventArgs e)
        {
            // Don't call base here, we only want to exit explicitly
            MainForm = null;
        }

        protected override void Dispose(bool disposing)
        {
            if (_TrayIcon != null) _TrayIcon.Dispose();
            if (MainForm != null) MainForm.Dispose();
            base.Dispose(disposing);
        }

        #endregion
    }
}
