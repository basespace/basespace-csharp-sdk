using System;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract]
    public class ContentReference<T> : IContentReference<T>
        where T : AbstractResource
    {

        public ContentReference(T resource, string relation)
        {
            Href = resource.Href;
            HrefContent = resource.Href;
            Rel = relation;
            Content = resource;
            Type = typeof(T).ToString().Replace("Compact", "").Replace("Illumina.BaseSpace.SDK.Types.", "");
        }


        [DataMember]
        public string Rel { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public Uri Href { get; set; }

        [DataMember]
        public Uri HrefContent { get; set; }

        [DataMember]
        public T Content { get; set; }
    }
}
