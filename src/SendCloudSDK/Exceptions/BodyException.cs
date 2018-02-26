using System;
using System.Collections.Generic;
using System.Text;

namespace SendCloudSDK.Exceptions
{

    public class BodyException
        : SCException
    {

        public BodyException(string message)
            : base(message, 1L, 303)
        {
        }

    }

}