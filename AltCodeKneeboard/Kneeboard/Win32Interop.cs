using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AltCodeKneeboard.Kneeboard
{
    internal static class WM
    {
        public const int
            NULL = 0x0000,
            CREATE = 0x0001,
            DELETEITEM = 0x002D,
            DESTROY = 0x0002,
            MOVE = 0x0003,
            SIZE = 0x0005,
            ACTIVATE = 0x0006,
            SETFOCUS = 0x0007,
            KILLFOCUS = 0x0008,
            ENABLE = 0x000A,
            SETREDRAW = 0x000B,
            SETTEXT = 0x000C,
            GETTEXT = 0x000D,
            GETTEXTLENGTH = 0x000E,
            PAINT = 0x000F,
            CLOSE = 0x0010,
            QUERYENDSESSION = 0x0011,
            QUIT = 0x0012,
            QUERYOPEN = 0x0013,
            ERASEBKGND = 0x0014,
            SYSCOLORCHANGE = 0x0015,
            ENDSESSION = 0x0016,
            SHOWWINDOW = 0x0018,
            WININICHANGE = 0x001A,
            SETTINGCHANGE = 0x001A,
            DEVMODECHANGE = 0x001B,
            ACTIVATEAPP = 0x001C,
            FONTCHANGE = 0x001D,
            TIMECHANGE = 0x001E,
            CANCELMODE = 0x001F,
            SETCURSOR = 0x0020,
            MOUSEACTIVATE = 0x0021,
            CHILDACTIVATE = 0x0022,
            QUEUESYNC = 0x0023,
            GETMINMAXINFO = 0x0024,
            PAINTICON = 0x0026,
            ICONERASEBKGND = 0x0027,
            NEXTDLGCTL = 0x0028,
            SPOOLERSTATUS = 0x002A,
            DRAWITEM = 0x002B,
            MEASUREITEM = 0x002C,
            VKEYTOITEM = 0x002E,
            CHARTOITEM = 0x002F,
            SETFONT = 0x0030,
            GETFONT = 0x0031,
            SETHOTKEY = 0x0032,
            GETHOTKEY = 0x0033,
            QUERYDRAGICON = 0x0037,
            COMPAREITEM = 0x0039,
            GETOBJECT = 0x003D,
            COMPACTING = 0x0041,
            COMMNOTIFY = 0x0044,
            WINDOWPOSCHANGING = 0x0046,
            WINDOWPOSCHANGED = 0x0047,
            POWER = 0x0048,
            COPYDATA = 0x004A,
            CANCELJOURNAL = 0x004B,
            NOTIFY = 0x004E,
            INPUTLANGCHANGEREQUEST = 0x0050,
            INPUTLANGCHANGE = 0x0051,
            TCARD = 0x0052,
            HELP = 0x0053,
            USERCHANGED = 0x0054,
            NOTIFYFORMAT = 0x0055,
            CONTEXTMENU = 0x007B,
            STYLECHANGING = 0x007C,
            STYLECHANGED = 0x007D,
            DISPLAYCHANGE = 0x007E,
            GETICON = 0x007F,
            SETICON = 0x0080,
            NCCREATE = 0x0081,
            NCDESTROY = 0x0082,
            NCCALCSIZE = 0x0083,
            NCHITTEST = 0x0084,
            NCPAINT = 0x0085,
            NCACTIVATE = 0x0086,
            GETDLGCODE = 0x0087,
            NCMOUSEMOVE = 0x00A0,
            NCMOUSELEAVE = 0x02A2,
            NCLBUTTONDOWN = 0x00A1,
            NCLBUTTONUP = 0x00A2,
            NCLBUTTONDBLCLK = 0x00A3,
            NCRBUTTONDOWN = 0x00A4,
            NCRBUTTONUP = 0x00A5,
            NCRBUTTONDBLCLK = 0x00A6,
            NCMBUTTONDOWN = 0x00A7,
            NCMBUTTONUP = 0x00A8,
            NCMBUTTONDBLCLK = 0x00A9,
            NCXBUTTONDOWN = 0x00AB,
            NCXBUTTONUP = 0x00AC,
            NCXBUTTONDBLCLK = 0x00AD,
            KEYFIRST = 0x0100,
            KEYDOWN = 0x0100,
            KEYUP = 0x0101,
            CHAR = 0x0102,
            DEADCHAR = 0x0103,
            CTLCOLOR = 0x0019,
            SYSKEYDOWN = 0x0104,
            SYSKEYUP = 0x0105,
            SYSCHAR = 0x0106,
            SYSDEADCHAR = 0x0107,
            KEYLAST = 0x0108,
            IME_STARTCOMPOSITION = 0x010D,
            IME_ENDCOMPOSITION = 0x010E,
            IME_COMPOSITION = 0x010F,
            IME_KEYLAST = 0x010F,
            INITDIALOG = 0x0110,
            COMMAND = 0x0111,
            SYSCOMMAND = 0x0112,
            TIMER = 0x0113,
            HSCROLL = 0x0114,
            VSCROLL = 0x0115,
            INITMENU = 0x0116,
            INITMENUPOPUP = 0x0117,
            MENUSELECT = 0x011F,
            MENUCHAR = 0x0120,
            ENTERIDLE = 0x0121,
            UNINITMENUPOPUP = 0x0125,
            CHANGEUISTATE = 0x0127,
            UPDATEUISTATE = 0x0128,
            QUERYUISTATE = 0x0129,
            CTLCOLORMSGBOX = 0x0132,
            CTLCOLOREDIT = 0x0133,
            CTLCOLORLISTBOX = 0x0134,
            CTLCOLORBTN = 0x0135,
            CTLCOLORDLG = 0x0136,
            CTLCOLORSCROLLBAR = 0x0137,
            CTLCOLORSTATIC = 0x0138,
            MOUSEFIRST = 0x0200,
            MOUSEMOVE = 0x0200,
            LBUTTONDOWN = 0x0201,
            LBUTTONUP = 0x0202,
            LBUTTONDBLCLK = 0x0203,
            RBUTTONDOWN = 0x0204,
            RBUTTONUP = 0x0205,
            RBUTTONDBLCLK = 0x0206,
            MBUTTONDOWN = 0x0207,
            MBUTTONUP = 0x0208,
            MBUTTONDBLCLK = 0x0209,
            XBUTTONDOWN = 0x020B,
            XBUTTONUP = 0x020C,
            XBUTTONDBLCLK = 0x020D,
            MOUSEWHEEL = 0x020A,
            MOUSELAST = 0x020A,
            PARENTNOTIFY = 0x0210,
            ENTERMENULOOP = 0x0211,
            EXITMENULOOP = 0x0212,
            NEXTMENU = 0x0213,
            SIZING = 0x0214,
            CAPTURECHANGED = 0x0215,
            MOVING = 0x0216,
            POWERBROADCAST = 0x0218,
            DEVICECHANGE = 0x0219,
            IME_SETCONTEXT = 0x0281,
            IME_NOTIFY = 0x0282,
            IME_CONTROL = 0x0283,
            IME_COMPOSITIONFULL = 0x0284,
            IME_SELECT = 0x0285,
            IME_CHAR = 0x0286,
            IME_KEYDOWN = 0x0290,
            IME_KEYUP = 0x0291,
            MDICREATE = 0x0220,
            MDIDESTROY = 0x0221,
            MDIACTIVATE = 0x0222,
            MDIRESTORE = 0x0223,
            MDINEXT = 0x0224,
            MDIMAXIMIZE = 0x0225,
            MDITILE = 0x0226,
            MDICASCADE = 0x0227,
            MDIICONARRANGE = 0x0228,
            MDIGETACTIVE = 0x0229,
            MDISETMENU = 0x0230,
            ENTERSIZEMOVE = 0x0231,
            EXITSIZEMOVE = 0x0232,
            DROPFILES = 0x0233,
            MDIREFRESHMENU = 0x0234,
            MOUSEHOVER = 0x02A1,
            MOUSELEAVE = 0x02A3,
            DPICHANGED = 0x02E0,
            GETDPISCALEDSIZE = 0x02e1,
            DPICHANGED_BEFOREPARENT = 0x02E2,
            DPICHANGED_AFTERPARENT = 0x02E3,
            CUT = 0x0300,
            COPY = 0x0301,
            PASTE = 0x0302,
            CLEAR = 0x0303,
            UNDO = 0x0304,
            RENDERFORMAT = 0x0305,
            RENDERALLFORMATS = 0x0306,
            DESTROYCLIPBOARD = 0x0307,
            DRAWCLIPBOARD = 0x0308,
            PAINTCLIPBOARD = 0x0309,
            VSCROLLCLIPBOARD = 0x030A,
            SIZECLIPBOARD = 0x030B,
            ASKCBFORMATNAME = 0x030C,
            CHANGECBCHAIN = 0x030D,
            HSCROLLCLIPBOARD = 0x030E,
            QUERYNEWPALETTE = 0x030F,
            PALETTEISCHANGING = 0x0310,
            PALETTECHANGED = 0x0311,
            HOTKEY = 0x0312,
            PRINT = 0x0317,
            PRINTCLIENT = 0x0318,
            THEMECHANGED = 0x031A,
            HANDHELDFIRST = 0x0358,
            HANDHELDLAST = 0x035F,
            AFXFIRST = 0x0360,
            AFXLAST = 0x037F,
            PENWINFIRST = 0x0380,
            PENWINLAST = 0x038F;
    }

    internal static class WndClassType
    {
        public const int
            CS_VREDRAW = 0x0001,
            CS_HREDRAW = 0x0002,
            CS_DBLCLKS = 0x0008,
            CS_OWNDC = 0x0020,
            CS_CLASSDC = 0x0040,
            CS_PARENTDC = 0x0080,
            CS_NOCLOSE = 0x0200,
            CS_SAVEBITS = 0x0800,
            CS_BYTEALIGNCLIENT = 0x1000,
            CS_BYTEALIGNWINDOW = 0x2000,
            CS_GLOBALCLASS = 0x4000,
            CS_IME = 0x00010000,
            CS_DROPSHADOW = 0x00020000;
    }

    internal static class WndStyle
    {
        public const uint
            WS_OVERLAPPED = 0x00000000,
            WS_POPUP = 0x80000000,
            WS_CHILD = 0x40000000,
            WS_MINIMIZE = 0x20000000,
            WS_VISIBLE = 0x10000000,
            WS_DISABLED = 0x08000000,
            WS_CLIPSIBLINGS = 0x04000000,
            WS_CLIPCHILDREN = 0x02000000,
            WS_MAXIMIZE = 0x01000000,
            WS_CAPTION = 0x00C00000,
            WS_BORDER = 0x00800000,
            WS_DLGFRAME = 0x00400000,
            WS_VSCROLL = 0x00200000,
            WS_HSCROLL = 0x00100000,
            WS_SYSMENU = 0x00080000,
            WS_THICKFRAME = 0x00040000,
            WS_GROUP = 0x00020000,
            WS_TABSTOP = 0x00010000,
            WS_MINIMIZEBOX = 0x00020000,
            WS_MAXIMIZEBOX = 0x00010000,
            WS_TILED = WS_OVERLAPPED,
            WS_ICONIC = WS_MINIMIZE,
            WS_SIZEBOX = WS_THICKFRAME,
            WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW,
            WS_OVERLAPPEDWINDOW = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX),
            WS_POPUPWINDOW = (WS_POPUP | WS_BORDER | WS_SYSMENU),
            WS_CHILDWINDOW = (WS_CHILD);

    }

    internal static class ExtendedWndStyle
    {
        public const uint
            WS_EX_DLGMODALFRAME = 0x00000001,
            WS_EX_NOPARENTNOTIFY = 0x00000004,
            WS_EX_TOPMOST = 0x00000008,
            WS_EX_ACCEPTFILES = 0x00000010,
            WS_EX_TRANSPARENT = 0x00000020,
            WS_EX_MDICHILD = 0x00000040,
            WS_EX_TOOLWINDOW = 0x00000080,
            WS_EX_WINDOWEDGE = 0x00000100,
            WS_EX_CLIENTEDGE = 0x00000200,
            WS_EX_CONTEXTHELP = 0x00000400,
            WS_EX_RIGHT = 0x00001000,
            WS_EX_LEFT = 0x00000000,
            WS_EX_RTLREADING = 0x00002000,
            WS_EX_LTRREADING = 0x00000000,
            WS_EX_LEFTSCROLLBAR = 0x00004000,
            WS_EX_RIGHTSCROLLBAR = 0x00000000,
            WS_EX_CONTROLPARENT = 0x00010000,
            WS_EX_STATICEDGE = 0x00020000,
            WS_EX_APPWINDOW = 0x00040000,
            WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE),
            WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST),
            WS_EX_LAYERED = 0x00080000,
            WS_EX_NOINHERITLAYOUT = 0x00100000,
            WS_EX_LAYOUTRTL = 0x00400000,
            WS_EX_COMPOSITED = 0x02000000,
            WS_EX_NOACTIVATE = 0x08000000;
    }

    internal static class HitTest
    {
        public const int
            HTERROR = -2,
            HTTRANSPARENT = -1,
            HTNOWHERE = 0,
            HTCLIENT = 1,
            HTCAPTION = 2,
            HTSYSMENU = 3,
            HTGROWBOX = 4,
            HTSIZE = HTGROWBOX,
            HTMENU = 5,
            HTHSCROLL = 6,
            HTVSCROLL = 7,
            HTMINBUTTON = 8,
            HTMAXBUTTON = 9,
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17,
            HTBORDER = 18,
            HTOBJECT = 19,
            HTCLOSE = 20,
            HTHELP = 21;
    }

    internal static class BlendOp
    {
        public const byte AC_SRC_OVER = 0x00;
        public const byte AC_SRC_ALPHA = 0x01;
    }

    internal static class UpdateLayerWindowParameter
    {
        public const int ULW_COLORKEY = 0x00000001;
        public const int ULW_ALPHA = 0x00000002;
        public const int ULW_OPAQUE = 0x00000004;
        public const int ULW_EX_NORESIZE = 0x00000008;
    }

    internal static class WindowShowStyle
    {
        public const int Hide = 0;
        public const int ShowNormal = 1;
        public const int ShowMinimized = 2;
        public const int ShowMaximized = 3;
        public const int Maximize = 3;
        public const int ShowNormalNoActivate = 4;
        public const int Show = 5;
        public const int Minimize = 6;
        public const int ShowMinNoActivate = 7;
        public const int ShowNoActivate = 8;
        public const int Restore = 9;
        public const int ShowDefault = 10;
        public const int ForceMinimized = 11;
    }

    internal static class SetWindowPosFlags
    {
        public const int
            SWP_NOSIZE = 0x0001,
            SWP_NOMOVE = 0x0002,
            SWP_NOZORDER = 0x0004,
            SWP_NOACTIVATE = 0x0010,
            SWP_SHOWWINDOW = 0x0040,
            SWP_HIDEWINDOW = 0x0080,
            SWP_DRAWFRAME = 0x0020,
            SWP_NOOWNERZORDER = 0x0200;
    }

    internal static class MouseValues
    {
        public const int WHEEL_DELTA = 120;

        public const int XBUTTON1 = 0x0001;
        public const int XBUTTON2 = 0x0002;

        public const int MA_ACTIVATE = 1;
        public const int MA_NOACTIVATE = 3;

        public const uint TME_CANCEL = 0x80000000;
        public const uint TME_HOVER = 0x00000001;
        public const uint TME_LEAVE = 0x00000002;
        public const uint TME_NONCLIENT = 0x00000010;
        public const uint TME_QUERY = 0x40000000;
    }

    internal delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct WNDCLASSEX
    {
        [MarshalAs(UnmanagedType.U4)]
        public uint cbSize;
        [MarshalAs(UnmanagedType.U4)]
        public uint style;
        public WndProcDelegate lpfnWndProc;
        public int cbClsExtra;
        public int cbWndExtra;
        public IntPtr hInstance;
        public IntPtr hIcon;
        public IntPtr hCursor;
        public IntPtr hbrBackground;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpszMenuName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpszClassName;
        public IntPtr hIconSm;

        public void Init()
        {
            cbSize = (uint)Marshal.SizeOf(this);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct BLENDFUNCTION
    {
        byte BlendOp;
        byte BlendFlags;
        byte SourceConstantAlpha;
        byte AlphaFormat;

        public BLENDFUNCTION(byte op, byte flags, byte alpha, byte format)
        {
            BlendOp = op;
            BlendFlags = flags;
            SourceConstantAlpha = alpha;
            AlphaFormat = format;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public int Height => Bottom - Top;
        public int Width => Right - Left;
        public Size Size => new Size(Width, Height);
        public Point Location => new Point(Left, Top);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SIZE
    {
        public int CX;
        public int CY;

        public SIZE(int cx, int cy)
        {
            CX = cx;
            CY = cy;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WINDOWPOS
    {
        public IntPtr hwndInsertAfter;
        public IntPtr hwnd;
        public int x;
        public int y;
        public int width;
        public int height;
        public uint flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct BITMAPINFOHEADER
    {
        public uint biSize;
        public int biWidth;
        public int biHeight;
        public ushort biPlanes;
        public ushort biBitCount;
        public uint biCompression;
        public uint biSizeImage;
        public int biXPelsPerMeter;
        public int biYPelsPerMeter;
        public uint biClrUsed;
        public uint biClrImportant;

        public void Init()
        {
            biSize = (uint)Marshal.SizeOf(this);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct TRACKMOUSEEVENT
    {
        public uint cbSize;
        public uint dwFlags;
        public IntPtr hwndTrack;
        public uint dwHoverTime;

        public void Init()
        {
            cbSize = (uint)Marshal.SizeOf(this);
        }
    }

    internal static class Win32Interop
    {
        #region Kernel32

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        #endregion

        #region Window manager

        [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.U2)]
        public static extern short RegisterClassEx(ref WNDCLASSEX lpwcx);

        [DllImport("user32", SetLastError = true)]
        public static extern bool UnregisterClass(string lpClassName, IntPtr hInstance);

        [DllImport("user32", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr CreateWindowEx(uint dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

        [DllImport("user32", SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32", CharSet = CharSet.Unicode, SetLastError = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int nMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32", SetLastError = true)]
        public static extern IntPtr DefWindowProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32", ExactSpelling = true, SetLastError = true)]
        public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref POINT pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT pptSrc, uint crKey, ref BLENDFUNCTION pblend, uint dwFlags);

        [DllImport("user32", SetLastError = true)]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, int crKey, byte bAlpha, int dwFlags);

        [DllImport("user32", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("user32", ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, ref POINT pt, int cPoints);

        [DllImport("user32", SetLastError = true)]
        public static extern IntPtr WindowFromPoint(POINT pt);

        [DllImport("user32", SetLastError = true)]
        public static extern IntPtr GetActiveWindow();

        [DllImport("user32", SetLastError = true)]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);

        [DllImport("user32", SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int x, int y, int cx, int cy, int flags);

        [DllImport("user32", SetLastError = true)]
        public static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);

        [DllImport("user32", SetLastError = true)]
        public static extern bool InvalidateRect(IntPtr hWnd, ref RECT lpRect, bool bErase);

        [DllImport("user32", SetLastError = true)]
        public static extern bool DestroyWindow(IntPtr hWnd);

        #endregion

        #region Mouse / Cursor

        [DllImport("user32")]
        public static extern IntPtr GetCapture();

        [DllImport("user32")]
        public static extern bool ReleaseCapture();

        [DllImport("user32")]
        public static extern bool GetCursorPos(ref POINT lpPoint);

        [DllImport("user32")]
        public static extern IntPtr SetCursor(IntPtr hCursor);

        [DllImport("user32", SetLastError = true)]
        public static extern bool TrackMouseEvent(ref TRACKMOUSEEVENT trackMouseEvent);

        #endregion

        #region GDI

        [DllImport("gdi32", SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32", SetLastError = true)]
        public static extern IntPtr CreateDIBSection(IntPtr hdc, ref BITMAPINFOHEADER pbmi, uint iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

        [DllImport("gdi32", ExactSpelling = true, PreserveSig = true, SetLastError = true)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32")]
        public static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32")]
        public static extern bool DeleteObject(IntPtr hObject);

        #endregion
    }
}
