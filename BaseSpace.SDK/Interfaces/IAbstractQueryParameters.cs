using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK
{
    public interface IAbstractQueryParameters
    {
        int Offset { get; set; }
        int Limit { get; set; }
        SortDirection SortDir { get; set; }
    }
}
