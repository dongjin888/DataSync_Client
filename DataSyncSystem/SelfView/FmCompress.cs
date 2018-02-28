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
using DataSyncSystem.Utils;

namespace DataSyncSystem.SelfView
{
    public partial class FmCompress : Form
    {
        FmMain parent;

        string[] labs = { "...", "......", "........", ".........", "..........","...........",
                        ".............","..............."};

        public FmCompress(Form par)
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            parent = (FmMain)par;
            parent.disable();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private delegate void CompDele(int index);
        public void updateInfo(int index)
        {
            if (this.InvokeRequired)
            {
                CompDele compD = new CompDele(updateInfo);
                this.Invoke(compD, new object[] { index });
            }
            else
            {
                if (index == -1)
                {
                    Dispose();
                }
                else { 
                    this.labInfo.Text = "等待文件压缩" + labs[index % labs.Length];
                    Application.DoEvents();
                }
            }
            
        }

        private void FmCompress_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.enable();
        }
    }
}
