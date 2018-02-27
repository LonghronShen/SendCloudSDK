using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SendCloudSDK.Utils
{

    internal static class Md5Utils
    {

        public static string MD5Signature(IDictionary<string, string> @params, string secret)
        {
            try
            {
                var orgin = secret + "&" + string.Join("&", @params.Select(x => $"{x.Key}={x.Value}")) + "&" + secret;
                return orgin?.GetMD5HashString();
            }
            catch (Exception ex)
            {
                throw new Exception("sign error!" + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

    }

}