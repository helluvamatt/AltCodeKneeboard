using System.ComponentModel;

namespace AltCodeKneeboard.Models
{
    internal class GroupVisible : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GroupVisible(Group group, bool initialChecked)
        {
            Group = group;
            _Checked = initialChecked;
        }

        private bool _Checked;
        public bool Checked
        {
            get => _Checked;
            set
            {
                if (_Checked != value)
                {
                    _Checked = value;
                    OnPropertyChanged(nameof(Checked));
                }
            }
        }

        public Group Group { get; }

        public string Text => Group.Name;
    }
}
