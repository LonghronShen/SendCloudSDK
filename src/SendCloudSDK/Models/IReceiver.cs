using System;
using System.Collections.Generic;
using System.Text;

namespace SendCloudSDK.Models
{

    public interface IReceiver
    {

        bool UseAddressList { get; }

        bool Validate();

    }

}