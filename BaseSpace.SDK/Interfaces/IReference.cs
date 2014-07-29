using System;

namespace Illumina.BaseSpace.SDK
{

    public interface IReference
    {
        string Rel { get; set; }

        string Type { get; set; }

        Uri Href { get; set; }

        Uri HrefContent { get; set; }
    }

    public interface IContentReference<out T> : IReference where T : IAbstractResource
    {
        T Content { get; }
    }

}
