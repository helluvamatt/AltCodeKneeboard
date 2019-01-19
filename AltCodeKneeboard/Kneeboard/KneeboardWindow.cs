using AltCodeKneeboard.Models;
using AltCodeKneeboard.Utils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using R = AltCodeKneeboard.Properties.Resources;
using Settings = AltCodeKneeboard.Properties.Settings;

namespace AltCodeKneeboard.Kneeboard
{
    internal class KneeboardWindow : NativeWindowEx
    {
        private readonly KneeboardLayout _LayoutManager;
        
        private int _HoveredDragRegion;

        public KneeboardWindow(AltCodeData altCodeData, KneeboardTheme style)
        {
            _LayoutManager = new KneeboardLayout(altCodeData, Settings.Default.Favorites, Settings.Default.HiddenGroups);

            TopMost = true;
            Theme = style;

            Settings.Default.PropertyChanged += Settings_PropertyChanged;
            
        }

        #region Properties

        public KneeboardTheme Theme
        {
            get => _LayoutManager.Theme;
            set
            {
                if (_LayoutManager.SetTheme(value, ThemePropertyChanged))
                {
                    _LayoutManager.Layout(Bounds.Size);
                    Invalidate();
                }
            }
        }

        private bool _IsPlacementMode;
        public bool IsPlacementMode
        {
            get => _IsPlacementMode;
            set
            {
                if (_IsPlacementMode != value)
                {
                    _IsPlacementMode = value;
                    Invalidate();
                }
            }
        }

        #endregion

        #region Form overrides

        #region Mouse handling

        protected override void OnMouseClick(MouseButtons buttons, Point location)
        {
            if (!IsPlacementMode)
            {
                var cell = _LayoutManager.FindAtPoint(location);
                if (cell != null)
                {
                    // TODO Send keystroke, could see how KeePass AutoType works
                }
            }
        }

        protected override void OnMouseDown(MouseButtons buttons, Point location)
        {
            if (IsPlacementMode && _HoveredDragRegion > HitTest.HTNOWHERE && buttons == MouseButtons.Left)
            {
                Win32Interop.ReleaseCapture();
                Win32Interop.SendMessage(Handle, WM.NCLBUTTONDOWN, new IntPtr(_HoveredDragRegion), IntPtr.Zero);
            }
        }

        protected override void OnMouseEnter()
        {
            Invalidate();
        }

        protected override void OnMouseMove(Point location)
        {
            if (IsPlacementMode)
            {
                _HoveredDragRegion = GetHitTest(location, new int[] { HitTest.HTCAPTION, HitTest.HTTOP, HitTest.HTTOPRIGHT, HitTest.HTRIGHT, HitTest.HTBOTTOMRIGHT, HitTest.HTBOTTOM, HitTest.HTBOTTOMLEFT, HitTest.HTLEFT, HitTest.HTTOPLEFT });
                switch (_HoveredDragRegion)
                {
                    case HitTest.HTTOP:
                    case HitTest.HTBOTTOM:
                        Cursor = Cursors.SizeNS;
                        break;
                    case HitTest.HTLEFT:
                    case HitTest.HTRIGHT:
                        Cursor = Cursors.SizeWE;
                        break;
                    case HitTest.HTTOPRIGHT:
                    case HitTest.HTBOTTOMLEFT:
                        Cursor = Cursors.SizeNESW;
                        break;
                    case HitTest.HTTOPLEFT:
                    case HitTest.HTBOTTOMRIGHT:
                        Cursor = Cursors.SizeNWSE;
                        break;
                    case HitTest.HTCAPTION:
                        Cursor = Cursors.SizeAll;
                        break;
                    default:
                        Cursor = Cursors.Default;
                        break;
                }
                Invalidate();
            }
            else
            {
                AltCode hovered;
                if (_LayoutManager.Hover(location, out hovered))
                {
                    Invalidate();
                }
                Cursor = hovered != null ? Cursors.Hand : Cursors.Default;
            }
        }

        protected override void OnMouseLeave()
        {
            if (IsPlacementMode)
            {
                _HoveredDragRegion = HitTest.HTNOWHERE;
            }
            else _LayoutManager.ClearHover();
            Invalidate();
        }

        protected override void OnMouseWheel(int delta, Point location)
        {
            if (!IsPlacementMode && _LayoutManager.Scroll(delta / SystemInformation.MouseWheelScrollDelta * 30))
            {
                Invalidate();
            }
        }

        #endregion

        #region Key handling

        //protected override bool OnKeyDown(Keys keys)
        //{
        //    if (!IsPlacementMode)
        //    {
        //        switch (keys)
        //        {
        //            case Keys.Down:
        //                if (_LayoutManager.Scroll(15)) Invalidate();
        //                break;
        //            case Keys.Up:
        //                if (_LayoutManager.Scroll(-15)) Invalidate();
        //                break;
        //        }
        //    }
        //    return true;
        //}

        //protected override bool OnKeyPress(char c)
        //{
        //    // TODO Handle numbers for searching alt codes?
        //    return true;
        //}

        #endregion

        #region Layout / Rendering

        protected override void OnVisibleChanged()
        {
            if (Visible)
            {
                Bounds = new Rectangle(Settings.Default.StartPosition, Settings.Default.Size);
                _LayoutManager.Layout(Bounds.Size);
                Invalidate();
            }
        }

        protected override void OnResizeEnd()
        {
            Settings.Default.Size = Bounds.Size;
            base.OnResizeEnd();
        }

