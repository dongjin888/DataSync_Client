using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataSyncSystem.Utils;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Text;

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

            // 从文件系统获取配置信息
            /*
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
            ContantInfo.Database.CONSQLSTR = ;
            ContantInfo.SockServ.ip = ;
            ContantInfo.SockServ.port = ;
            */


            //弹出登录提示框
            Form fmLogin = new FmLogin();
            fmLogin.FormBorderStyle = FormBorderStyle.FixedDialog;

            DialogResult fmLoginRes = fmLogin.ShowDialog();

            //OK , 表示登录成功 > MainForm 
            if (fmLoginRes == DialogResult.OK)
            {
                //主界面开始运行
                Form fmMain = new FmMain();
                //fmMain.FormBorderStyle = FormBorderStyle.FixedDialog;

                Application.Run(fmMain);

            }else if(fmLoginRes == DialogResult.Yes)
            {
                //工厂界面
                FmFactory fmFactory = new FmFactory();
                fmFactory.FormBorderStyle = FormBorderStyle.FixedDialog;

                Application.Run(fmFactory);
            }
        }
    }
}
