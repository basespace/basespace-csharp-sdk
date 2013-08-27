using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Illumina.BaseSpace.SDK
{
    public interface IPropertyContent
    {
        string Type { get; }
        Uri Href { get; }
    }
}
