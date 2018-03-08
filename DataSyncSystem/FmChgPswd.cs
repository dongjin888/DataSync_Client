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
    public partial class FmChgPswd : Form
    {
        bool eye1 = false;
        bool eye2 = false;
        DataService service;
        User curUser;

        public FmChgPswd(DataService service,User curUser)
        {
            InitializeComponent();
            this.service = service;
            this.curUser = curUser;
            StartPosition = FormStartPosition.CenterScreen;
            
        }

        private void picPswd1_MouseClick(object sender, MouseEventArgs e)
        {
            eye1 = !eye1;
            if (eye1)
            {
                txtPswd.PasswordChar = new char();
                picPswd1.Image = Properties.Resources.eye;
            }
            else
            {
                txtPswd.PasswordChar = '*';
                picPswd1.Image = Properties.Resources.hide;
            }
        }

        private void picPswd2_Click(object sender, EventArgs e)
        {
            eye2 = !eye2;
            if (eye2)
            {
                txtPswd2.PasswordChar = new char();
                picPswd2.Image = Properties.Resources.eye;
            }
            else
            {
                txtPswd2.PasswordChar = '*';
                picPswd2.Image = Properties.Resources.hide;
            }
        }

        private void btSure_Click(object sender, EventArgs e)
        {
            if (!txtPswd.Text.Trim().Equals(txtPswd2.Text.Trim()))
            {
                MessageBox.Show("two password not equal!", "error");
            }
            else
            {
                string pswd = MyMd5.getMd5EncryptedStr(txtPswd2.Text.Trim());
                try
                {
                    service.chgPswd(curUser.UserId, pswd);
                    MessageBox.Show("change password success!", "message");
                    this.Dispose();

                }catch(Exception ex)
                {
                    MyLogger.WriteLine("change password failed!");
                    MessageBox.Show("change password failed!\r\n" + ex.Message, "error");
                }
            }
        }
    }
}
