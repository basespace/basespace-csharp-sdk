using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
    public enum SortDirection { Asc, Desc }

    public enum QueryParameters
    {
        Offset,
        Limit,
        SortDir,
        SortBy,
        Name
    }

    public class AbstractQueryParameters : IAbstractQueryParameters
    {
        [DataMember]
        public int Offset { get; set; }

        [DataMember]
        public int Limit { get; set; }

        [DataMember]
        public SortDirection SortDir { get; set; }
    }
}
