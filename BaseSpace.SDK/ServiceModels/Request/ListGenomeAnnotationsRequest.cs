using Illumina.BaseSpace.SDK.Types;
using System;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListGenomeAnnotationsRequest : AbstractResourceListRequest<ListGenomeAnnotationsResponse, GenomeAnnotationSortByParameters>
    {
        Uri _href;
        public ListGenomeAnnotationsRequest(GenomeCompact owner)
            : base(owner.Id)
        {
            _href = owner.HrefAnnotations;
        }

        protected override string GetUrl()
        {
            return _href.ToString();
        }
    }
}
