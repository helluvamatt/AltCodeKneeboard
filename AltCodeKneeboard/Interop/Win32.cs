using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AltCodeKneeboard.Interop
{
    [Flags]
    internal enum FlashWinInfoFlags : int
    {
        FLASHW_STOP = 0,
        FLASHW_CAPTION = 0x00000001,
        FLASHW_TRAY = 0x00000002,
        FLASHW_TIMER = 0x00000004,
        FLASHW_TIMERNOFG = 0x0000000C,
        FLASHW_ALL = FLASHW_CAPTION | FLASHW_TRAY
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct FlashWinInfo
    {
        public uint Size;
        public IntPtr Hwnd;
        public FlashWinInfoFlags Flags;
        public uint Count;
        public int Timeout;
    }

    internal static class Win32
    {
        #region Window flash

        [DllImport("User32", SetLastError = true)]
        public static extern bool FlashWindow(IntPtr hWnd, bool bInvert);

        [DllImport("User32", SetLastError = true)]
        public static extern bool FlashWindowEx(ref FlashWinInfo info);

        public static void FlashWindow(IntPtr handle, FlashWinInfoFlags flags, uint count, int timeout)
        {
            var flash = new FlashWinInfo
            {
                Size = (uint)Marshal.SizeOf<FlashWinInfo>(),
                Hwnd = handle,
                Flags = flags,
                Count = count,
                Timeout = timeout
            };
            FlashWindowEx(ref flash);
        }

        #endregion
    }
}
