using System;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract( Name = "User")]
    public class UserCompact : AbstractResource
    {
        [DataMember(IsRequired = true)]
        public override string Id { get; set; }

        [DataMember]
        public override Uri Href { get; set; }

        [DataMember]
        public string Name { get; set; }
    }

    [DataContract()]
    public class User : UserCompact
    {
        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string GravatarUrl { get; set; }

        [DataMember]
        public DateTime? DateLastActive { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public Uri HrefRuns { get; set; }

        [DataMember]
        public Uri HrefProjects { get; set; }

        [DataMember]
        public string[] Roles { get; set; }
    }

    public enum UserSortByParameters { Id, Username, Fullname, DateLastActive, DateCreated }

}
