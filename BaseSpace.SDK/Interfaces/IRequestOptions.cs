using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK
{
    public interface IRequestOptions
    {
        uint RetryAttempts { get;}

        string AccessToken { get; }

        string BaseUrl { get; }


    }
}
