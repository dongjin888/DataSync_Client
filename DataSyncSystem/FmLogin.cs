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
        DataService service = null;

        public FmLogin()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void FmLogin_Load(object sender, EventArgs e)
        {
            service = new DataService();
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
                string encryptedPass = MyMd5.getMd5EncryptedStr(userPass);
                string userLevel = "";

                //用户身份合法
                if (service.checkUser(userId, encryptedPass,ref userLevel))
                {
                    if (userLevel.Equals("normallv"))
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                    else if (userLevel.Equals("lowlv"))
                    {
                        this.DialogResult = DialogResult.Yes;
                    }

                    //保持用户id 到cache
                    Cache.userId = userId;
                    this.Close();
                }
                //用户身份不合法
                else
                {
                    labLoginStatus.Visible = true;
                    labLoginStatus.Text = "用户名或密码错误!";
                    txtName.Focus();
                }
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

        private void FmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(service != null)
            {
                service.closeCon();
            }
        }
    }
}
