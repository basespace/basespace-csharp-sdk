using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract]
    public class OptionItem
    {
        [DataMember]
        public string ItemValue { get; set; }
    }
}
