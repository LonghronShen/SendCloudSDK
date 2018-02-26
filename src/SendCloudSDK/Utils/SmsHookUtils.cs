using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SendCloudSDK.Utils
{

    public static class SmsHookUtils
    {

        public static bool Verify(string appkey, string token, long timestamp, string signature)
        {
            var sha256 = new HMACSHA256(Encoding.UTF8.GetBytes(appkey));
            sha256.Initialize();
            var source = $"{timestamp}{token}";
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(source));
            var signatureCal = string.Join("", hash.Select(x => x.ToString("X")));
            return signatureCal.Equals(signature);
        }

    }

}