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

namespace DataSyncSystem.SelfView
{
    public partial class MyPlatform : UserControl
    {
        private Color parentColor;
        private string pltfmName;

        //获取pmPan ， 设置颜色控制
        private FmMain fmMain;

        public string PltfmName
        {
            get { return pltfmName; }
        }

        public MyPlatform(string pltfm,Color bkColor,FmMain fm)
        {
            InitializeComponent();
            BackColor = bkColor;
            parentColor = bkColor;
            pltfmName = pltfm;
            fmMain = fm;

            labPltfmName.Text = pltfm;
        }

        private void MyPlatform_Click(object sender, EventArgs e)
        {
            fmMain.pmCurPan += 1;
            fmMain.pmCurLab += 2;
            fmMain.pmLabTexts[fmMain.pmCurLab - 1] = pltfmName;
            fmMain.setCurPmLab();
            fmMain.setCurPmPan();
            //platform 中点击时，要设置好下一个Product中的内容
            DataService service = new DataService();
            fmMain.pmPdctPgNow = 1;
            fmMain.pmPdctList = service.getPdctPageList(
                                fmMain.pmPdctPgNow, fmMain.pmPdctPgSize, this.pltfmName, ref fmMain.pmPdctPgAll);
            fmMain.pmPanPdcts.Controls.Clear();
            fmMain.Refresh();
            service.closeCon();
        }

        private void MyPlatform_Paint(object sender, PaintEventArgs e)
        {
            //使用黑色虚线绘制边框
            Pen pen1 = new Pen(Color.Black, 3);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            pen1.DashPattern = new float[] { 4f, 2f };
            e.Graphics.DrawRectangle(pen1, 0, 0, this.Width - 1, this.Height - 1);
        }

        private void MyPlatform_MouseLeave(object sender, EventArgs e)
        {
            BackColor = parentColor;
        }

        private void MyPlatform_MouseEnter(object sender, EventArgs e)
        {
            BackColor = Color.Silver;
        }
    }
}
