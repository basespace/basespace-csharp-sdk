using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK
{
    public interface IResource
    {
        string Name { get; set; }

        string Rel { get; set; }

        string Type { get; set; }
    }

    public interface IReferenceResource : IResource
    {
        Uri Href { get; set; }

        Uri HrefContent { get; set; }
    }
}
