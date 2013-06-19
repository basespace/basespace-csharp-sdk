using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK
{
    public interface IContentSetting<T> : ISetting
    {
        T Content { get; set; }
    }
}
