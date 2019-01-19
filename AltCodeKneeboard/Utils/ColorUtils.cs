using System;
using System.Drawing;
using System.Globalization;
using System.Linq;

namespace AltCodeKneeboard.Utils
{
    internal static class ColorUtils
    {
        public static void ToHSV(this Color color, out double hue, out double saturation, out double value)
        {
            int max = new int[] { color.R, color.G, color.B }.Max();
            int min = new int[] { color.R, color.G, color.B }.Min();
            hue = color.GetHue();
            saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            value = max / 255d;
        }

        public static Color ColorFromHSV(double hue, double saturation, double value, byte alpha = 0xFF)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(alpha, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(alpha, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(alpha, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(alpha, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(alpha, t, p, v);
            else
                return Color.FromArgb(alpha, v, p, q);
        }

        public static Color ParseHex(string hex)
        {
            hex = hex.TrimStart('#');
            if (hex.Length == 8) // Long hex form with alpha
            {
                return Color.FromArgb(
                    int.Parse(hex.Substring(0, 2), NumberStyles.HexNumber),
                    int.Parse(hex.Substring(2, 2), NumberStyles.HexNumber),
                    int.Parse(hex.Substring(4, 2), NumberStyles.HexNumber),
                    int.Parse(hex.Substring(6, 2), NumberStyles.HexNumber));
            }
            else if (hex.Length == 6) // Long hex form without alpha
            {
                return Color.FromArgb(
                    0xFF, // Opaque
                    int.Parse(hex.Substring(0, 2), NumberStyles.HexNumber),
                    int.Parse(hex.Substring(2, 2), NumberStyles.HexNumber),
                    int.Parse(hex.Substring(4, 2), NumberStyles.HexNumber));
            }
            else if (hex.Length == 4) // Short hex form with alpha
            {
                return Color.FromArgb(
                    int.Parse(hex.Substring(0, 1) + hex.Substring(0, 1), NumberStyles.HexNumber),
                    int.Parse(hex.Substring(1, 1) + hex.Substring(1, 1), NumberStyles.HexNumber),
                    int.Parse(hex.Substring(2, 1) + hex.Substring(2, 1), NumberStyles.HexNumber),
                    int.Parse(hex.Substring(3, 1) + hex.Substring(2, 1), NumberStyles.HexNumber));
            }
            else if (hex.Length == 3) // Short hex form without alpha
            {
                return Color.FromArgb(
                    0xFF, // Opaque
                    int.Parse(hex.Substring(0, 1) + hex.Substring(0, 1), NumberStyles.HexNumber),
                    int.Parse(hex.Substring(1, 1) + hex.Substring(1, 1), NumberStyles.HexNumber),
                    int.Parse(hex.Substring(2, 1) + hex.Substring(2, 1), NumberStyles.HexNumber));
            }
            else
            {
                return Color.Black; // fallback: #000000
            }
        }
    }
}
