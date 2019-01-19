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
    internal class KneeboardWindow : NativeWindow
    {
        

        public KneeboardWindow()
        {
            //_CtrlWndProcDelegate = new WndProcDelegate(CtrlWndProc);
        }

        

        public override void ReleaseHandle()
        {
            base.ReleaseHandle();

        }

        protected override void WndProc(ref Message m)
        {
            //if (m.Msg == WindowsMessage.WM_MOUSEACTIVATE)
            //{
            //    m.Result = (IntPtr)MA_NOACTIVATE;
            //    return;
            //}
            base.WndProc(ref m);
        }

        private void RenderWindow()
        {

        }

        public void RenderKneeboard(Graphics g, Rectangle bounds, int scrollOffset)
        {

        }

        

    }
}
