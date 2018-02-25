using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataSyncSystem.Utils
{
    public class CsvParser
    {
        public static List<string> parse(string csvPath)
        {
            FileStream stream = new FileStream(csvPath, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string tmp = "";
            List<string> rows = new List<string>();
            int[] colMaxSpac = null ;
            bool isFirstLine = true;

            while((tmp = sr.ReadLine())!= null)
            {
                rows.Add(tmp);
                if (isFirstLine)
                {
                    colMaxSpac = new int[tmp.Split(',').Length];
                    isFirstLine = false;
                }
                int i = 0;
                foreach(string t in tmp.Split(','))
                {
                    if(t.Length > colMaxSpac[i])
                    {
                        colMaxSpac[i] = t.Length;
                    }

                    i++;
                }
            }
            sr.Close();
            stream.Close();

            MyLogger.WriteLine("col num:" + colMaxSpac.Length);

            //在textbox 中开始画出csv数据表
            List<string> lines = new List<string>() ;
            bool title = true;

            //int colNum = colMaxSpac.Length; //显示所有列时，textbox 不能正确的显示
            int colNum = 82; //能完整显示的textbox 列只有82
            foreach(string line in rows)
            {
                StringBuilder ret = new StringBuilder("");
                for(int i=0; i< colNum; i++)
                {
                    string s = line.Split(',')[i];
                    ret.Append(s);

                    int spac = colMaxSpac[i] - s.Length;
                    while (spac >= 0)
                    {
                        ret.Append(" ");
                        spac--;
                    }
                    if (title)
                    {
                        ret.Append("+");
                    }else
                    {
                        ret.Append("|");
                    }
                }
                
                if (title)
                {
                    title = false;
                    Console.WriteLine("line width:" + ret.Length);
                }
                ret.Append("\r\n");
                lines.Add(ret.ToString());
            }
            return lines;
        }
    }
}
