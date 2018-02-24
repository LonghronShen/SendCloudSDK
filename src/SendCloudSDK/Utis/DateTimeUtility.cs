using System;

namespace SendCloudSDK.Utis
{
    public static class DateTimeUtility
    {
        /// <summary>
        /// unix时间戳转换成日期
        /// </summary>
        public static DateTime FromDateTime(this long datetime)
        {
            //x10000才能转成*100毫微秒
            datetime *= 10000;
            var ts = new TimeSpan(datetime);
            var dt = new DateTime(1970, 1, 1, 8, 0, 0, DateTimeKind.Utc);
            dt = dt.Add(ts);
            return dt;
        }

        public static DateTime FromDateTime(this string timespan)
        {
            long lDatetime = Int64.Parse(timespan);
            return FromDateTime(lDatetime);
        }

        /// <summary>
        /// unix时间戳转换成日期
        /// </summary>
        public static DateTime? ToNullableDateTime(long? datetime)
        {
            //x10000才能转成*100毫微秒
            var time = datetime == null ? null : (DateTime?)((long)datetime).FromDateTime();
            return time;
        }

        /// <summary>
        /// 日期转换成unix时间戳
        /// </summary>
        public static long ToUnixTimespan(this DateTime dateTime)
        {
            var epoch = new DateTime(1970, 1, 1, 8, 0, 0, DateTimeKind.Utc);
            return (long)(dateTime - epoch).TotalMilliseconds;
        }

        public static long ToUnixTimespan(this string dateTime)
        {
            DateTime outDateTime;
            if (!DateTime.TryParse(dateTime, out outDateTime))
            {
                return 0;
            }

            return ToUnixTimespan(outDateTime);
        }

        /// <summary>
        /// 日期转换成unix时间戳
        /// </summary>
        public static long? ToNullableUnixTimespan(DateTime? dateTime)
        {
            var time = dateTime == null ? null : (long?)((DateTime)dateTime).ToUnixTimespan();
            return time;
        }

        public static string ToStandardFormat(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

    }
}
