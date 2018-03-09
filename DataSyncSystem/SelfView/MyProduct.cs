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
    public partial class MyProduct : UserControl
    {
        private Color parentColor;
        private string pdctName;

        //获取pmPan ， 设置颜色控制
        private FmMain fmMain;

        public string PdctName
        {
            get { return pdctName; }
        }
        public MyProduct(string pdct, Color bkColor, FmMain fm)
        {
            InitializeComponent();
            parentColor = bkColor;
            BackColor = bkColor;
            fmMain = fm;
            pdctName = pdct;

            labPdctName.Text = pdct;
        }

        private void MyProduct_Load(object sender, EventArgs e)
        {

        }

        private void MyProduct_Paint(object sender, PaintEventArgs e)
        {
            //使用红色虚线绘制边框
            Pen pen1 = new Pen(Color.White, 3);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            pen1.DashPattern = new float[] { 4f, 2f };
            e.Graphics.DrawRectangle(pen1, 0, 0, this.Width - 1, this.Height - 1);
        }

        private void MyProduct_Click(object sender, EventArgs e)
        {
            fmMain.pmCurPan += 1;
            fmMain.pmCurLab += 2;
            fmMain.pmLabTexts[fmMain.pmCurLab - 1] = pdctName;
            fmMain.setCurPmLab();
            fmMain.setCurPmPan();

            //同时设置好该Product中 包含的Trial List数据
            DataService service = new DataService();
            fmMain.pmTrialsList = service.getTrPgByPdct(fmMain.pmLabTexts[2], pdctName,
                                        fmMain.pmTrialPgNow, fmMain.pmTrialPgSize, ref fmMain.pmTrialPgAll);
            fmMain.pmPanTrials.Controls.Clear();
            fmMain.Refresh();
            service.closeCon();
        }

        private void MyProduct_MouseLeave(object sender, EventArgs e)
        {
            BackColor = parentColor;
        }

        private void MyProduct_MouseEnter(object sender, EventArgs e)
        {
            BackColor = Color.Silver;
        }
    }
}
