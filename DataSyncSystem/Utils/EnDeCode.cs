using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncSystem.Utils
{
    public class EnDeCode
    {
        static List<char> codes = new List<char>();
        static int listIndex = 0;
        static Dictionary<char, int> lightDict = new Dictionary<char, int>();
        static Dictionary<char, int> darkDict = new Dictionary<char, int>();

        static EnDeCode()
        {
            init(34);
        }
        private static void init(int times)
        {
            //small
            for (int i = 97; i <= 110; i++) //122
            {
                codes.Add((char)i);
                lightDict.Add((char)i, listIndex);
                listIndex++;
            }
            // big
            for (int i = 65; i <= 78; i++) //90
            {
                codes.Add((char)i);
                lightDict.Add((char)i, listIndex);
                listIndex++;
            }

            // num
            for (int i = 48; i <= 53; i++) //57
            {
                codes.Add((char)i);
                lightDict.Add((char)i, listIndex);
                listIndex++;
            }

            // -big
            for (int i = 79; i <= 90; i++) //90
            {
                codes.Add((char)i);
                lightDict.Add((char)i, listIndex);
                listIndex++;
            }
            // -num
            for (int i = 54; i <= 57; i++) //57
            {
                codes.Add((char)i);
                lightDict.Add((char)i, listIndex);
                listIndex++;
            }
            // -small
            for (int i = 111; i <= 122; i++) //122
            {
                codes.Add((char)i);
                lightDict.Add((char)i, listIndex);
                listIndex++;
            }

            char[] arr = new char[codes.Count];
            int index = 0;
            foreach (char c in codes)
                arr[index++] = c;

            //移位
            int shiftTimes = times;
            for (int i = 0; i < shiftTimes; i++)
            {
                char first = arr[0];
                for (int j = 0; j < arr.Length; j++)
                {
                    if (j + 1 != arr.Length)
                        arr[j] = arr[j + 1];
                    else
                        arr[j] = first;
                }
            }

            for (int i = 0; i < arr.Length; i++)
            {
                darkDict.Add(arr[i], i);
            }
        }

        public static string encode(string str)
        {
            if (!str.Contains("_"))
            {
                throw new Exception("original string format error!");
            }
            else
            {
                string[] splits = str.Split('_');
                str = splits[1] + "_" + splits[0];
            }

            StringBuilder encSb = new StringBuilder();
            foreach (char c in str)
            {
                if (c != '_')
                    encSb.Append(darkDict.ElementAt(lightDict[c]).Key);
                else
                    encSb.Append(c);
            }
            return encSb.ToString();
        }
        public static string decode(string str)
        {
            if (!str.Contains("_"))
            {
                throw new Exception("original string format error!");
            }
            else
            {
                string[] splits = str.Split('_');
                str = splits[1] + "_" + splits[0];
            }
            StringBuilder decSb = new StringBuilder();
            foreach (char c in str)
            {
                if (c != '_')
                    decSb.Append(lightDict.ElementAt(darkDict[c]).Key);
                else
                    decSb.Append(c);
            }
            return decSb.ToString();
        }

    }
}
