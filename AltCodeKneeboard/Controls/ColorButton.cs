using System.Drawing;
using System.Windows.Forms;

namespace AltCodeKneeboard.Controls
{
    internal class ColorButton : Button
    {
        public ColorButton()
        {
            SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var rect = new Rectangle(Padding.Left, Padding.Top, Width - (Padding.Horizontal + 1), Height - (Padding.Vertical + 1));
            e.Graphics.FillRectangle(new SolidBrush(ForeColor), rect);
            e.Graphics.DrawRectangle(new Pen(SystemBrushes.ControlText), rect);
        }
    }
}
