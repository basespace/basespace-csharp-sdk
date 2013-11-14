using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract( Name = "File")]
    public class FileCompact : AbstractResource, IPropertyContent
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

        [DataMember]
        public SampleCompact ParentSample { get; set; }

        [DataMember]
        public AppResultCompact ParentAppResult { get; set; }

        [DataMember]
        public RunCompact ParentRun { get; set; }

        [DataMember]
        public GenomeCompact ParentGenome { get; set; }

        public override string ToString()
        {
            return string.Format("Href: {0}; Name: {1}; Path: {2}; Size: {3}", Href, Name, Path, Size);
        }

        public string Type
        {
            get { return PropertyTypes.FILE; }
        }
    }

    [DataContract]
    public class File : FileCompact, IPropertyContainingResource
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

        [DataMember]
        public PropertyContainer Properties { get; set; }
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
