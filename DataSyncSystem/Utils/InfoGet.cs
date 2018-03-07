using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataSyncSystem.Utils
{
    public static class InfoGet
    {
        public static List<string> getHeadList(string infoFilePath)
        {
            List<string> ret = new List<string>();
            using (FileStream fs = new FileStream(infoFilePath, FileMode.Open, FileAccess.Read))
            {
                StreamReader sr = new StreamReader(fs);
                string tmp = "";
                while ((tmp = sr.ReadLine()) != null)
                {
                    //不是注释，不是空行
                    if (!tmp.StartsWith("#") && !tmp.Equals(""))
                    {
                        MyLogger.WriteLine("info line:" + tmp);
                        ret.Add(tmp);
                    }
                }
            }
            return ret;
        }
    }
}
