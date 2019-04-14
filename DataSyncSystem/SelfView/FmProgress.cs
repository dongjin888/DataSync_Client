using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataSyncSystem.Utils;

namespace DataSyncSystem.SelfView
{
    public partial class FmProgress : Form
    {
        //private Form parent;
        private string progType;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="par">寄生的Form</param>
        /// <param name="maxValue">进度条的最大值</param>
        /// <param name="type">类型:upld, dnld , csvdnld </param>
        public FmProgress(Form par,long maxValue,string type)
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            progress.Maximum = (int)maxValue/100;
            progType = type;
            //parent = par;
            StartPosition = FormStartPosition.CenterScreen;

            string msg = "";
            if (type.Equals("dnld")) { progType = "下载"; }
            else if (type.Equals("upld")) { progType = "上传"; }
            else if (type.Equals("csvdnld")) { progType = "csv文件下载"; }
            else { type = "任务"; }

            Text = progType + "进度";
            MyLogger.WriteLine("progress bar max value:" + maxValue);
        }

        private delegate void SetProgValue(long pos,string info);
        public void updateProgValue(long pos,string info)
        {
            if (InvokeRequired)
            {
                SetProgValue setProg = new SetProgValue(updateProgValue);
                Invoke(setProg, new object[] { pos,info});
            }
            else
            {
                int tmp = (int)pos / 100;
                if (tmp > progress.Maximum)
                    tmp = progress.Maximum;
                progress.Value = tmp;
                progress.Refresh();
                labInfo.Text = "当前进度:" + (int)(((float)progress.Value / progress.Maximum) * 100) + "/100";
                Application.DoEvents(); //这句代码用于labinfo 信息的显示，否则labinfo 不能正确显示出来
                if (progress.Value == progress.Maximum)
                {
                    Dispose();
                    //parent.enable();
                }
            }
        }

        private void FmProgress_Load(object sender, EventArgs e)
        {
            //CheckForIllegalCrossThreadCalls = false;
            //parent.disable();
        }

        private void FmProgress_FormClosed(object sender, FormClosedEventArgs e)
        {
            //parent.enable();
        }
    }
}
