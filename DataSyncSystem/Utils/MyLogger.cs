using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncSystem.Utils
{
    public static class MyLogger
    {
        public static bool ifShowScreen = true;
        public static string buf = "";
        public static void WriteLine(string message)
        {
            if (!message.Contains("position"))
            {
                buf += message + "\r\n";
            }
            if (ifShowScreen)
            {
                Console.WriteLine(message);
            }
        }
    }
}
