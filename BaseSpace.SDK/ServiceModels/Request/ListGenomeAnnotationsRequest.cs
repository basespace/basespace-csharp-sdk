using Illumina.BaseSpace.SDK.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