        protected override void OnResize()
        {
            _LayoutManager.Layout(Bounds.Size);
            Invalidate();
            base.OnResize();
        }

        protected override void OnMove()
        {
            _LayoutManager.Layout(Bounds.Size);
            Invalidate();
            Settings.Default.StartPosition = Location;
            base.OnMove();
        }

        protected override bool OnClosing(CloseReason reason)
        {
            return reason == CloseReason.UserClosing;
        }

        protected override void OnPaint(Graphics g)
        {
            var bounds = new Rectangle(new Point(0, 0), Bounds.Size);

            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            // Draw semi-transparent background, since the window will be created with no painting
            g.FillRectangle(new SolidBrush(Theme.DefaultBackColor), bounds);

            _LayoutManager.Render(g);

            if (IsPlacementMode)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(0xDD, Color.Black)), bounds);
                g.DrawRectangle(new Pen(Color.Gray, 10), 5, 5, bounds.Width - 10, bounds.Height - 10);

                if (_HoveredDragRegion > HitTest.HTNOWHERE && _HoveredDragRegion != HitTest.HTCAPTION)
                {
                    var rect = GetDragTypeRect(_HoveredDragRegion);
                    if (!rect.IsEmpty) g.FillRectangle(new SolidBrush(Color.White), rect);
                }

                var textFont = new Font(Theme.DefaultFont.FontFamily, 16, FontStyle.Regular, GraphicsUnit.Point);
                g.DrawString(R.PlacementMessage, textFont, Color.White, new RectangleF(10, 10, bounds.Width - 20, bounds.Height - 20), HorizontalAlignment.Center, StringAlignment.Center);
            }
            else if (IsMouseOver && _LayoutManager.HasScrollbar() && Theme.ScrollbarWidth > 0)
            {
                var scrollRect = GetDragTypeRect(HitTest.HTVSCROLL);
                var thumbRect = _LayoutManager.GetScrollThumbRect(scrollRect);
                using (var path = FormUtils.RoundedRectangle(thumbRect, (int)(scrollRect.Width / 2)))
                {
                    g.FillPath(new SolidBrush(Color.FromArgb(0xCC, Theme.ScrollbarForeColor)), path);
                }
            }
        }

        protected override bool PreventActivation => true;

        #endregion

        #endregion

        #region Private members

        #region Event handlers

        private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (nameof(Settings.Favorites) == e.PropertyName || nameof(Settings.HiddenGroups) == e.PropertyName)
            {
                if (_LayoutManager.SetFavorites(Settings.Default.Favorites))
                {
                    _LayoutManager.Layout(Bounds.Size);
                    Invalidate();
                }
            }
            if (nameof(Settings.HiddenGroups) == e.PropertyName)
            {
                if (_LayoutManager.SetHiddenGroups(Settings.Default.HiddenGroups))
                {
                    _LayoutManager.Layout(Bounds.Size);
                    Invalidate();
                }
            }
            if (nameof(Settings.StartPosition) == e.PropertyName || nameof(Settings.Size) == e.PropertyName)
            {
                Bounds = new Rectangle(Settings.Default.StartPosition, Settings.Default.Size);
                _LayoutManager.Layout(Bounds.Size);
                Invalidate();
            }
            if (nameof(Settings.SortMode) == e.PropertyName)
            {
                if (_LayoutManager.SetSortMode(Settings.Default.SortMode))
                {
                    _LayoutManager.Layout(Bounds.Size);
                    Invalidate();
                }
            }
        }

        private void ThemePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            _LayoutManager.Layout(Bounds.Size);
            Invalidate();
        }

        #endregion

        #region Layout

        private int GetHitTest(Point loc, int[] hitTypes)
        {
            foreach (var ht in hitTypes)
            {
                var rect = GetDragTypeRect(ht);
                if (!rect.IsEmpty && rect.Contains(loc))
                {
                    return ht;
                }
            }
            return HitTest.HTNOWHERE;
        }

        private RectangleF GetDragTypeRect(int ht)
        {
            switch (ht)
            {
                case HitTest.HTTOP:
                    return new Rectangle(10, 0, Bounds.Width - 20, 10);
                case HitTest.HTTOPRIGHT:
                    return new Rectangle(Bounds.Width - 10, 0, 10, 10);
                case HitTest.HTRIGHT:
                    return new Rectangle(Bounds.Width - 10, 10, 10, Bounds.Height - 20);
                case HitTest.HTBOTTOMRIGHT:
                    return new Rectangle(Bounds.Width - 10, Bounds.Height - 10, 10, 10);
                case HitTest.HTBOTTOM:
                    return new Rectangle(10, Bounds.Height - 10, Bounds.Width - 20, 10);
                case HitTest.HTBOTTOMLEFT:
                    return new Rectangle(0, Bounds.Height - 10, 10, 10);
                case HitTest.HTLEFT:
                    return new Rectangle(0, 10, 10, Bounds.Height - 20);
                case HitTest.HTTOPLEFT:
                    return new Rectangle(0, 0, 10, 10);
                case HitTest.HTCAPTION:
                    return new Rectangle(10, 10, Bounds.Width - 20, Bounds.Height - 20);
                case HitTest.HTVSCROLL:
                    return new RectangleF(Bounds.Width - Theme.ScrollbarWidth, 0, Theme.ScrollbarWidth, Bounds.Height);
                default:
                    return Rectangle.Empty;
            }
        }

        #endregion

        #endregion
    }
}
