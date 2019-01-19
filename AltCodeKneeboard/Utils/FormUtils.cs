using AltCodeKneeboard.Interop;
using Svg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace AltCodeKneeboard.Utils
{
    internal static class FormUtils
    {
        public static void BringToFront(this Form form, bool flash)
        {
            // get our current "TopMost" value
            bool top = form.TopMost;
            // make our form jump to the top of everything
            form.TopMost = true;
            // set it back to whatever it was
            form.TopMost = top;

            if (form.IsHandleCreated && flash) Win32.FlashWindow(form.Handle, FlashWinInfoFlags.FLASHW_ALL, 5, 100);
        }

        #region SVG

        public static void DrawSvg(this Graphics g, byte[] iconData, Color foreColor, Rectangle bounds)
        {
            using (var stream = new MemoryStream(iconData))
            {
                var svg = SvgDocument.Open<SvgDocument>(stream);
                svg.Fill = new SingleColorPaintServer(foreColor);
                svg.X = new SvgUnit(SvgUnitType.Pixel, bounds.X);
                svg.Y = new SvgUnit(SvgUnitType.Pixel, bounds.Y);
                svg.Width = new SvgUnit(SvgUnitType.Pixel, bounds.Width);
                svg.Height = new SvgUnit(SvgUnitType.Pixel, bounds.Height);
                svg.Draw(g);
            }
        }

        public static Bitmap RenderSvg(byte[] iconData, Color foreColor, Size size)
        {
            var bm = new Bitmap(size.Width, size.Height);
            using (var g = Graphics.FromImage(bm))
            {
                g.DrawSvg(iconData, foreColor, new Rectangle(new Point(0, 0), size));
            }
            return bm;
        }

        #endregion

        public static void DrawString(this Graphics g, string text, Font font, Color color, RectangleF bounds, HorizontalAlignment hAlign, StringAlignment vAlign)
        {
            DrawString(g, text, font, new SolidBrush(color), bounds, hAlign, vAlign);
        }

        public static void DrawString(this Graphics g, string text, Font font, Brush brush, RectangleF bounds, HorizontalAlignment hAlign, StringAlignment vAlign)
        {
            var format = new StringFormat();
            format.Trimming = StringTrimming.EllipsisCharacter;
            switch (hAlign)
            {
                case HorizontalAlignment.Left: format.Alignment = StringAlignment.Near; break;
                case HorizontalAlignment.Center: format.Alignment = StringAlignment.Center; break;
                case HorizontalAlignment.Right: format.Alignment = StringAlignment.Far; break;
            }
            format.LineAlignment = vAlign;
            g.DrawString(text, font, brush, bounds, format);
        }

        public static string ToCSS(this Color color)
        {
            return $"rgba({color.R}, {color.G}, {color.B}, {color.A / 255f})";
        }

        public static string GetApplicationDirectory()
        {
            var codeBase = new Uri(Assembly.GetEntryAssembly().CodeBase);
            if (codeBase.Scheme == "file") return Path.GetDirectoryName(Uri.UnescapeDataString(codeBase.AbsolutePath));
            return null;
        }

        public static GraphicsPath RoundedRectangle(RectangleF bounds, int radius)
        {
            return RoundedRectangle(bounds, radius, radius, radius, radius);
        }

        public static GraphicsPath RoundedRectangle(RectangleF bounds, int radiusTopLeft, int radiusTopRight, int radiusBottomLeft, int radiusBottomRight)
        {
            GraphicsPath path = new GraphicsPath();

            // Short-circuit for no rounding
            if (radiusTopLeft == 0 && radiusTopRight == 0 && radiusBottomLeft == 0 && radiusBottomRight == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc
            if (radiusTopLeft > 0)
            {
                path.AddArc(new RectangleF(bounds.Left, bounds.Top, radiusTopLeft * 2, radiusTopLeft * 2), 180, 90);
            }
            else
            {
                path.AddLine(new PointF(bounds.Left, bounds.Top), new PointF(bounds.Left, bounds.Top));
            }

            // top right arc
            if (radiusTopRight > 0)
            {
                path.AddArc(new RectangleF(bounds.Right - radiusTopRight * 2, bounds.Top, radiusTopRight * 2, radiusTopRight * 2), 270, 90);
            }
            else
            {
                path.AddLine(new PointF(bounds.Right, bounds.Top), new PointF(bounds.Right, bounds.Top));
            }

            // bottom right arc
            if (radiusBottomRight > 0)
            {
                path.AddArc(new RectangleF(bounds.Right - radiusBottomRight * 2, bounds.Bottom - radiusBottomRight * 2, radiusBottomRight * 2, radiusBottomRight * 2), 0, 90);
            }
            else
            {
                path.AddLine(new PointF(bounds.Right, bounds.Bottom), new PointF(bounds.Right, bounds.Bottom));
            }

            // bottom left arc
            if (radiusBottomLeft > 0)
            {
                path.AddArc(new RectangleF(bounds.Left, bounds.Bottom - radiusBottomLeft * 2, radiusBottomLeft * 2, radiusBottomLeft * 2), 90, 90);
            }
            else
            {
                path.AddLine(new PointF(bounds.Left, bounds.Bottom), new PointF(bounds.Left, bounds.Bottom));
            }

            path.CloseFigure();
            return path;
        }

        public static IList<EnumListItem> GetEnumList(Type enumType)
        {
            if (!enumType.IsEnum) throw new ArgumentException("Type must be an enum type.");
            return Enum.GetValues(enumType).Cast<object>().Select(v => new EnumListItem(v)).ToList();
        }
    }

    internal class EnumListItem
    {
        public EnumListItem(object value)
        {
            Value = value;
            var enumType = value.GetType();
            var name = Enum.GetName(enumType, value);
            var member = enumType.GetMember(name).First();
            var displayNameAttr = member.GetCustomAttribute<DisplayNameAttribute>();
            var descriptionAttr = member.GetCustomAttribute<DescriptionAttribute>();
            if (displayNameAttr != null) Text = displayNameAttr.DisplayName;
            else if (descriptionAttr != null) Text = descriptionAttr.Description;
            else Text = name;
        }

        public string Text { get; }

        public object Value { get; }
    }
}
