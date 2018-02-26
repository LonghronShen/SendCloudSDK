using System;
using System.Collections.Generic;
using System.Text;

namespace SendCloudSDK.Exceptions
{

    public class SmsException
        : SCException
    {

        public SmsException(string message)
            : base(message, 1L, 401)
        {
        }

    }

}