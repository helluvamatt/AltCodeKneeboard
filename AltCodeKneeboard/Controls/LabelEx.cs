using AltCodeKneeboard.Utils;
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
    internal class LabelEx : Label
    {
        private Size _CachedTextSize;
        private float _CachedIconCharSize;

        #region Icon property

        private byte[] _Icon;
        [Category("Appearance")]
        public byte[] Icon
        {
            get => _Icon;
            set
            {
                if (_Icon != value)
                {
                    _Icon = value;
                    OnIconChanged();
                }
            }
        }

        public event EventHandler IconChanged;

        protected void OnIconChanged()
        {
            Invalidate();
            IconChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region IconChar property

        private char _IconChar;
        [Category("Appearance")]
        public char IconChar
        {
            get => _IconChar;
            set
            {
                _IconChar = value;
                OnIconCharChanged();
            }
        }

        public event EventHandler IconCharChanged;

        protected void OnIconCharChanged()
        {
            _CachedIconCharSize = ComputeIconCharSize();
            Invalidate();
            IconCharChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region IconSize property

        private Size _IconSize;
        [Category("Layout")]
        public Size IconSize
        {
            get => _IconSize;
            set
            {
                if (_IconSize != value)
                {
                    _IconSize = value;
                    OnIconSizeChanged();
                }
            }
        }

        public event EventHandler IconSizeChanged;

        protected void OnIconSizeChanged()
        {
            _CachedIconCharSize = ComputeIconCharSize();
            PerformLayout();
            Invalidate();
            IconSizeChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region IconAlignment property

        private ContentAlignment _IconAlignment = ContentAlignment.TopLeft;
        [Category("Layout")]
        public ContentAlignment IconAlignment
        {
            get => _IconAlignment;
            set
            {
                if (_IconAlignment != value)
                {
                    _IconAlignment = value;
                    OnIconAlignmentChanged();
                }
            }
        }

        public event EventHandler IconAlignmentChanged;

        protected void OnIconAlignmentChanged()
        {
            PerformLayout();
            Invalidate();
            IconAlignmentChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region IconMargin property

        private Padding _IconMargin;
        [Category("Layout")]
        public Padding IconMargin
        {
            get => _IconMargin;
            set
            {
                if (_IconMargin != value)
                {
                    _IconMargin = value;
                    OnIconMarginChanged();
                }
            }
        }

        public event EventHandler IconMarginChanged;

        protected void OnIconMarginChanged()
        {
            PerformLayout();
            Invalidate();
            IconMarginChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        public override Size GetPreferredSize(Size proposedSize)
        {
            if (_CachedTextSize.IsEmpty) _CachedTextSize = ComputeTextSize();
            if (AutoSize)
            {
                var iconSize = new Size(IconSize.Width + IconMargin.Horizontal, IconSize.Height + IconMargin.Vertical);

                int height;
                switch (IconAlignment)
                {
                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.MiddleCenter:
                    case ContentAlignment.MiddleRight:
                        height = Math.Max(_CachedTextSize.Height, iconSize.Height);
                        break;
                    default:
                        height = _CachedTextSize.Height + iconSize.Height;
                        break;
                }

                int width;
                switch (IconAlignment)
                {
                    case ContentAlignment.TopCenter:
                    case ContentAlignment.MiddleCenter:
                    case ContentAlignment.BottomCenter:
                        width = Math.Max(_CachedTextSize.Width, iconSize.Width);
                        break;
                    default:
                        width = _CachedTextSize.Width + iconSize.Width;
                        break;
                }

                if (!MaximumSize.IsEmpty)
                {
                    width = Math.Min(MaximumSize.Width, width);
                    height = Math.Min(MaximumSize.Height, height);
                }
                if (!MinimumSize.IsEmpty)
                {
                    width = Math.Max(MinimumSize.Width, width);
                    height = Math.Max(MaximumSize.Height, height);
                }

                return new Size(width + Padding.Horizontal, height + Padding.Vertical);
            }
            return base.GetPreferredSize(proposedSize);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_CachedTextSize.IsEmpty) _CachedTextSize = ComputeTextSize();

            int iconX = 0;
            int iconY = 0;
            int textX = 0;
            int textY = 0;
            int textW = AutoSize ? _CachedTextSize.Width : Width - IconSize.Width;
            int textH = AutoSize ? _CachedTextSize.Height : Height - IconSize.Height;

            // Calculate X-coordinates
            switch (IconAlignment)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                    textX = Width - textW;
                    break;
                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                    textX = (Width - textW) / 2;
                    iconX = (Width - IconSize.Width) / 2;
                    break;
                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    iconX = (Width - IconSize.Width);
                    break;
            }

            // Calculate Y-coordinates
            switch (IconAlignment)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopCenter:
                case ContentAlignment.TopRight:
                    textY = Height - textH;
                    break;
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleRight:
                    textY = (Height - textH) / 2;
                    iconY = (Height - IconSize.Height) / 2;
                    break;
                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomRight:
                    iconY = Height - IconSize.Height;
                    break;
            }

            var iconRect = new Rectangle(new Point(iconX, iconY), IconSize);
            if (Icon != null)
            {
                e.Graphics.DrawSvg(Icon, ForeColor, iconRect);
            }
            else if (IconChar != '\0')
            {
                var f = new Font(Font.FontFamily, _CachedIconCharSize, GraphicsUnit.Point);
                var iconTextRect = TextRenderer.MeasureText(IconChar.ToString(), f);
                var iconCharX = iconRect.X + (iconRect.Width - iconTextRect.Width) / 2;
                var iconCharY = iconRect.Y + (iconRect.Height - iconTextRect.Height) / 2;
                e.Graphics.DrawString(IconChar.ToString(), f, new SolidBrush(ForeColor), iconCharX, iconCharY);
            }

            e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), new RectangleF(textX, textY, textW, textH));
        }

        protected override void OnTextChanged(EventArgs e)
        {
            _CachedTextSize = Size.Empty;
            base.OnTextChanged(e);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            _CachedTextSize = Size.Empty;
            base.OnFontChanged(e);
        }

        private Size ComputeTextSize()
        {
            var flags = TextFormatFlags.Default;
            if (AutoEllipsis) flags |= TextFormatFlags.EndEllipsis;
            return TextRenderer.MeasureText(Text, Font, Size.Empty, flags);
        }

        private float ComputeIconCharSize()
        {
            var height = IconSize.Height * 0.99f;
            var square = TextRenderer.MeasureText("M", Font);
            return Font.SizeInPoints * (height / square.Height);
        }
    }
}
