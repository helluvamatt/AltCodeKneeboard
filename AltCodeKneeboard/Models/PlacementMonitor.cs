using System.Drawing;
using System.Windows.Forms;

namespace AltCodeKneeboard.Models
{
    public class PlacementMonitor
    {
        private readonly int _Index;
        private readonly Screen _Screen;

        public PlacementMonitor(int index, Screen screen)
        {
            _Index = index;
            _Screen = screen;
        }

        public Rectangle Bounds => _Screen.WorkingArea;
        
        public override string ToString()
        {
            return string.Format("Monitor {0} ({1}, {2}){3}", _Index + 1, _Screen.Bounds.Size.Width, _Screen.Bounds.Size.Height, _Screen.Primary ? " (Primary)" : string.Empty);
        }
    }
}
