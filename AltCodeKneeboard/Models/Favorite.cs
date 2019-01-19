using System;
using System.ComponentModel;

namespace AltCodeKneeboard.Models
{
    internal class Favorite : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Favorite(AltCode code)
        {
            Code = code;
        }

        public AltCode Code { get; }

        public string Character => Char.ConvertFromUtf32(Code.Unicode);

        public string Description => Code.Description;

        private bool _IsFavorite;
        public bool IsFavorite
        {
            get => _IsFavorite;
            set
            {
                if (_IsFavorite != value)
                {
                    _IsFavorite = value;
                    OnPropertyChanged(nameof(IsFavorite));
                }
            }
        }
    }
}
