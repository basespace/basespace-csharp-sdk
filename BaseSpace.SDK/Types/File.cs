using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{

    [DataContract( Name = "File")]
    public class FileCompact : AbstractResource
    {
        [DataMember(IsRequired = true)]
        public override string Id { get; set; }

        [DataMember(IsRequired = true)]
        public override Uri Href { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ContentType { get; set; }

        [DataMember]
        public long Size { get; set; }

        [DataMember]
        public string Path { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }
    }

    [DataContract()]
    public class File : FileCompact
    {
        [DataMember]
        public FileUploadStatus? UploadStatus { get; set; }

        [DataMember]
        public Uri HrefContent { get; set; }

        [DataMember]
        [Description("If set, provides the relative Uri to fetch the variants stored in the file")]
        public Uri HrefVariants { get; set; }

        [DataMember]
        [Description("If set, provides the relative Uri to fetch the mean coverage statistics for data stored in the file")]
        public Uri HrefCoverage { get; set; }

        [DataMember]
        [Description("If set, provides the relative Uri to fetch a list of completed file parts for multi-part file uploads in progress")]
        public Uri HrefParts { get; set; }

        [DataMember]
        public IContentReference<IAbstractResource>[] References { get; set; }
    }

    [DataContract]
    public class FileInformation : FileCompact
    {
        [DataMember]
        public FileUploadStatus? UploadStatus { get; set; }

        [DataMember]
        public Uri HrefContent { get; set; }

        [DataMember]
        public Uri HrefVariants { get; set; }

        [DataMember]
        public Uri HrefCoverage { get; set; }

    }

    public enum FileUploadStatus { undefined, pending, aborted, complete }
    public enum FilesSortByParameters { Id, Path, DateCreated }
}
