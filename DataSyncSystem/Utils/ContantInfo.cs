using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncSystem.Utils
{
    public class ContantInfo
    {
        public static class Database
        {
            public static string CONSQLSTR = "server=localhost;" +
                "database=datasync;uid=root;" +
                "pwd=Sql@My_!;" +
                "charset=utf8;";
        }

        public static class SockServ
        {
            //public static string ip = "10.113.200.34";
            //public static string ip = "192.168.0.101";
            public static string ip = "127.0.0.1";
            public static string port = "5000";
        }

        public static class UpldDir
        {
            public static Dictionary<int, string> upldDirErrDict = new Dictionary<int, string>();
            public static int NULLDIR = 1;
            public static int NOINFO = 2;
            public static int NOSUM = 3;
            public static int NOKEYDIR = 4;
            public static int CANTACCESS = 5;

            static UpldDir()
            {
                upldDirErrDict.Add(NULLDIR, "上传目录为空!");
                upldDirErrDict.Add(NOINFO, "目录中没有info.txt文件!");
                upldDirErrDict.Add(NOSUM, "目录中没有summary.csv文件!");
                upldDirErrDict.Add(NOKEYDIR, "目录中没有[bin,csv,sv]子目录!");
                upldDirErrDict.Add(CANTACCESS, "对该目录没有权限");
            }
        }

        public static class Compress
        {
            public static Dictionary<int, string> compErrDict = new Dictionary<int, string>();
            public static int WAIT = -1;
            public static int ERROR = 0;
            public static int PRESSOK = 1;

            static Compress()
            {
                compErrDict.Add(WAIT, "正等待压缩！");
                compErrDict.Add(ERROR, "压缩出错！");
                compErrDict.Add(PRESSOK, "压缩完成!");
            }
        }
        
    }
}
