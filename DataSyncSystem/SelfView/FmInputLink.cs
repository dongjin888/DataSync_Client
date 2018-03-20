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
using DataSyncSystem.Dao;

namespace DataSyncSystem.SelfView
{
    public partial class FmInputLink : Form
    {
        private Trial trial = null;
        private DataService service = null;

        public FmInputLink()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        public FmInputLink(ref Trial tr,DataService service)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            trial = tr;

            this.service = service;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.dnldon;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.dnldlev;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //检查txt是否为空
            if (txtDnldLink.Text == null || txtDnldLink.Text.Equals(""))
            {
                MessageBox.Show("please parse the link !", "error");
                return;
            }

            //检查格式
            string[] splits = txtDnldLink.Text.Trim().Split('_');
            if (splits.Length == 4)
            {
                if(splits[1].Length == 10 && splits[3].Length == 8)
                {
                }
                else
                {
                    MessageBox.Show("link format error !", "message");
                    return;
                }
            }
            else
            {
                MessageBox.Show("link format error !", "message");
                return;
            }

            //检查txtstring 是否符合规范
            try
            {
                string dnldLink = EnDeCode.decodeLink(txtDnldLink.Text.Trim());
                MyLogger.WriteLine(dnldLink);

                string[] splits2 = dnldLink.Split('_');
                Trial tmp = service.getTrialByUidDate(splits2[0], splits2[1]);
                trial.TrUserId = tmp.TrUserId;
                trial.TrOperator = tmp.TrOperator;
                trial.TrDate = tmp.TrDate;
                trial.TrInfo = tmp.TrInfo;
                Console.WriteLine(trial);

                Close();
                DialogResult = DialogResult.OK;
            }
            catch {
                MessageBox.Show("decode link failed !", "error");
                return;
            }
        }

        private void FmInputLink_FormClosing(object sender, FormClosingEventArgs e)
        {
            //判断 ref dnldlink 是否为空，给DialogResult 赋值
            DialogResult = DialogResult.No;
        }

        private void FmInputLink_Load(object sender, EventArgs e)
        {
            txtDnldLink.Text = (string)Clipboard.GetDataObject().GetData(DataFormats.Text);
            if (txtDnldLink.Text.Equals(""))
            {
                return;
            }

            //检查格式
            string[] splits = txtDnldLink.Text.Trim().Split('_');
            if (splits.Length == 4)
            {
                if (splits[1].Length == 10 && splits[3].Length == 8)
                {
                }
                else
                {
                    MessageBox.Show("link format error !", "message");
                    return;
                }
            }
            else
            {
                MessageBox.Show("link format error !", "message");
                return;
            }

            pictureBox1.Focus();
        }
    }
}
