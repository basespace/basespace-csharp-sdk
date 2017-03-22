using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Illumina.BaseSpace.SDK.Types
{
    public class V2DatasetCompact : AbstractResource
    {
        public override string Id { get; set; }
        public override Uri Href { get; set; }
        public Uri HrefFiles { get; set; }
        public Uri HrefBaseSpaceUI { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public V2AppSessionCompact AppSession { get; set; }
        public ProjectCompact Project { get; set; }
        public long TotalSize { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsFileDataDeleted { get; set; }
        public UserCompact UserOwnedBy { get; set; }
        public V2DatasetTypeCompact DatasetType { get; set; }
        public Dictionary<string, Dictionary<string, object>> Attributes { get; set; }
        public PropertyContainer Properties { get; set; }

        public string QcStatus { get; set; }
        public string QcStatusSummary { get; set; }
        public string UploadStatus { get; set; }
        public string UploadStatusSummary { get; set; }
        public string ValidationStatus { get; set; }
        public string V1pre3Id { get; set; }
        public Uri HrefComments { get; set; }
        public bool ContainsComments { get; set; }
    }

    public class V2Dataset : V2DatasetCompact
    {
        public string[] ValidationsPerformed { get; set; }
    }
}
