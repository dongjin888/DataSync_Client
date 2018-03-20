using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataSyncSystem.SelfView
{
    public partial class FmWaitAnlyzFile : Form
    {
        private string[] dots = { ".", "..", "...", "....", ".....", "......", ".......", "........" };
        volatile private bool runFlg;
        private int index = 0;

        public FmWaitAnlyzFile()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            runFlg = true;
            StartPosition = FormStartPosition.CenterScreen;

            Thread th = new Thread(showDots);
            th.IsBackground = true;
            th.Start();
        }
        private void showDots()
        {
            while (runFlg)
            {
                update();
                Thread.Sleep(400);
            }
        }

        private delegate void MyUpdate();
        private void update()
        {
            index++;

            if (this.InvokeRequired)
            {
                MyUpdate upd = new MyUpdate(update);
                this.Invoke(upd, new object[] { });
            }
            else
            {
                labDot.Text = dots[index % dots.Length];
                Application.DoEvents();
            }
        }

        private void FmWaitAnlyzFile_FormClosed(object sender, FormClosedEventArgs e)
        {
            runFlg = false;
        }
    }
}
