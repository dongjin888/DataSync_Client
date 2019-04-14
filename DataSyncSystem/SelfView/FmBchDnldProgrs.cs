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
        private int allLength = 0;

        public FmBchDnldProgrs()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        public FmBchDnldProgrs(int fileNum,long leng)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;

            maxFileNum = fileNum;
            allLength = (int)(leng / 1000);
            Text = "download files";
            progress.Maximum = allLength;
            labPersent.Text = "0/" + maxFileNum;
        }

        private delegate void UpdtProg(int num,string fileName,long sent);
        public void updtProg(int num,string fileName,long sent)
        {
            if (this.InvokeRequired)
            {
                UpdtProg updt = new UpdtProg(updtProg);
                this.Invoke(updt, new object[] { num,fileName,sent});
            }
            else
            {
                int cur = (int)(sent / 1000);
                if (cur > allLength)
                    cur = allLength;
                progress.Value = cur;
                labInfo.Text = fileName;
                labPersent.Text = (maxFileNum - num) + "/" + maxFileNum;
                Application.DoEvents();
                if (cur== allLength || (maxFileNum-num >= maxFileNum))
                {
                    Dispose();
                }
            }
        }

    }
}
