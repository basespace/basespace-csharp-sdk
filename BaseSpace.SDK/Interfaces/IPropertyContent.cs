using System;

namespace Illumina.BaseSpace.SDK
{
    public interface IPropertyContent
    {
        string Type { get; }
        Uri Href { get; }
    }
}
