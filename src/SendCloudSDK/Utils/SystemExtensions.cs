using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace System
{

    internal static class SystemExtensions
    {

        public static string ToJson(this object self)
        {
            return JsonConvert.SerializeObject(self);
        }

        public static string GetMD5HashString(this string self)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(self));
            return string.Join("", hash.Select(x => x.ToString("X")));
        }

        public static long ToUnixTimeStamp(this DateTime time)
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (long)(time - startTime).TotalSeconds;
        }

    }

}