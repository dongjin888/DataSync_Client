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
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace DataSyncSystem.SelfView
{
    public partial class UploadRecord : UserControl
    {
        private Trial trial;
        Color bkColor;

        FmMain parent;
        FolderBrowserDialog dnldDialog;
        Socket dnldSock;

        public UploadRecord(Trial trial,Color bkColor,Form fm,FolderBrowserDialog dialog,Socket sock)
        {
            this.trial = trial;
            InitializeComponent();
            BackColor = bkColor;
            this.bkColor = bkColor;

            parent = (FmMain)fm;
            dnldDialog = dialog;
            dnldSock = sock;

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
            //从配置文件中获取 默认下载路径
            parent.dnldPath = CfgTool.getDnldPath(dnldDialog);
            if(parent.dnldPath == null)
            {
                MyLogger.WriteLine("下载取消!");
                return;
            }

            //先判断要下载的文件是否已经存在
            FileInfo existFile = new FileInfo(parent.dnldPath + "\\" + trial.TrUserId + "_" + trial.TrDate + ".zip");
            if (existFile.Exists)
            {
                MessageBox.Show("you have download the trial data!", "message");
                return;
            }

            //先获取socket 连接
            if (dnldSock == null)
            {
                MessageBox.Show("与服务端失去连接!", "message");
                parent.dnldRunFlg = false;
                return;
            }
            string dnldHead = "dnld:#";
            dnldHead += trial.TrUserId + "_" + trial.TrDate + "#";

            try
            {
                //传输head 
                dnldSock.Send(Encoding.UTF8.GetBytes(dnldHead.ToCharArray()));
                parent.dnldRunFlg = true;
            }
            catch
            {
                MessageBox.Show("与服务端断开连接!", "message");
                parent.dnldRunFlg = false;
                MyLogger.WriteLine("发送下载请求头时 遇到异常！");
            }
        }

        private void UploadRecord_MouseEnter(object sender, EventArgs e)
        {
            BackColor = Color.Silver;
        }

        private void UploadRecord_MouseLeave(object sender, EventArgs e)
        {
            BackColor = bkColor;
        }

        private void btDownload_MouseEnter(object sender, EventArgs e)
        {
            btDownload.BackColor = Color.Black;
            btDownload.ForeColor = Color.White;
        }

        private void btDownload_MouseLeave(object sender, EventArgs e)
        {
            btDownload.BackColor = Color.White;
            btDownload.ForeColor = Color.Black;
        }
    }
}
