using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataSyncSystem.Dao;
using DataSyncSystem.Utils;

namespace DataSyncSystem.SelfView
{
    public partial class UploadRecord : UserControl
    {
        private Trial trial;
        Color bkColor;
        public UploadRecord(Trial trial,Color bkColor)
        {
            this.trial = trial;
            InitializeComponent();
            BackColor = bkColor;
            this.bkColor = bkColor;

            labPltfm.Text = trial.TrPltfmName;
            labPdct.Text = trial.TrPdctName;
            labDate.Text = TimeHandle.milSecondsToDatetime(long.Parse(trial.TrDate)).ToString();
            labInfo.Text = trial.TrInfo;

        }

        private void UploadRecord_Paint(object sender, PaintEventArgs e)
        {
            Pen pen1 = new Pen(Color.White, 3);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            pen1.DashPattern = new float[] { 4f, 2f };
            e.Graphics.DrawRectangle(pen1, 0, 0, this.Width - 1, this.Height - 1);
        }

        private void btDownload_Click(object sender, EventArgs e)
        {
            //向服务端发送下载请求
            MessageBox.Show("summary.csv path:" + trial.TrSummaryPath + "summary.csv\ndebug folder:"+
                trial.TrDebugPath,"Download!");
        }

        private void UploadRecord_MouseEnter(object sender, EventArgs e)
        {
            BackColor = Color.Silver;
        }

        private void UploadRecord_MouseLeave(object sender, EventArgs e)
        {
            BackColor = bkColor;
        }
    }
}
