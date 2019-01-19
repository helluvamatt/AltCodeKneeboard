using System;
using System.Windows.Forms;
using R = AltCodeKneeboard.Properties.Resources;

namespace AltCodeKneeboard.Hotkeys
{
	public partial class HotkeyListItem : UserControl
	{
		public GlobalHotkey Hook { get; }
        public string ID { get; }

        private bool _KeyComboEditing;
        public bool KeyComboEditing
        {
            get => _KeyComboEditing;
            set
            {
                if (_KeyComboEditing != value)
                {
                    _KeyComboEditing = value;
                    OnKeyComboEditingChanged();
                }
            }
        }

		public HotkeyListItem(string id, GlobalHotkey hook)
		{
            ID = id ?? throw new ArgumentNullException(nameof(id));
            Hook = hook ?? throw new ArgumentNullException(nameof(hook));
			InitializeComponent();
            groupBox.Text = Hook.Name;
            descriptionLabel.Text = Hook.Description;
            enabledCheckBox.Checked = Hook.Enabled;
            hotkeyButton.Text = Hook.Key != null ? Hook.Key.ToString() : R.NotSet;
            hotkeyButton.Click += hotkeyButton_Click;
			enabledCheckBox.CheckedChanged += enabledCheckBox_CheckedChanged;
		}

		private void hotkeyButton_Click(object sender, EventArgs e)
		{
			HotkeyForm hotkeyForm = new HotkeyForm();
			hotkeyForm.Cancelled += (t, args) => { hotkeyForm.Close(); hotkeyForm.Dispose(); KeyComboEditing = false; };
            hotkeyForm.FormClosed += (t, args) => KeyComboEditing = false;
			hotkeyForm.KeyComboPressed += hotkeyForm_KeyComboPressed;
            KeyComboEditing = true;
            hotkeyForm.ShowDialog(ParentForm);
		}

		private void hotkeyForm_KeyComboPressed(object sender, KeyEventModArgs e)
		{
			HotkeyForm form = (HotkeyForm)sender;
			form.Close();
			form.Dispose();
			Hook.Key = e.KeyCombo;
			hotkeyButton.Text = e.KeyCombo != null ? e.KeyCombo.ToString() : R.NotSet;
            KeyComboEditing = false;
			OnHotkeyChanged();
		}

		private void enabledCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			bool enabled = enabledCheckBox.Checked;
			Hook.Enabled = enabled;
			OnHotkeyChanged();
		}

        private void OnHotkeyChanged()
        {
            HotkeyChanged?.Invoke(this, new HotkeySetComboEventArgs(ID, Hook));
        }

		public event EventHandler<HotkeySetComboEventArgs> HotkeyChanged;

        private void OnKeyComboEditingChanged()
        {
            KeyComboEditingChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler KeyComboEditingChanged;

        public class HotkeySetComboEventArgs : EventArgs
		{
            public HotkeySetComboEventArgs(string id, GlobalHotkey hook)
            {
                ID = id;
                ItemHook = hook;
            }

            public string ID { get; }
			public GlobalHotkey ItemHook { get; }
		}
	}
}
