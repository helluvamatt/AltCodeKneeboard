using System;
using Settings = AltCodeKneeboard.Properties.Settings;

namespace AltCodeKneeboard.Models
{
    internal class PlacementViewModel
    {
        public decimal X
        {
            get => Settings.Default.StartPosition.X;
            set => Settings.Default.StartPosition = new System.Drawing.Point(Convert.ToInt32(value), Settings.Default.StartPosition.Y);
        }

        public decimal Y
        {
            get => Settings.Default.StartPosition.Y;
            set => Settings.Default.StartPosition = new System.Drawing.Point(Settings.Default.StartPosition.X, Convert.ToInt32(value));
        }

        public decimal Width
        {
            get => Settings.Default.Size.Width;
            set => Settings.Default.Size = new System.Drawing.Size(Convert.ToInt32(value), Settings.Default.Size.Height);
        }

        public decimal Height
        {
            get => Settings.Default.Size.Height;
            set => Settings.Default.Size = new System.Drawing.Size(Settings.Default.Size.Width, Convert.ToInt32(value));
        }
    }
}
