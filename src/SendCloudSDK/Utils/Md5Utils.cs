using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SendCloudSDK.Utils
{

    internal static class Md5Utils
    {

        public static string MD5Signature(Dictionary<string, string> @params, string secret)
        {
            try
            {
                var orgin = secret + "&" + string.Join("&", @params.OrderBy(x => x.Key).Select(x => $"{x.Key}={x.Value}"));
                return orgin?.GetMD5HashString();
            }
            catch (Exception ex)
            {
                throw new Exception("sign error!" + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

    }

}