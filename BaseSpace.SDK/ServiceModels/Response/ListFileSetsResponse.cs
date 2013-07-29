using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListGenomeFileSetsResponse : AbstractResourceListResponse<GenomeFileSet, FileSetSortFields>
    { }
    public class ListGenomeAnnotationFileSetsResponse : AbstractResourceListResponse<GenomeAnnotationFileSet, FileSetSortFields>
    { }
}
