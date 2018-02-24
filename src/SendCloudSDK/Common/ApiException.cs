using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SendCloudSDK.Common
{
    internal class ApiException : Exception
    {
        public ApiException(string message)
            : base(message)
        {
        }

        public ApiException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
