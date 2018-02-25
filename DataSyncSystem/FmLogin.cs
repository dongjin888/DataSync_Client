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

namespace DataSyncSystem
{
    public partial class FmLogin : Form
    {

        public FmLogin()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void FmLogin_Load(object sender, EventArgs e)
        {

        }

        //登录事件
        private void btLogin_Click(object sender, EventArgs e)
        {
            //验证用户
            string userId = txtName.Text.Trim();
            string userPass = txtPass.Text.Trim();

            //输入合法
            if(userId !="" && userPass!="")
            {
                //开发时临时身份检查
                if(userId.Equals("123") && userPass.Equals("123"))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();

                    Cache.userId = "1000248501";
                }
                /*正常的身份检查
                string encryptedPass = MyMd5.getMd5EncryptedStr(userPass);
                DataService service = new DataService();
                //用户身份合法
                if (service.checkUser(userId, encryptedPass))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();

                    //保持用户id 到cache
                    Cache.userId = userId;
                }
                //用户身份不合法
                else
                {
                    labLoginStatus.Visible = true;
                    labLoginStatus.Text = "用户名或密码错误!";
                    txtName.Focus();
                }
                service.closeCon();
                */
            }
            //输入不合法
            else
            {
                labLoginStatus.Visible = true;
                labLoginStatus.Text = "请输入用户名和密码！";
                txtName.Focus();
            }
        }

        //用户名输入
        private void txtName_MouseClick(object sender, MouseEventArgs e)
        {
            if (labLoginStatus.Visible)
            {
                labLoginStatus.Visible = false;
                labLoginStatus.Text = "";
            }
        }

        //用户密码输入
        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            if (labLoginStatus.Visible)
            {
                labLoginStatus.Visible = false;
                labLoginStatus.Text = "";
            }
        }
    }
}
