using System;
using System.Collections.Generic;
using System.Text;

namespace SendCloudSDK.Models
{

    public interface IContent
    {

        bool UseTemplate { get; }

        bool Validate();

    }

}