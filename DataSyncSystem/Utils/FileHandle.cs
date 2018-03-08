using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataSyncSystem.Utils
{
    public class FileHandle
    {
        //遍历所有的目录名
        public static void traceFolder(DirectoryInfo root,List<DirectoryInfo> sonFolder)
        {
            if (root.GetDirectories().Length > 0)
            {
                foreach (DirectoryInfo d in root.GetDirectories())
                {
                    traceFolder(d,sonFolder);
                }
            }
            else
            {
                sonFolder.Add(root);
            }
        }

        //遍历所有文件
        public static void traceAllFile(DirectoryInfo root, List<FileInfo> files)
        {
            if (root.GetFiles().Length > 0)
            {
                foreach (FileInfo f in root.GetFiles())
                {
                    files.Add(f);
                }
            }

            if (root.GetDirectories().Length > 0)
            {
                foreach (DirectoryInfo d in root.GetDirectories())
                {
                    traceAllFile(d, files);
                }
            }
        }

        // 循环删除目录
        public static void cycDeleteDir(DirectoryInfo dir)
        {
            if (dir.GetDirectories().Length > 0)
            {
                foreach (DirectoryInfo d in dir.GetDirectories())
                {
                    cycDeleteDir(d);
                }
            }

            //删除目录中的文件
            if (dir.GetFiles().Length > 0)
            {
                FileInfo[] files = dir.GetFiles();
                for (int i = 0; i < files.Length; i++)
                    File.Delete(files[i].FullName);
            }
            //删除文件
            Directory.Delete(dir.FullName);
        }

        // 判断待上传的文件夹是否符合规范
        public static bool checkUpldDir(string upldPath,ref int checkCode)
        {
            //check code: 
            /*
            upldDirErr.Add(NULLDIR, "上传目录为空!");  false 
            upldDirErr.Add(NOINFO, "目录中没有info.txt文件!");  false
            upldDirErr.Add(NOSUM, "目录中没有summary.csv文件!"); 
            upldDirErr.Add(NOKEYDIR, "目录中没有[bin,csv,sv]子目录!"); 
            */

            bool canUpld = true;

            if (!checkDirCanWrite(upldPath))
            {
                checkCode = ContantInfo.UpldDir.CANTACCESS;
                return false;
            }

            DirectoryInfo root = new DirectoryInfo(upldPath);
            DirectoryInfo parent = root.Parent;
            List<FileInfo> files = new List<FileInfo>();
            traceAllFile(root, files);
            if(files.Count == 0) // 上传目录中没有一个文件
            {
                checkCode = ContantInfo.UpldDir.NULLDIR;
                return false;
            }
            // 获取子目录
            List<DirectoryInfo> sonFolder = new List<DirectoryInfo>();
            traceFolder(root, sonFolder);

            //检测 info.txt 文件 
            bool hasCsv = false;
            foreach(FileInfo f in root.GetFiles())
            {
                string[] split = f.FullName.Split('.');
                if (split[split.Length - 1].Equals("csv") || split[split.Length - 1].Equals("Csv"))
                {
                    hasCsv = true;
                    break;
                }
            }
            if (!hasCsv)
            {
                checkCode = ContantInfo.UpldDir.NOSUM;
            }
            
            if(sonFolder.Count==1 && sonFolder[0].FullName.Equals(upldPath)) //root没有子目录
            {
                if (root.GetFiles().Length == 0) //root目录中没有文件
                {
                    checkCode = ContantInfo.UpldDir.NULLDIR;
                    canUpld = false;
                }
            }

            return canUpld;
        }

        // 判断一个路径是否可以写入
        public static bool checkDirCanWrite(string path)
        {
            bool canWrite = false;
            try
            {
                string testFile = path + "\\elif.test";
                FileStream fs = new FileStream(testFile, FileMode.Create);
                fs.Close();
                canWrite = true;
                try
                {
                    File.Delete(testFile);
                }
                catch
                {
                    MyLogger.WriteLine(testFile + " delete error!");
                }
            }
            catch (Exception ex)
            {
                MyLogger.WriteLine(path + " test failed!" + ex.Message);
            }
            return canWrite;
        }
    }
}
