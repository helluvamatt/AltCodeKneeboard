using AltCodeKneeboard.Data;
using AltCodeKneeboard.Kneeboard;
using AltCodeKneeboard.Models;
using AltCodeKneeboard.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace AltCodeKneeboard
{
    internal partial class KneeboardForm : Form
    {
        public KneeboardForm()
        {
            InitializeComponent();
        }

        private const int WM_MOUSEACTIVATE = 0x0021;
        private const int MA_NOACTIVATE = 0x0003;

        protected override bool ShowWithoutActivation => true;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_MOUSEACTIVATE)
            {
                m.Result = (IntPtr)MA_NOACTIVATE;
                return;
            }
            base.WndProc(ref m);
        }


        
    }
}
