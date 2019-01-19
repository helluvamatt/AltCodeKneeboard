using AltCodeKneeboard.Kneeboard;
using AltCodeKneeboard.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AltCodeKneeboard.Controls
{
    internal class AppearancePreviewControl : Control
    {
        private readonly KneeboardLayout _LayoutManager;

        public AppearancePreviewControl(KneeboardLayout layoutManager, KneeboardTheme theme)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            _LayoutManager = layoutManager ?? throw new ArgumentNullException("layoutManager");
            Theme = theme ?? throw new ArgumentNullException("theme");
        }

        public KneeboardTheme Theme
        {
            get => _LayoutManager.Theme;
            set
            {
                if (_LayoutManager.Theme != value)
                {
                    _LayoutManager.SetTheme(value, HandleThemeChange);
                    Invalidate();
                }
            }
        }

        private PreviewMode _PreviewMode;
        public PreviewMode PreviewMode
        {
            get => _PreviewMode;
            set
            {
                if (_PreviewMode != value)
                {
                    _PreviewMode = value;
                    Invalidate();
                }
            }
        }

        private void HandleThemeChange(object sender, PropertyChangedEventArgs args)
        {
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            AltCode hovered;
            if (_LayoutManager.Hover(new Point(e.X - 10, e.Y - 10), out hovered))
            {
                Invalidate();
            }
            Cursor = hovered != null ? Cursors.Hand : Cursors.Default;

            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (_LayoutManager.ClearHover())
            {
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var imSize = new Size(Width - 20, Height - 20);
            _LayoutManager.Layout(imSize, true);
            e.Graphics.FillRectangle(new SolidBrush(_LayoutManager.Theme.DefaultBackColor), new Rectangle(new Point(10, 10), imSize));

            e.Graphics.TranslateTransform(10, 10);
            e.Graphics.SetClip(new Rectangle(new Point(0, 0), imSize));

            _LayoutManager.Render(e.Graphics);
        }

    }

    internal enum PreviewMode
    {
        Default, Headers, Tiles, Scrollbar
    }
}
