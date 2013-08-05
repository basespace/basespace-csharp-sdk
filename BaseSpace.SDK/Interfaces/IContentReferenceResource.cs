using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK
{
    public interface IContentReferenceResource<T> : IReferenceResource where T : IAbstractResource
    {
        T Content { get; set; }
    }
}
