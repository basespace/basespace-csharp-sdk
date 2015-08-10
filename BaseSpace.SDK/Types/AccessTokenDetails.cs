using System;
using System.Runtime.Serialization;


namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract]
    public class AccessTokenDetails : AbstractResource
    {
        [DataMember(IsRequired = true)]
        public override string Id { get; set; }

        [DataMember]
        public override Uri Href { get; set; }

        [DataMember]
        public string AccessToken { get; set; }

        [DataMember]
        public ApplicationCompact Application { get; set; }

        [DataMember]
        public User UserResourceOwner { get; set; }

        [DataMember]
        public String Status { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public string[] Scopes { get; set; }

    }
}
