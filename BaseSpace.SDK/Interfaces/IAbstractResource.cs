using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK
{
    public interface IAbstractResource
    {
        string Id { get; set; }

        Uri Href { get; set; }

    }
}
