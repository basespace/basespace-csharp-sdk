using System;

namespace Illumina.BaseSpace.SDK
{
    public interface IAbstractResource
    {
        string Id { get; set; }

        Uri Href { get; set; }

    }
}
