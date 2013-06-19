using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract( Name = "AppSession")]
    public class AppSessionCompact : AbstractResource
    {
        [DataMember(IsRequired = true)]
        public override string Id { get; set; }

        [DataMember(IsRequired = true)]
        public override Uri Href { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public ApplicationCompact Application { get; set; }

        [DataMember]
        public UserCompact UserCreatedBy { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string StatusSummary { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }
    }


    [DataContract]
    public class AppSession : AppSessionCompact
    {
        [DataMember]
        public string OriginatingUri { get; set; }

        [DataMember]
        public IResource[] Resources { get; set; }

        public IEnumerable<IReferenceResource> ReferenceResources
        {
            get
            {
                return ReferencesOfType<SampleCompact>()
                    .Union<IReferenceResource>(ReferencesOfType<FileCompact>())
                    .Union(ReferencesOfType<AppResultCompact>())
                    .Union(ReferencesOfType<ProjectCompact>())
                    .Union(ReferencesOfType<RunCompact>());
            }
        }

        public IEnumerable<IResource> ValueResources
        {
            get
            {
                return ValuesOfType<string>()
                    .Union<IResource>(ValuesOfType<string[]>())
                    .Union(ValuesOfType<BigInteger>());
            }
        }

        public IEnumerable<IContentReferenceResource<T>> ReferencesOfType<T>() where T: IAbstractResource
        {
            return Resources.OfType<IContentReferenceResource<T>>();
        }

        public IEnumerable<IContentValueResource<T>> ValuesOfType<T>()
        {
            return Resources.OfType<IContentValueResource<T>>();
        }
    }

    public enum AppSessionStatus { Running, Complete, NeedsAttention, Aborted }
    public enum AppSessionQueryParameters { Status, StatusSummary }
}
