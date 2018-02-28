using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncSystem.Utils
{
    public class EnDeCode
    {
        private static char[] codes = null;
        private static Dictionary<char, char> dict = new Dictionary<char, char>();
        public static void init()
        {
            dict.Add('G', '0');
            dict.Add('k', '1');
            dict.Add('B', '2');
            dict.Add('C', '3');
            dict.Add('d', '4');
            dict.Add('Y', '5');
            dict.Add('z', '6');
            dict.Add('f', '7');
            dict.Add('T', '8');
            dict.Add('t', '9');
            dict.Add('u', '_');
            codes = new char[] { 'G', 'k', 'B', 'C', 'd', 'Y', 'z', 'f', 'T', 't' };
        }

        static EnDeCode()
        {
            init();
        }

        public static string enCode(string numStr)
        {
            if (!numStr.Contains("_"))
            {
                throw new Exception("待编码的字符串格式错误!");
            }
            else
            {
                string res = "";
                numStr = numStr.Split('_')[1] + "_" + numStr.Split('_')[0];
                foreach (char c in numStr)
                {
                    if (c >= '0' && c <= '9')
                    {
                        res += codes[Int32.Parse(c + "")];
                    }
                    else if (c == '_')
                    {
                        res += 'u';
                    }
                    else
                    {
                        throw new Exception("待编码的字符串格式错误!");
                    }
                }
                return res;
            }
        }

        public static string deCode(string codeStr)
        {
            string res = "";
            foreach (char c in codeStr)
            {
                res += dict[c];
            }
            try
            {
                res = res.Split('_')[1] + "_" + res.Split('_')[0];
            }
            catch
            {
                res = "";
                throw new Exception("解码错误!");
            }
            return res;
        }
    }
}
