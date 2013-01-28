using System;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract()]
    public class ReferenceWithProjectContent :  IContentReference<ProjectCompact>
    {
        [DataMember]
        public string Rel { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public Uri Href { get; set; }

        [DataMember]
        public Uri HrefContent { get; set; }
        [DataMember]
        public ProjectCompact Content { get; set; }
    }

    [DataContract()]
    public class ReferenceWithFileContent :  IContentReference<FileCompact>
    {
        [DataMember]
        public string Rel { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public Uri Href { get; set; }

        [DataMember]
        public Uri HrefContent { get; set; }

        [DataMember]
        public FileCompact Content { get; set; }
    }

    [DataContract()]
    public class ReferenceWithRunContent : IContentReference<RunCompact>
    {
        [DataMember]
        public string Rel { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public Uri Href { get; set; }

        [DataMember]
        public Uri HrefContent { get; set; }

        [DataMember]
        public RunCompact Content { get; set; }
    }


    [DataContract()]
    public class ReferenceWithSampleContent :  IContentReference<SampleCompact>
    {
        [DataMember]
        public string Rel { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public Uri Href { get; set; }

        [DataMember]
        public Uri HrefContent { get; set; }
      
        [DataMember]
        public SampleCompact Content { get; set; }

    }

    [DataContract()]
    public class ReferenceWithAppResultContent :  IContentReference<AppResultCompact>
    {
        [DataMember]
        public string Rel { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public Uri Href { get; set; }

        [DataMember]
        public Uri HrefContent { get; set; }

        [DataMember]
        public AppResultCompact Content { get; set; }

    }

  

}
