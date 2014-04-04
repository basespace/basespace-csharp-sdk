using System;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract()]
    public class AgreementCompact : AbstractResource
    {
        [DataMember]
        public override string Id { get; set; }

        [DataMember]
        public override Uri Href { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public string AgreementStatus { get; set; }

        [DataMember]
        public DateTime? DateSigned { get; set; }
    }

    [DataContract()]
    public class Agreement : AgreementCompact
    {
        [DataMember]
        public string RichText { get; set; }

        [DataMember]
        public string Html { get; set; }
    }
}
