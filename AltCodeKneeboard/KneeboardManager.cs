using AltCodeKneeboard.Kneeboard;
using AltCodeKneeboard.Models;
using System.Drawing;
using System.Windows.Forms;

namespace AltCodeKneeboard
{
    internal class KneeboardManager
    {
        private readonly KneeboardWindow _Kneeboard;
        private readonly AltCodeData _AltCodes;

        public KneeboardManager(AltCodeData altCodeData)
        {
            _AltCodes = altCodeData;
            _Kneeboard = new KneeboardWindow(_AltCodes, new KneeboardTheme());
        }

        #region Placement mode

        private bool _KneeboardWasShowing;
        
        public bool IsPlacementMode
        {
            get => _Kneeboard.IsPlacementMode;
            set
            {
                if (IsPlacementMode != value)
                {
                    _Kneeboard.IsPlacementMode = value;
                    if (IsPlacementMode)
                    {
                        _KneeboardWasShowing = KneeboardVisible;
                        _Kneeboard.Visible = true;
                    }
                    else if (!_KneeboardWasShowing)
                    {
                        _Kneeboard.Visible = false;
                    }
                }
            }
        }

        #endregion

        public bool KneeboardVisible => _Kneeboard.Visible;

        public Screen CurrentScreen => Screen.FromHandle(_Kneeboard.Handle);
        public Rectangle CurrentBounds => _Kneeboard.Bounds;

        public KneeboardTheme Theme
        {
            get => _Kneeboard.Theme;
            set => _Kneeboard.Theme = value;
        }

        public void SetKneeboardBounds(int x, int y, int w, int h)
        {
            _Kneeboard.Bounds = new Rectangle(x, y, w, h);
        }

        public void ShowKneeboard()
        {
            _Kneeboard.Visible = _KneeboardWasShowing = true;
        }

        public void HideKneeboard()
        {
            if (!IsPlacementMode) _Kneeboard.Visible = false;
            else _KneeboardWasShowing = false;
        }

        public void ToggleKneeboard()
        {
            if (IsPlacementMode)
            {
                _KneeboardWasShowing = !_KneeboardWasShowing;
            }
            else
            {
                if (_Kneeboard.Visible) HideKneeboard();
                else ShowKneeboard();
            }
        }
    }
}
