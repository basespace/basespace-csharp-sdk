using System;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK
{
    public interface IPropertyContainingResource
    {
        PropertyContainer Properties { get; set; }
        Uri Href { get; }
    }
}
