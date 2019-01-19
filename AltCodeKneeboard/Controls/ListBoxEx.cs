using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AltCodeKneeboard.Controls
{
    internal class ListBoxEx : ListBox
    {
        public event EventHandler<DrawItemEventArgs> DrawItemEx;

        protected virtual void OnDrawItemEx(DrawItemEventArgs e)
        {
            if (DrawItemEx != null)
            {
                DrawItemEx?.Invoke(this, e);
            }
            else
            {
                var listSource = DataSource as IList;
                if (listSource != null && e.Index > -1 && e.Index < listSource.Count)
                {
                    var str = GetItemText(listSource[e.Index]);
                    var textRect = TextRenderer.MeasureText(str, e.Font);
                    var x = e.Bounds.X + 1.0f;
                    var y = e.Bounds.Y + (e.Bounds.Height - textRect.Height) / 2.0f;
                    e.Graphics.DrawString(str, e.Font, new SolidBrush(e.ForeColor), x, y);
                }
            }
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            var currentContext = BufferedGraphicsManager.Current;
            var newBounds = new Rectangle(0, 0, e.Bounds.Width, e.Bounds.Height);
            using (var bufferedGraphics = currentContext.Allocate(e.Graphics, newBounds))
            {
                var newArgs = new DrawItemEventArgs(bufferedGraphics.Graphics, e.Font, newBounds, e.Index, e.State, e.ForeColor, e.BackColor);
                OnDrawItemEx(newArgs);
                base.OnDrawItem(newArgs);
                GDI.CopyGraphics(e.Graphics, e.Bounds, bufferedGraphics.Graphics, new Point(0, 0));
            }
        }

        private static class GDI
        {
            private const uint SRCCOPY = 0x00CC0020;

            [DllImport("gdi32.dll", CallingConvention = CallingConvention.StdCall)]
            private static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);

            public static void CopyGraphics(Graphics g, Rectangle bounds, Graphics bufferedGraphics, Point p)
            {
                IntPtr hdc1 = g.GetHdc();
                IntPtr hdc2 = bufferedGraphics.GetHdc();
                BitBlt(hdc1, bounds.X, bounds.Y, bounds.Width, bounds.Height, hdc2, p.X, p.Y, SRCCOPY);
                g.ReleaseHdc(hdc1);
                bufferedGraphics.ReleaseHdc(hdc2);
            }
        }
    }
}
