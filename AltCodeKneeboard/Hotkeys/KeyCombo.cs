using System;
using System.Text;
using System.Windows.Forms;
using R = AltCodeKneeboard.Properties.Resources;

namespace AltCodeKneeboard.Hotkeys
{
    /// <summary>
    /// The enumeration of possible modifiers.
    /// </summary>
    [Flags]
    public enum KeyModifier : uint
    {
        Alt = 1,
        Control = 2,
        Shift = 4,
        Win = 8
    }

    /// <summary>
    /// Single object type for passing a key combination
    /// </summary>
    public class KeyCombo
    {
        public Keys Key { get; set; }
        public KeyModifier Modifier { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (Modifier.HasFlag(KeyModifier.Control)) sb.Append(R.Ctrl);
            if (Modifier.HasFlag(KeyModifier.Shift))
            {
                if (sb.Length > 0) sb.Append(" + ");
                sb.Append(R.Shift);
            }
            if (Modifier.HasFlag(KeyModifier.Alt))
            {
                if (sb.Length > 0) sb.Append(" + ");
                sb.Append(R.Alt);
            }
            if (Modifier.HasFlag(KeyModifier.Win))
            {
                if (sb.Length > 0) sb.Append(" + ");
                sb.Append(R.Win);
            }
            if (sb.Length > 0) sb.Append(" + ");
            sb.Append(Key);
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            var other = obj as KeyCombo;
            return other != null && other.Key == Key && other.Modifier == Modifier;
        }

        public override int GetHashCode()
        {
            int hash = 23;
            hash = hash * 31 + Key.GetHashCode();
            hash = hash * 31 + Modifier.GetHashCode();
            return hash;
        }

        public ulong ToStorage()
        {
            return (((ulong)Modifier) << 32) + ((ulong)Key);
        }

        public static KeyCombo FromStorage(ulong val)
        {
            if (val == 0) return null;
            var key = (Keys)(val & 0xFFFFFFFF);
            var modifier = (KeyModifier)((val >> 32) & 0xFFFFFFFF);
            return new KeyCombo() { Key = key, Modifier = modifier };
        }
    }
}
