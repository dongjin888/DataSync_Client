using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataSyncSystem.Utils
{
    public class CfgTool
    {

        static FileInfo cfgFile = new FileInfo(Environment.CurrentDirectory + "\\" + ".datasync.cfg");

        /// <summary>
        /// //读取配置文件中的dnldpath
        /// </summary>
        /// <param name="dnldDialog">如果配置文件不存在，用dialog让用户选择</param>
        /// <returns></returns>
        public static string getDnldPath(FolderBrowserDialog dnldDialog)
        {
            string dnldPath = null;
            if (cfgFile.Exists)
            {
                using (StreamReader sr = new StreamReader(new FileStream(cfgFile.FullName, FileMode.Open, FileAccess.Read)))
                {
                    string tmp = "";
                    while ((tmp = sr.ReadLine()) != null)
                    {
                        if (tmp.StartsWith("path"))
                        {
                            dnldPath = tmp.Split('=')[1];
                            break;
                        }
                    }

                    if(dnldPath != null)
                    {
                        //防止下载目录不存在
                        DirectoryInfo dnldDir = new DirectoryInfo(dnldPath);
                        if (!dnldDir.Exists)
                        {
                            dnldDir.Create();
                        }
                    }
                }
                MyLogger.WriteLine("dnld_path:" + dnldPath);
            }
            else
            {
                MyLogger.WriteLine("configure file not exists!");
                if (dnldDialog.ShowDialog() == DialogResult.OK)
                {
                    dnldPath = dnldDialog.SelectedPath;
                }
            }//dialog ok

            return dnldPath;
        }

        /// <summary>
        /// 读取configure 文件中的当前专注的领域，更好的方便用户使用软件
        /// </summary>
        /// <returns></returns>
        public static string[] getCurFocus()
        {
            string[] curFocus = new string[2];

            if (cfgFile.Exists)
            {
                using (StreamReader sr = new StreamReader(new FileStream(cfgFile.FullName, FileMode.Open, FileAccess.Read)))
                {
                    string tmp = "";
                    while ((tmp = sr.ReadLine()) != null)
                    {
                        if (tmp.StartsWith("pltfm"))
                        {
                            curFocus[0] = tmp.Split('=')[1];
                        }
                        if (tmp.StartsWith("pdct"))
                        {
                            curFocus[1] = tmp.Split('=')[1];
                            break;
                        }
                    }
                }
            }

            return curFocus;
        }
    }
}
