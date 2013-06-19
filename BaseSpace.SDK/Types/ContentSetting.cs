using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract]
    public class ContentSettingResource<T> : IContentValueResource<T>
    {
        public ContentSettingResource(T resource, string name, string relation, string type)
        {
            Rel = relation;
            Name = name;
            Content = resource;
            Type = type;
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public T Content { get; set; }

        [DataMember]
        public string Rel { get; set; }

        [DataMember]
        public string Type { get; set; }
    }


}
