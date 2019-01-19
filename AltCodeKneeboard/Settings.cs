using System.ComponentModel;

namespace AltCodeKneeboard.Properties
{
    internal sealed partial class Settings
    {
        public Settings()
        {
            PropertyChanged += Settings_PropertyChanged;
        }

        private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Save();
        }
    }
}
