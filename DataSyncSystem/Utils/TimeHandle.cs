using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncSystem.Utils
{
    public static class TimeHandle
    {
        /// <summary>
        /// 将给定的毫秒数转换成DateTime
        /// </summary>
        public static DateTime milSecondsToDatetime(long milliseconds)
        {
            DateTime dt_1970 = new DateTime(1970, 1, 1);
            //// .net开发中计算的都是标准时区的差，但java的解析时间跟时区有关，
            // 而我们的java服务器系统时区不是标准时区，解析时间会差8个小时。
            TimeSpan span = TimeSpan.FromMilliseconds(milliseconds) + TimeSpan.FromHours(8);

            return dt_1970 + span;
        }

        /// <summary>
        /// 将给定的DateTime转换成毫秒
        /// </summary>
        public static long datetimeToMilSeconds(DateTime dt)
        {
            DateTime dt_1970 = new DateTime(1970, 1, 1);
            TimeSpan span = dt - dt_1970;
            // .net开发中计算的都是标准时区的差，但java的解析时间跟时区有关，
            // 而我们的java服务器系统时区不是标准时区，解析时间会差8个小时。
            span -= TimeSpan.FromHours(8);

            return (long)span.TotalMilliseconds;
        }
    }
}
