using System;
using System.Text;
using System.Windows.Forms;
using R = AltCodeKneeboard.Properties.Resources;

namespace AltCodeKneeboard.Hotkeys
{
	public partial class HotkeyForm : Form
	{
		private const int WM_KEYDOWN = 0x0100;
		private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_SYSKEYUP = 0x0105;

        private bool _WinPressed = false;
		private bool _ShiftPressed = false;
		private bool _CtrlPressed = false;
		private bool _AltPressed = false;
		private Keys _KeyPressed = Keys.None;

		public HotkeyForm()
		{
			InitializeComponent();
			UpdateUI();
		}

		private void UpdateUI()
		{
			StringBuilder sb = new StringBuilder();
			if (_CtrlPressed) sb.Append(R.Ctrl);
			if (_ShiftPressed)
			{
				if (sb.Length > 0) sb.Append(" + ");
				sb.Append(R.Shift);
			}
			if (_AltPressed)
			{
				if (sb.Length > 0) sb.Append(" + ");
				sb.Append(R.Alt);
			}
			if (_WinPressed)
			{
				if (sb.Length > 0) sb.Append(" + ");
				sb.Append(R.Win);
			}
			if (_KeyPressed != Keys.None)
			{
				if (sb.Length > 0) sb.Append(" + ");
				sb.Append(_KeyPressed);
			}
			if (sb.Length > 0)
			{
				keyCombinationLabel.Text = sb.ToString();
			}
			else
			{
				keyCombinationLabel.Text = R.PressKeyCombination;
			}
		}

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_KEYDOWN || m.Msg == WM_SYSKEYDOWN)
            {
                Keys key = (Keys)m.WParam;
                Keys keyCode = key & Keys.KeyCode;
                switch (keyCode)
                {
                    case Keys.ShiftKey:
                    case Keys.LShiftKey:
                    case Keys.RShiftKey:
                        _ShiftPressed = true;
                        break;
                    case Keys.ControlKey:
                    case Keys.LControlKey:
                    case Keys.RControlKey:
                        _CtrlPressed = true;
                        break;
                    case Keys.LMenu:
                    case Keys.RMenu:
                    case Keys.Menu:
                        _AltPressed = true;
                        break;
                    case Keys.RWin:
                    case Keys.LWin:
                        _WinPressed = true;
                        break;
                    default:
                        _KeyPressed = keyCode;
                        break;
                }
                UpdateUI();
                m.Result = IntPtr.Zero;
            }
            else if (m.Msg == WM_KEYUP || m.Msg == WM_SYSKEYUP)
            {
                Keys key = (Keys)m.WParam;
                Keys keyCode = key & Keys.KeyCode;
                switch (keyCode)
                {
                    case Keys.ShiftKey:
                    case Keys.LShiftKey:
                    case Keys.RShiftKey:
                        if (_KeyPressed == Keys.None) _ShiftPressed = false;
                        break;
                    case Keys.ControlKey:
                    case Keys.LControlKey:
                    case Keys.RControlKey:
                        if (_KeyPressed == Keys.None) _CtrlPressed = false;
                        break;
                    case Keys.LMenu:
                    case Keys.RMenu:
                    case Keys.Menu:
                        if (_KeyPressed == Keys.None) _AltPressed = false;
                        break;
                    case Keys.RWin:
                    case Keys.LWin:
                        if (_KeyPressed == Keys.None) _WinPressed = false;
                        break;
                    default:
                        if (keyCode == Keys.Escape && !_ShiftPressed && !_CtrlPressed && !_AltPressed && !_WinPressed)
                        {
                            Cancelled?.Invoke(this, new EventArgs());
                        }
                        else
                        {
                            if (KeyComboPressed != null)
                            {
                                var modifier = new KeyModifier();
                                if (_AltPressed) modifier |= KeyModifier.Alt;
                                if (_CtrlPressed) modifier |= KeyModifier.Control;
                                if (_ShiftPressed) modifier |= KeyModifier.Shift;
                                if (_WinPressed) modifier |= KeyModifier.Win;
                                KeyComboPressed(this, new KeyEventModArgs(_KeyPressed, modifier));
                            }
                        }
                        break;
                }
                UpdateUI();
                m.Result = IntPtr.Zero;
            }

            base.WndProc(ref m);
        }

		#region Events

		public event EventHandler<KeyEventModArgs> KeyComboPressed;

		public event EventHandler Cancelled;

		#endregion

	}

}
