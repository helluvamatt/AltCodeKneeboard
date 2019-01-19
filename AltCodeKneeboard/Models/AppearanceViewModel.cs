using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AltCodeKneeboard.Models
{
    internal class AppearanceViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private PropertySort _SortMode = PropertySort.Categorized;
        public PropertySort SortMode
        {
            get => _SortMode;
            set
            {
                if (_SortMode != value)
                {
                    _SortMode = value;
                    OnPropertyChanged(nameof(SortMode));
                    OnPropertyChanged(nameof(SortCategory));
                    OnPropertyChanged(nameof(SortAlpha));
                }
            }
        }

        public bool SortCategory
        {
            get => SortMode == PropertySort.Categorized;
            set
            {
                if (value)
                {
                    SortMode = PropertySort.Categorized;
                }
            }
        }

        public bool SortAlpha
        {
            get => SortMode == PropertySort.Alphabetical;
            set
            {
                if (value)
                {
                    SortMode = PropertySort.Alphabetical;
                }
            }
        }
    }
}
