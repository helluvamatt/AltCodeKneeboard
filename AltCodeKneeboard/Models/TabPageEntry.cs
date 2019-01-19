namespace AltCodeKneeboard.Models
{
    internal class TabPageEntry
    {
        public TabPageEntry(byte[] icon, string text)
        {
            Icon = icon;
            Text = text;
        }

        public byte[] Icon { get; }
        public string Text { get; }
    }
}
