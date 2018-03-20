using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataSyncSystem.SelfView
{
    public partial class FmBchDnldProgrs : Form
    {
        private int maxFileNum = 0;

        public FmBchDnldProgrs()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        public FmBchDnldProgrs(int fileNum)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;

            maxFileNum = fileNum;
            Text = "download files";
            progress.Maximum = maxFileNum;
            labPersent.Text = "0/" + maxFileNum;
        }

        


        private delegate void UpdtProg(int num,string fileName);
        public void updtProg(int num,string fileName)
        {
            if (this.InvokeRequired)
            {
                UpdtProg updt = new UpdtProg(updtProg);
                this.Invoke(updt, new object[] { num,fileName });
            }
            else
            {
                int cur = maxFileNum - num;
                if (cur > maxFileNum)
                    cur = maxFileNum;
                this.progress.Value = cur;
                this.labInfo.Text = fileName;
                labPersent.Text = (maxFileNum - num) + "/" + maxFileNum;
                Application.DoEvents();
                if (cur == maxFileNum)
                {
                    Dispose();
                }
            }
        }

    }
}
