using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataSyncSystem.Dao;
using DataSyncSystem.Utils;

namespace DataSyncSystem
{
    public partial class FmUser : Form
    {
        User curUser = null;
        DataService service;
        FmMain owner = null;
        public FmUser(FmMain fm,string userId)
        {
            service = new DataService();
            curUser = service.getUserByUserId(userId);

            owner = fm;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            labUserIds.Text = curUser.UserId;
            labUserNames.Text = curUser.UserName;
            labTeamNames.Text = curUser.TeamName;
            labUserTels.Text = curUser.UserTel;
            labUserLevels.Text = curUser.UserLevel;
            labUserInfos.Text = curUser.UserInfo;
            this.Text = userId;
        }

        private void btHimUpload_MouseEnter(object sender, EventArgs e)
        {
            btHimUpload.BackColor = Color.Salmon;
            btHimUpload.ForeColor = Color.White;
        }

        private void btHimUpload_MouseLeave(object sender, EventArgs e)
        {
            btHimUpload.BackColor = Color.LightGray;
            btHimUpload.ForeColor = Color.Black;
        }

        private void FmUser_Load(object sender, EventArgs e)
        {

        }

        private void btHimUpload_Click(object sender, EventArgs e)
        {
            //点击他的上传，更改panMyUpload 中的内容
            owner.plUploaderId = curUser.UserId;
            owner.plUploadPgNow = 1;
            owner.plUploadList = service.getTrialPageList(curUser.UserId, owner.plUploadPgNow,
                                                owner.plUploadPgSize, ref owner.plUploadPgAll);

            //设置页面中panel 的显示
            if (!owner.panMyLoad.Visible) { owner.panMyLoad.Visible = true; owner.btMyLoad.BackColor = Color.Red; }
            if (owner.panMain.Visible) { owner.panMain.Visible = false; owner.btMain.BackColor = Color.White; }
            if (owner.panSetting.Visible) { owner.panSetting.Visible = false; owner.btSetting.BackColor = Color.White; }

            //更改我的上传为他的名字
            owner.btMyLoad.Text = curUser.UserName;

            owner.panMyLoad.Controls.Clear();
            owner.panMyLoad.Refresh();
            this.Close();
        }

        private void FmUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            service.closeCon();
        }

    }
}
