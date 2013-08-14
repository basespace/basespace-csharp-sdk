using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract]
    public class PropertyItem
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public IPropertyContent Content { get; set; }

        public override string ToString()
        {
            return Content != null ? Content.ToString() : null;
        }
    }
}
