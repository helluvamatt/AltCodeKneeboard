using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AltCodeKneeboard.Hotkeys
{
	internal sealed class HotkeyManager : IDisposable
	{
		#region Private members

        private readonly Window _Window;

		#endregion

        public HotkeyManager()
        {
            _Window = new Window(OnPressedVKeysChanged);
        }

        public void AddHook(string id, string name, string description, bool enabled, KeyCombo initialKeyCombo, Action pressedCallback, Action releasedCallback, PropertyChangedEventHandler changeHandler)
        {
            if (id == null) throw new ArgumentNullException("id");
            var hook = new GlobalHotkey(id, name, description) { Enabled = enabled, Key = initialKeyCombo };
            hook.KeyPressed += (t, args) => { pressedCallback?.Invoke(); };
            hook.KeyReleased += (t, args) => { releasedCallback?.Invoke(); };
            if (changeHandler != null) hook.PropertyChanged += changeHandler;
            _Window.AddHook(hook);
        }

        public GlobalHotkey GetHook(string id) => _Window.GetHook(id);

        public IEnumerable<KeyValuePair<string, GlobalHotkey>> GetHooks() => _Window.GetHooks();

        public IEnumerable<ushort> PressedVKeys => _Window.PressedVKeys;

        public event EventHandler PressedVKeysChanged;

        private void OnPressedVKeysChanged()
        {
            PressedVKeysChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool Enabled
		{
            get => _Window.Enabled;
            set => _Window.Enabled = value;
		}

		#region IDisposable implementation

		public void Dispose()
		{
            if (_Window != null) _Window.Dispose();
		}

        #endregion

        #region Internal utility types

        /// <summary>
        /// Represents the window that is used internally to get the messages.
        /// </summary>
        private class Window : NativeWindow, IDisposable
        {
            private readonly Dictionary<string, GlobalHotkey> _Hooks = new Dictionary<string, GlobalHotkey>();
            private readonly HashSet<ushort> _PressedKeys = new HashSet<ushort>();
            private readonly Action _PressedChangeHandler;
            private GlobalHotkey _HotkeyPressed = null;

            public Window(Action pressedChangeHandler)
            {
                _PressedChangeHandler = pressedChangeHandler;

                // create the handle for the window.
                var cp = new CreateParams();
                CreateHandle(cp);

                RawInputDevice[] rawDevices =
                {
                    new RawInputDevice
                    {
                        UsagePage = 0x01, // From USB spec for HID keyboard
                        Usage = 0x06,     // From USB spec for HID keyboard
                        Flags = RIDEV_INPUTSINK,
                        Target = Handle
                    }
                };

                if (!RegisterRawInputDevices(rawDevices, 1, (uint)Marshal.SizeOf<RawInputDevice>()))
                {
                    int err = Marshal.GetLastWin32Error();
                    throw new Win32Exception(err, "Failed to register raw input device listener");
                }
            }

            public IEnumerable<ushort> PressedVKeys => _PressedKeys;

            public IEnumerable<KeyValuePair<string, GlobalHotkey>> GetHooks()
            {
                return _Hooks;
            }

            public GlobalHotkey GetHook(string id)
            {
                if (!_Hooks.ContainsKey(id)) throw new ArgumentException("Invalid hook with id = " + id);
                return _Hooks[id];
            }

            public void AddHook(GlobalHotkey hook)
            {
                _Hooks.Add(hook.ID, hook);
            }

            public bool Enabled { get; set; }

            /// <summary>
            /// Overridden to get the notifications.
            /// </summary>
            /// <param name="m"></param>
            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                if (m.Msg == WM_INPUT && Enabled)
                {
                    if (HandleRawInput(m.LParam))
                    {
                        m.Result = IntPtr.Zero;
                    }
                }
            }

            private GlobalHotkey FindHotkey()
            {
                foreach (var hotkey in _Hooks.Values)
                {
                    if (!hotkey.Enabled) continue;
                    if (hotkey.Key == null) continue;

                    // Check modifiers
                    if (hotkey.Key.Modifier.HasFlag(KeyModifier.Shift) && !_PressedKeys.Contains(VK_SHIFT) && !_PressedKeys.Contains(VK_LSHIFT) && !_PressedKeys.Contains(VK_RSHIFT)) continue;
                    if (hotkey.Key.Modifier.HasFlag(KeyModifier.Control) && !_PressedKeys.Contains(VK_CONTROL) && !_PressedKeys.Contains(VK_LCONTROL) && !_PressedKeys.Contains(VK_RCONTROL)) continue;
                    if (hotkey.Key.Modifier.HasFlag(KeyModifier.Alt) && !_PressedKeys.Contains(VK_MENU) && !_PressedKeys.Contains(VK_LMENU) && !_PressedKeys.Contains(VK_RMENU)) continue;
                    if (hotkey.Key.Modifier.HasFlag(KeyModifier.Win) && !_PressedKeys.Contains(VK_LWIN) && !_PressedKeys.Contains(VK_RWIN)) continue;

                    // Check key
                    ushort key = (ushort)((uint)hotkey.Key.Key & 0xFFFF);
                    if (_PressedKeys.Contains(key)) return hotkey;
                }

                return null;
            }

            private bool HandleRawInput(IntPtr ptr)
            {
                uint size = 0;
                uint hSize = (uint)Marshal.SizeOf<RawInputHeader>();
                GetRawInputData(ptr, RID_INPUT, IntPtr.Zero, ref size, hSize);
                RawInput input;
                int receivedBytes = GetRawInputData(ptr, RID_INPUT, out input, ref size, hSize);
                if (receivedBytes == size && input.Header.Type == RIM_TYPEKEYBOARD)
                {
                    if (CheckFlags(input.Keyboard.Flags, RI_KEY_BREAK))
                    {
                        // Key released
                        _PressedKeys.Remove(input.Keyboard.VKey);
                        _PressedChangeHandler?.Invoke();
                        if (_HotkeyPressed != null)
                        {
                            var pressedHotkey = FindHotkey();
                            if (pressedHotkey == null)
                            {
                                _HotkeyPressed.OnKeyReleased(this);
                                _HotkeyPressed = null;
                                return true;
                            }
                        }
                    }
                    else
                    {
                        // Key pressed
                        _PressedKeys.Add(input.Keyboard.VKey);
                        _PressedChangeHandler?.Invoke();
                        if (_HotkeyPressed == null)
                        {
                            _HotkeyPressed = FindHotkey();
                            if (_HotkeyPressed != null)
                            {
                                _HotkeyPressed.OnKeyPressed(this);
                                return true;
                            }
                        }
                    }
                }
                return false;
            }

            private bool CheckFlags(ushort flags, ushort mask) => (flags & mask) == mask;

            #region IDisposable Members

            public void Dispose()
            {
                DestroyHandle();
            }

            #endregion

            #region Win32 interop

            //private const int WM_HOTKEY = 0x0312;
            private const int WM_INPUT = 0x00FF;

            public const int RID_HEADER = 0x10000005;
            public const int RID_INPUT = 0x10000003;

            private const uint RIM_TYPEMOUSE = 0;
            private const uint RIM_TYPEKEYBOARD = 1;
            private const uint RIM_TYPEHID = 2;

            private const ushort RI_KEY_MAKE = 0;
            private const ushort RI_KEY_BREAK = 1;
            private const ushort RI_KEY_E0 = 2;
            private const ushort RI_KEY_E1 = 4;

            private const uint RIDEV_REMOVE = 0x1;
            private const uint RIDEV_EXCLUDE = 0x10;
            private const uint RIDEV_PAGEONLY = 0x20;
            private const uint RIDEV_NOLEGACY = 0x30;
            private const uint RIDEV_INPUTSINK = 0x100;
            private const uint RIDEV_CAPTUREMOUSE = 0x200;
            private const uint RIDEV_NOHOTKEYS = 0x200;
            private const uint RIDEV_APPKEYS = 0x400;
            private const uint RIDEV_EXINPUTSINK = 0x1000;
            private const uint RIDEV_DEVNOTIFY = 0x2000;

            private const ushort VK_SHIFT = 0x10;
            private const ushort VK_LSHIFT = 0xA0;
            private const ushort VK_RSHIFT = 0xA1;
            private const ushort VK_CONTROL = 0x11;
            private const ushort VK_LCONTROL = 0xA2;
            private const ushort VK_RCONTROL = 0xA3;
            private const ushort VK_MENU = 0x12;
            private const ushort VK_LMENU = 0xA4;
            private const ushort VK_RMENU = 0xA5;
            private const ushort VK_LWIN = 0x5B;
            private const ushort VK_RWIN = 0x5C;

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            internal struct RawInputDevice
            {
                public ushort UsagePage;
                public ushort Usage;
                public uint Flags;
                public IntPtr Target;
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            private struct RawKeyboard
            {
                public ushort MakeCode;
                public ushort Flags;
                public ushort Reserved;
                public ushort VKey;
                public uint Message;
                public uint ExtraInformation;
            }

            [StructLayout(LayoutKind.Explicit, Pack = 1)]
            private struct RawMouse
            {
                [MarshalAs(UnmanagedType.U2)]
                [FieldOffset(0)]
                public ushort Flags;
                [MarshalAs(UnmanagedType.U4)]
                [FieldOffset(4)]
                public uint ButtonsRaw;
                [FieldOffset(4)]
                public RawMouseButtons Buttons;
                [MarshalAs(UnmanagedType.U4)]
                [FieldOffset(8)]
                public uint RawButtons;
                [MarshalAs(UnmanagedType.U4)]
                [FieldOffset(12)]
                public int LastX;
                [MarshalAs(UnmanagedType.U4)]
                [FieldOffset(16)]
                public int LastY;
                [MarshalAs(UnmanagedType.U4)]
                [FieldOffset(20)]
                public uint ExtraInformation;
            }

            [StructLayout(LayoutKind.Explicit, Pack = 1)]
            private struct RawMouseButtons
            {
                [FieldOffset(0)]
                public ushort ButtonFlags;
                [FieldOffset(2)]
                public ushort ButtonData;
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            private struct RawHid
            {
                public uint SizeHid;
                public uint Count;
                public IntPtr RawData;
            }

            [StructLayout(LayoutKind.Explicit, Pack = 1)]
            private struct RawInput
            {
                [FieldOffset(0)]
                public RawInputHeader Header;
                [FieldOffset(16)]
                public RawMouse Mouse;
                [FieldOffset(16)]
                public RawKeyboard Keyboard;
                [FieldOffset(16)]
                public RawHid Hid;
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            private struct RawInputHeader
            {
                public uint Type;
                public uint Size;
                public IntPtr hDevice;
                public IntPtr wParam;
            }

            [DllImport("User32", SetLastError = true)]
            private static extern int GetRawInputData(IntPtr rawInput, uint command, IntPtr data, [In, Out] ref uint size, uint sizeHeader);

            [DllImport("User32", SetLastError = true)]
            private static extern int GetRawInputData(IntPtr rawInput, uint command, [Out] out RawInput data, [In, Out] ref uint size, uint sizeHeader);

            [DllImport("User32", SetLastError = true)]
            private static extern bool RegisterRawInputDevices([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] RawInputDevice[] devices, uint numDevices, uint size);

            [DllImport("User32", SetLastError = true)]
            private static extern int GetRawInputDeviceInfoA(IntPtr hDevice, uint uiCommand, IntPtr pData, [Out] out uint pcbSize);

            #endregion

        }

        #endregion
    }

    public class KeyEventModArgs : KeyEventArgs
    {
        public KeyEventModArgs(Keys key, KeyModifier modifier) : base(key)
        {
            ModifierKeys = modifier;
        }

        public KeyModifier ModifierKeys { get; set; }

        public KeyCombo KeyCombo
        {
            get
            {
                return new KeyCombo { Key = KeyCode, Modifier = ModifierKeys };
            }
        }
    }
}
