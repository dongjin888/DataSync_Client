using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataSyncSystem.Utils;
using System.IO;

namespace DataSyncSystem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FileInfo cfg = new FileInfo(Environment.CurrentDirectory+"\\.syscfg.cfg");
            FileStream stream = new FileStream(cfg.FullName, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string tmp = "";
            string[] parts = new string[4];
            int i = 0;
            while((tmp = sr.ReadLine())!= null)
            {
                if (!tmp.StartsWith("#"))
                {
                    parts[i] = tmp.Split('%')[1];
                    MyLogger.WriteLine("cfg:"+parts[i]);
                    i++;
                }
            }
            sr.Close();
            stream.Close();

            ContantInfo.Database.CONSQLSTR = parts[0];
            ContantInfo.SockServ.ip = parts[1];
            ContantInfo.SockServ.port = parts[2];
            //ContantInfo.Fs.path = parts[3];
            
            Form fmMain = new FmMain();
            fmMain.FormBorderStyle = FormBorderStyle.FixedDialog;

            string userLevel = "";
            Form fmLogin = new FmLogin();
            fmLogin.FormBorderStyle = FormBorderStyle.FixedDialog;

            DialogResult fmLoginRes = fmLogin.ShowDialog();

            //OK , 表示登录成功 > MainForm 
            if (fmLoginRes == DialogResult.OK)
            {
                //主界面开始运行
                Application.Run(fmMain);

            }else if(fmLoginRes == DialogResult.Yes)
            {
                //
                //Application.Run(new FmFactory());
            }
        }
    }
}
