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
using System.IO;

namespace DataSyncSystem
{
    public partial class FmLogin : Form
    {
        DataService service = null;
        bool isAuto = false;
        string autoPas = null;

        public FmLogin()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            chkBoxCache.Checked = true;
        }

        private void FmLogin_Load(object sender, EventArgs e)
        {
            service = new DataService();

            //从cache 文件中读取
            string cache = Environment.CurrentDirectory + "//.user.cache";
            if (File.Exists(cache))
            {
                string read = null;
                using (FileStream fs = new FileStream(cache, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        read = sr.ReadLine();
                    }
                }

                if (read != null)
                {
                    string[] splits = read.Split('%');
                    if (splits != null)
                    {
                        txtName.Text = splits[0];

                        string pas = PsEnDecode.decode(splits[1]);
                        txtPass.Text = pas; //得到md5
                        isAuto = true;
                        autoPas = pas;
                    }
                }
            }
        }

        //登录事件
        private void btLogin_Click(object sender, EventArgs e)
        {
            judgeLogin();
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

        private void judgeLogin()
        {
            //验证用户
            string userId = txtName.Text.Trim();
            string userPass = txtPass.Text.Trim();

            //输入合法
            if (userId != "" && userPass != "")
            {
                string encryptedPass = null;
                if (isAuto)
                    encryptedPass = autoPas;
                else
                    encryptedPass = MyMd5.getMd5EncryptedStr(userPass);

                string userLevel = "";

                //用户身份合法
                if (service.checkUser(userId, encryptedPass, ref userLevel))
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


                    //写入文件
                    string cache = Environment.CurrentDirectory + "//.user.cache";
                    //记住用户
                    if (chkBoxCache.Checked)
                    {
                        using (FileStream fs = new FileStream(cache, FileMode.Create))
                        {
                            using (StreamWriter sw = new StreamWriter(fs))
                            {
                                sw.WriteLine(userId + "%" + PsEnDecode.encode(encryptedPass) + "%");
                            }
                        }
                    }
                    else //不记住用户
                    {
                        if (File.Exists(cache))
                        {
                            try
                            {
                                File.Delete(cache);
                            }
                            catch { }
                        }
                    }

                    Close();
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

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                judgeLogin();
            }
            if (e.KeyCode == Keys.Back)
            {
                isAuto = false;
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            isAuto = false;
        }
    }
}
