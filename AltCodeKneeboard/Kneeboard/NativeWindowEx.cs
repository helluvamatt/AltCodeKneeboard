using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AltCodeKneeboard.Kneeboard
{
    internal class NativeWindowEx : IDisposable
    {
        private readonly WndProcDelegate _DefWndProc;
        private readonly WndProcDelegate _CtrlWndProc;

        private readonly string _RenderingWndClassName = Guid.NewGuid().ToString("N");
        private readonly string _CtrlWndClassName = Guid.NewGuid().ToString("N");

        private bool _IsRendering;
        private IntPtr _RenderingHwnd;
        private bool _MouseDown;
        private bool _MouseDoubleClicking;

        public NativeWindowEx()
        {
            _CtrlWndProc = new WndProcDelegate(WndProc);
            _DefWndProc = new WndProcDelegate(Win32Interop.DefWindowProc);

            #region Control Window

            WNDCLASSEX ctrlWndClsEx = new WNDCLASSEX();
            ctrlWndClsEx.Init();
            ctrlWndClsEx.style = 0;
            ctrlWndClsEx.lpfnWndProc = _CtrlWndProc;
            ctrlWndClsEx.cbClsExtra = 0;
            ctrlWndClsEx.cbWndExtra = 0;
            ctrlWndClsEx.hInstance = Win32Interop.GetModuleHandle(null);
            ctrlWndClsEx.hIcon = IntPtr.Zero;
            ctrlWndClsEx.hIconSm = IntPtr.Zero;
            ctrlWndClsEx.hCursor = IntPtr.Zero;
            ctrlWndClsEx.hbrBackground = IntPtr.Zero;
            ctrlWndClsEx.lpszClassName = _CtrlWndClassName;
            ctrlWndClsEx.lpszMenuName = null;

            if (Win32Interop.RegisterClassEx(ref ctrlWndClsEx) == 0) throw new Win32Exception(Marshal.GetLastWin32Error(), "RegisterClassEx failed.");
            Handle = Win32Interop.CreateWindowEx(ExtendedWndStyle.WS_EX_LAYERED | ExtendedWndStyle.WS_EX_NOACTIVATE, _CtrlWndClassName, null, WndStyle.WS_POPUP, 0, 0, 0, 0, IntPtr.Zero, IntPtr.Zero, Win32Interop.GetModuleHandle(null), IntPtr.Zero);
            if (Handle == IntPtr.Zero) throw new Win32Exception(Marshal.GetLastWin32Error(), "CreateWindowEx failed.");
            Win32Interop.SetLayeredWindowAttributes(Handle, 0, 1, UpdateLayerWindowParameter.ULW_ALPHA);

            #endregion

            #region Rendering Window

            WNDCLASSEX renderingWndClsEx = new WNDCLASSEX();
            renderingWndClsEx.Init();
            renderingWndClsEx.style = WndClassType.CS_VREDRAW | WndClassType.CS_HREDRAW;
            renderingWndClsEx.lpfnWndProc = _DefWndProc;
            renderingWndClsEx.cbClsExtra = 0;
            renderingWndClsEx.cbWndExtra = 0;
            renderingWndClsEx.hInstance = Win32Interop.GetModuleHandle(null);
            renderingWndClsEx.hIcon = IntPtr.Zero;
            renderingWndClsEx.hIconSm = IntPtr.Zero;
            renderingWndClsEx.hCursor = IntPtr.Zero;
            renderingWndClsEx.hbrBackground = IntPtr.Zero;
            renderingWndClsEx.lpszClassName = _RenderingWndClassName;
            renderingWndClsEx.lpszMenuName = null;

            if (Win32Interop.RegisterClassEx(ref renderingWndClsEx) == 0) throw new Win32Exception(Marshal.GetLastWin32Error(), "RegisterClassEx failed.");
            _RenderingHwnd = Win32Interop.CreateWindowEx(ExtendedWndStyle.WS_EX_LAYERED | ExtendedWndStyle.WS_EX_TRANSPARENT | ExtendedWndStyle.WS_EX_NOACTIVATE, _RenderingWndClassName, null, WndStyle.WS_POPUP, 0, 0, 0, 0, Handle, IntPtr.Zero, Win32Interop.GetModuleHandle(null), IntPtr.Zero);
            if (_RenderingHwnd == IntPtr.Zero) throw new Win32Exception(Marshal.GetLastWin32Error(), "CreateWindowEx failed.");

            #endregion
        }

        #region Properties

        public IntPtr Handle { get; private set; }

        private Rectangle _Bounds;
        public Rectangle Bounds
        {
            get => _Bounds;
            set
            {
                if (_Bounds != value)
                {
                    Win32Interop.SetWindowPos(Handle, IntPtr.Zero, value.X, value.Y, value.Width, value.Height, SetWindowPosFlags.SWP_NOZORDER);
                    _Bounds = value;
                }
            }
        }

        public int Left => Bounds.Left;
        public int Top => Bounds.Top;
        public int Width => Bounds.Width;
        public int Height => Bounds.Height;
        public Point Location => Bounds.Location;

        private bool _Visible;
        public bool Visible
        {
            get => _Visible;
            set
            {
                if (_Visible != value)
                {
                    var flags = SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_NOZORDER
                        | (value ? SetWindowPosFlags.SWP_SHOWWINDOW : SetWindowPosFlags.SWP_HIDEWINDOW);
                    Win32Interop.SetWindowPos(Handle, IntPtr.Zero, 0, 0, 0, 0, flags);
                    _Visible = value;
                }
            }
        }

        private Cursor _Cursor;
        public Cursor Cursor
        {
            get => _Cursor;
            set
            {
                if (_Cursor != value)
                {
                    _Cursor = value;
                    if (Handle != IntPtr.Zero && _Cursor != null)
                    {
                        var p = new POINT();
                        Win32Interop.GetCursorPos(ref p);
                        if (Bounds.Contains(p.X, p.Y) || Win32Interop.GetCapture() == Handle)
                        {
                            Win32Interop.SendMessage(Handle, WM.SETCURSOR, Handle, new IntPtr(HitTest.HTCLIENT));
                        }
                    }
                }
            }
        }

        private bool _TopMost;
        public bool TopMost
        {
            get => _TopMost;
            set
            {
                if (_TopMost != value)
                {
                    if (!Win32Interop.SetWindowPos(Handle, new IntPtr(value ? -1 : -2), 0, 0, 0, 0, SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_NOMOVE))
                    {
                        int err = Marshal.GetLastWin32Error();
                        throw new Win32Exception(err, "SetWindowPos failed");
                    }
                    _TopMost = value;
                }
            }
        }

        public bool IsActive => Handle != IntPtr.Zero && Win32Interop.GetActiveWindow() == Handle;

        public bool IsMouseOver { get; private set; }

        #endregion

        #region Public methods

        public void Invalidate()
        {
            if (!Win32Interop.InvalidateRect(Handle, IntPtr.Zero, false)) throw new Win32Exception(Marshal.GetLastWin32Error(), "InvalidateRect failed");
        }

        public void Invalidate(Rectangle r)
        {
            var lpRect = new RECT(r.Left, r.Top, r.Right, r.Bottom);
            if (!Win32Interop.InvalidateRect(Handle, ref lpRect, false)) throw new Win32Exception(Marshal.GetLastWin32Error(), "InvalidateRect failed");
        }

        #endregion

        #region Virtual methods

        protected virtual int OnHitTest(Point p) => HitTest.HTCLIENT;

        protected virtual void OnPaint(Graphics g) { }

        protected virtual void OnResize() { }

        protected virtual void OnMove() { }

        protected virtual void OnResizeEnd() => OnResize();

        protected virtual void OnMoveEnd() => OnMove();

        protected virtual void OnVisibleChanged() { }

        protected virtual bool OnClosing(CloseReason reason) => false;

        protected virtual void OnMouseClick(MouseButtons buttons, Point location) { }

        protected virtual void OnMouseDoubleClick(MouseButtons buttons, Point location) { }

        protected virtual void OnMouseDown(MouseButtons buttons, Point location) { }

        protected virtual void OnMouseUp(MouseButtons buttons, Point location) { }

        protected virtual void OnMouseEnter() { }

        protected virtual void OnMouseLeave() { }

        protected virtual void OnMouseMove(Point location) { }

        protected virtual void OnMouseWheel(int delta, Point location) { }

        //protected virtual bool OnKeyDown(Keys keys) => false;

        //protected virtual bool OnKeyUp(Keys keys) => false;

        //protected virtual bool OnKeyPress(char c) => false;

        protected virtual bool PreventActivation => false;

        #endregion

        #region Private methods

        private IntPtr WndProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            switch (msg)
            {
                case WM.MOUSEACTIVATE:
                    return new IntPtr(PreventActivation ? MouseValues.MA_NOACTIVATE : MouseValues.MA_ACTIVATE);
                case WM.PAINT:
                    WmPaint();
                    break;
                case WM.WINDOWPOSCHANGING:
                    WmWindowPosChanging(lParam);
                    return IntPtr.Zero;
                case WM.WINDOWPOSCHANGED:
                    WmWindowPosChanged(lParam);
                    return IntPtr.Zero;
                case WM.CLOSE:
                    if (WmClose(CloseReason.UserClosing)) return IntPtr.Zero;
                    break;
                case WM.QUERYENDSESSION:
                case WM.ENDSESSION:
                    if (WmClose(CloseReason.WindowsShutDown)) return IntPtr.Zero;
                    break;
                case WM.NCHITTEST:
                    return WmHitTest(lParam);
                case WM.LBUTTONDOWN:
                case WM.RBUTTONDOWN:
                case WM.MBUTTONDOWN:
                case WM.XBUTTONDOWN:
                    WmMouseDown(msg, lParam, wParam, false);
                    break;
                case WM.LBUTTONUP:
                case WM.RBUTTONUP:
                case WM.MBUTTONUP:
                case WM.XBUTTONUP:
                    WmMouseUp(msg, lParam, wParam);
                    break;
                case WM.NCLBUTTONDOWN:
                case WM.NCRBUTTONDOWN:
                case WM.NCMBUTTONDOWN:
                case WM.NCXBUTTONDOWN:
                case WM.NCLBUTTONUP:
                case WM.NCRBUTTONUP:
                case WM.NCMBUTTONUP:
                case WM.NCXBUTTONUP:
                    // Fall through to DefWindowProc so the window manager will handle moving and resizing
                    return Win32Interop.DefWindowProc(hwnd, msg, wParam, lParam);
                case WM.LBUTTONDBLCLK:
                case WM.RBUTTONDBLCLK:
                case WM.MBUTTONDBLCLK:
                case WM.XBUTTONDBLCLK:
                    WmMouseDown(msg, lParam, wParam, true);
                    break;
                case WM.MOUSEMOVE:
                    WmMouseMove(lParam);
                    break;
                case WM.MOUSEWHEEL:
                    WmMouseWheel(lParam, wParam);
                    break;
                case WM.MOUSELEAVE:
                    WmMouseLeave();
                    break;
                case WM.SETCURSOR:
                    if (wParam == Handle) Win32Interop.SetCursor((Cursor ?? Cursors.Default).Handle);
                    break;
                //case WM.KEYDOWN:
                //case WM.SYSKEYDOWN:
                //    if (WmKeyDown(wParam, lParam)) return IntPtr.Zero;
                //    break;
                //case WM.KEYUP:
                //case WM.SYSKEYUP:
                //    if (WmKeyUp(wParam, lParam)) return IntPtr.Zero;
                //    break;
                //case WM.CHAR:
                //case WM.SYSCHAR:
                //    if (WmKeyPress(wParam)) return IntPtr.Zero;
                //    break;
            }

            return Win32Interop.DefWindowProc(hwnd, msg, wParam, lParam);
        }

        private void WmWindowPosChanging(IntPtr lParam)
        {
            var windowpos = Marshal.PtrToStructure<WINDOWPOS>(lParam);
            var setLocation = (windowpos.flags & SetWindowPosFlags.SWP_NOMOVE) != SetWindowPosFlags.SWP_NOMOVE;
            var setSize = (windowpos.flags & SetWindowPosFlags.SWP_NOSIZE) != SetWindowPosFlags.SWP_NOSIZE;
            var newLocation = setLocation ? new Point(windowpos.x, windowpos.y) : _Bounds.Location;
            var newSize = setSize ? new Size(windowpos.width, windowpos.height) : _Bounds.Size;
            var newBounds = new Rectangle(newLocation, newSize);
            var moved = setLocation && newLocation != _Bounds.Location;
            var resized = setSize && newSize != _Bounds.Size;
            if (moved || resized)
            {
                _Bounds = newBounds;
                if (_RenderingHwnd != IntPtr.Zero) Win32Interop.SetWindowPos(_RenderingHwnd, IntPtr.Zero, _Bounds.X, _Bounds.Y, _Bounds.Width, _Bounds.Height, SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_NOZORDER);
                if (moved) OnMove();
                if (resized) OnResize();
            }
        }

        private void WmWindowPosChanged(IntPtr lParam)
        {
            var windowpos = Marshal.PtrToStructure<WINDOWPOS>(lParam);
            var setLocation = (windowpos.flags & SetWindowPosFlags.SWP_NOMOVE) != SetWindowPosFlags.SWP_NOMOVE;
            var setSize = (windowpos.flags & SetWindowPosFlags.SWP_NOSIZE) != SetWindowPosFlags.SWP_NOSIZE;
            var newLocation = setLocation ? new Point(windowpos.x, windowpos.y) : _Bounds.Location;
            var newSize = setSize ? new Size(windowpos.width, windowpos.height) : _Bounds.Size;
            var newBounds = new Rectangle(newLocation, newSize);
            var moved = setLocation && newLocation != _Bounds.Location;
            var resized = setSize && newSize != _Bounds.Size;
            var show = (windowpos.flags & SetWindowPosFlags.SWP_SHOWWINDOW) == SetWindowPosFlags.SWP_SHOWWINDOW;
            var hide = (windowpos.flags & SetWindowPosFlags.SWP_HIDEWINDOW) == SetWindowPosFlags.SWP_HIDEWINDOW;
            var visible = (_Visible || show) ^ hide;
            if (moved || resized || show || hide)
            {
                _Bounds = newBounds;
                if (_RenderingHwnd != IntPtr.Zero)
                {
                    var flags = SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_NOZORDER
                        | (show ? SetWindowPosFlags.SWP_SHOWWINDOW : 0)
                        | (hide ? SetWindowPosFlags.SWP_HIDEWINDOW : 0);
                    if (_RenderingHwnd != IntPtr.Zero) Win32Interop.SetWindowPos(_RenderingHwnd, IntPtr.Zero, _Bounds.X, _Bounds.Y, _Bounds.Width, _Bounds.Height, flags);
                }
                if (visible != _Visible)
                {
                    _Visible = visible;
                    OnVisibleChanged();
                }
                if (moved) OnMoveEnd();
                if (resized) OnResizeEnd();
            }
        }

        private IntPtr WmHitTest(IntPtr lParam)
        {
            return new IntPtr(OnHitTest(GetWindowCoord(lParam)));
        }

        private void WmMouseMove(IntPtr lParam)
        {
            if (!IsMouseOver)
            {
                var tme = new TRACKMOUSEEVENT();
                tme.Init();
                tme.dwFlags = MouseValues.TME_LEAVE;
                tme.hwndTrack = Handle;
                if (!Win32Interop.TrackMouseEvent(ref tme)) throw new Win32Exception(Marshal.GetLastWin32Error(), "TrackMouseEvent failed");
                IsMouseOver = true;
                OnMouseEnter();
            }

            OnMouseMove(new Point(lParam.ToInt32()));
        }

        private void WmMouseWheel(IntPtr lParam, IntPtr wParam)
        {
            var delta = SignedHIWORD(wParam.ToInt32());
            OnMouseWheel(delta, new Point(lParam.ToInt32()));
        }

        private void WmMouseDown(uint msg, IntPtr lParam, IntPtr wParam, bool doubleClick)
        {
            var buttons = MouseButtons.None;
            if (msg == WM.LBUTTONDOWN || msg == WM.NCLBUTTONDOWN || msg == WM.LBUTTONDBLCLK) buttons = MouseButtons.Left;
            else if (msg == WM.MBUTTONDOWN || msg == WM.NCMBUTTONDOWN || msg == WM.MBUTTONDBLCLK) buttons = MouseButtons.Middle;
            else if (msg == WM.RBUTTONDOWN || msg == WM.NCRBUTTONDOWN || msg == WM.RBUTTONDBLCLK) buttons = MouseButtons.Right;
            else if (msg == WM.XBUTTONDOWN || msg == WM.NCXBUTTONDOWN || msg == WM.XBUTTONDBLCLK)
            {
                int btn = HIWORD(wParam.ToInt32());
                if (btn == MouseValues.XBUTTON1) buttons = MouseButtons.XButton1;
                else if (btn == MouseValues.XBUTTON2) buttons = MouseButtons.XButton2;
            }
            if (doubleClick) _MouseDoubleClicking = true;
            else _MouseDown = true;
            OnMouseDown(buttons, new Point(lParam.ToInt32()));
        }

        private void WmMouseUp(uint msg, IntPtr lParam, IntPtr wParam)
        {
            var buttons = MouseButtons.None;
            if (msg == WM.LBUTTONUP || msg == WM.NCLBUTTONUP) buttons = MouseButtons.Left;
            else if (msg == WM.MBUTTONUP || msg == WM.NCMBUTTONUP) buttons = MouseButtons.Middle;
            else if (msg == WM.RBUTTONUP || msg == WM.NCRBUTTONUP) buttons = MouseButtons.Right;
            else if (msg == WM.XBUTTONUP || msg == WM.NCXBUTTONUP)
            {
                int btn = HIWORD(wParam.ToInt32());
                if (btn == MouseValues.XBUTTON1) buttons = MouseButtons.XButton1;
                else if (btn == MouseValues.XBUTTON2) buttons = MouseButtons.XButton2;
            }
            var pt = new Point(lParam.ToInt32());
            if (Win32Interop.WindowFromPoint(new POINT(pt.X, pt.Y)) == Handle)
            {
                if (_MouseDoubleClicking) OnMouseDoubleClick(buttons, pt);
                else if (_MouseDown) OnMouseClick(buttons, pt);
            }
            OnMouseUp(buttons, pt);
            _MouseDown = false;
        }

        private void WmMouseLeave()
        {
            IsMouseOver = false;
            OnMouseLeave();
        }

        //private bool WmKeyDown(IntPtr wParam, IntPtr lParam)
        //{
        //    return OnKeyDown((Keys)wParam);
        //}

        //private bool WmKeyUp(IntPtr wParam, IntPtr lParam)
        //{
        //    return OnKeyUp((Keys)wParam);
        //}

        //private bool WmKeyPress(IntPtr wParam)
        //{
        //    return OnKeyPress((char)wParam);
        //}

        private void WmPaint()
        {
            if (_IsRendering || _RenderingHwnd == IntPtr.Zero || Width == 0 || Height == 0) return;

            // Paint to the special rendering-only layered window
            _IsRendering = true;
            try
            {
                var ptSrc = new POINT(0, 0);
                var ptWinPos = new POINT(Left, Top);
                var szWin = new SIZE(Width, Height);
                var stBlend = new BLENDFUNCTION(BlendOp.AC_SRC_OVER, 0, 0xFF, BlendOp.AC_SRC_ALPHA);

                var hDC = Win32Interop.GetDC(_RenderingHwnd);
                if (hDC == IntPtr.Zero) throw new Win32Exception(Marshal.GetLastWin32Error(), "Failed to get device context.");

                var hdcMemory = Win32Interop.CreateCompatibleDC(hDC);
                int nBytesPerLine = ((Width * 32 + 31) & (~31)) >> 3;
                BITMAPINFOHEADER stBmpInfoHeader = new BITMAPINFOHEADER();
                stBmpInfoHeader.Init();
                stBmpInfoHeader.biWidth = Width;
                stBmpInfoHeader.biHeight = Height;
                stBmpInfoHeader.biPlanes = 1;
                stBmpInfoHeader.biBitCount = 32;
                stBmpInfoHeader.biCompression = 0; // BI_RGB
                stBmpInfoHeader.biClrUsed = 0;
                stBmpInfoHeader.biSizeImage = (uint)(nBytesPerLine * Height);

                IntPtr pvBits = IntPtr.Zero;
                IntPtr hbmpMem = Win32Interop.CreateDIBSection(hDC, ref stBmpInfoHeader, 0, out pvBits, IntPtr.Zero, 0);
                if (hbmpMem == IntPtr.Zero)
                {
                    int error = Marshal.GetLastWin32Error();
                    throw new Win32Exception(error, "Failed to create DIB.");
                }
                IntPtr hbmpOld = Win32Interop.SelectObject(hdcMemory, hbmpMem);

                using (var g = Graphics.FromHdcInternal(hdcMemory))
                {
                    OnPaint(g);
                    Win32Interop.UpdateLayeredWindow(_RenderingHwnd, hDC, ref ptWinPos, ref szWin, hdcMemory, ref ptSrc, 0, ref stBlend, UpdateLayerWindowParameter.ULW_ALPHA);
                }

                Win32Interop.SelectObject(hbmpMem, hbmpOld);
                Win32Interop.DeleteObject(hbmpMem);
                Win32Interop.DeleteDC(hdcMemory);
                Win32Interop.DeleteDC(hDC);
            }
            finally
            {
                _IsRendering = false;
            }
        }

        private bool WmClose(CloseReason reason)
        {
            return OnClosing(reason);
        }

        private Point GetWindowCoord(IntPtr lParam)
        {
            var x = SignedLOWORD(lParam.ToInt32());
            var y = SignedHIWORD(lParam.ToInt32());
            var pt = new POINT(x, y);
            Win32Interop.MapWindowPoints(IntPtr.Zero, Handle, ref pt, 1);
            return new Point(pt.X, pt.Y);
        }

        private Point GetScreenCoord(IntPtr lParam)
        {
            var x = SignedLOWORD(lParam.ToInt32());
            var y = SignedHIWORD(lParam.ToInt32());
            var pt = new POINT(x, y);
            Win32Interop.MapWindowPoints(Handle, IntPtr.Zero, ref pt, 1);
            return new Point(pt.X, pt.Y);
        }

        private static int HIWORD(int n) => (n >> 16) & 0xffff;
        private static int LOWORD(int n) => n & 0xffff;
        private static int SignedHIWORD(int n) => (short)((n >> 16) & 0xffff);
        private static int SignedLOWORD(int n) => (short)(n & 0xFFFF);

        #endregion

        #region Dispose pattern

        private bool _Disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_Disposed)
            {
                if (disposing)
                {
                    if (_RenderingHwnd != IntPtr.Zero)
                    {
                        Win32Interop.DestroyWindow(_RenderingHwnd);
                        Win32Interop.UnregisterClass(_RenderingWndClassName, Win32Interop.GetModuleHandle(null));
                    }

                    if (Handle != IntPtr.Zero)
                    {
                        Win32Interop.DestroyWindow(Handle);
                        Win32Interop.UnregisterClass(_CtrlWndClassName, Win32Interop.GetModuleHandle(null));
                    }
                }

                _Disposed = true;
            }
        }

        ~NativeWindowEx()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
