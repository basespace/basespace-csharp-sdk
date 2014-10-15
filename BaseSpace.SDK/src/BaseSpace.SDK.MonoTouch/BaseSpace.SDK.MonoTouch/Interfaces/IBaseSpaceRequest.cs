using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK
{
    public interface IBaseSpaceRequest
    {
        string GenerateUrl(string version);
    }
}
