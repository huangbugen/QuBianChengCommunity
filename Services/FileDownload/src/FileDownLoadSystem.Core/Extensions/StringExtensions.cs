using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDownLoadSystem.Core.Extensions
{
    public static class StringExtensions
    {
        private static DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
        // 表示从 0000年00月00日00：00：00 ~ 1970年01月01日00：00：00的刻度值
        private static long longTime = 621355968000000000;
        // 毫秒 * 10000000 = 纳秒
        private static int samllTime = 10000000;

        public static int GetInt(this object obj)
        {
            if (obj == null)
                return 0;
            _ = int.TryParse(obj.ToString(), out int _number);
            return _number;
        }

        public static DateTime GetTimeStampToDate(this object timeStamp)
        {
            if (timeStamp == null) return dateStart;
            DateTime dateTime = new DateTime(longTime + Convert.ToInt64(timeStamp) * samllTime, DateTimeKind.Utc).ToLocalTime();
            return dateTime;
        }
    }
}