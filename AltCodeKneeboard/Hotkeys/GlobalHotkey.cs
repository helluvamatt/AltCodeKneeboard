using System;
using System.ComponentModel;

namespace AltCodeKneeboard.Hotkeys
{
    public class GlobalHotkey : INotifyPropertyChanged
    {
        public GlobalHotkey(string id, string name, string description)
        {
            ID = id;
            Name = name;
            Description = description;
        }

        public string ID { get; }
        public string Name { get; }
        public string Description { get; }

        private KeyCombo _Key;
        public KeyCombo Key
        {
            get => _Key;
            set
            {
                if (_Key != value)
                {
                    _Key = value;
                    OnPropertyChanged(nameof(Key));
                }
            }
        }

        private bool _Enabled;
        public bool Enabled
        {
            get => _Enabled;
            set
            {
                if (_Enabled != value)
                {
                    _Enabled = value;
                    OnPropertyChanged(nameof(Enabled));
                }
            }
        }

        /// <summary>
        /// A hot key has been pressed.
        /// </summary>
        public event EventHandler<KeyEventModArgs> KeyPressed;
        public void OnKeyPressed(object sender)
        {
            KeyPressed?.Invoke(sender, new KeyEventModArgs(Key.Key, Key.Modifier));
        }

        /// <summary>
        /// A hot key has been released.
        /// </summary>
        public event EventHandler<KeyEventModArgs> KeyReleased;
        public void OnKeyReleased(object sender)
        {
            KeyReleased?.Invoke(sender, new KeyEventModArgs(Key.Key, Key.Modifier));
        }

        /// <summary>
        /// A property on the model has changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
