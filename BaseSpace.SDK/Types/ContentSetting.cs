using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract]
    public class ContentSetting<T> : IContentSetting<T>
    {
        public ContentSetting(T resource, string name, string relation)
        {
            Rel = relation;
            Name = name;
            Content = resource;
            Type = typeof(T).ToString().Replace("Illumina.BaseSpace.SDK.Types.", "");
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
