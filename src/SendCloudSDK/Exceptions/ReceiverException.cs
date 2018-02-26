using System;
using System.Collections.Generic;
using System.Text;

namespace SendCloudSDK.Exceptions
{

    public class ReceiverException
        : SCException
    {

        public ReceiverException(string message)
            : base(message, 1L, 301)
        {
        }

    }

}