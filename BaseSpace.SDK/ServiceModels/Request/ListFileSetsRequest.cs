using Illumina.BaseSpace.SDK.Types;
using System;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public enum FileSetSortFields { Id, DateCreated }

    public class ListGenomeFileSetsRequest : AbstractResourceListRequest<ListGenomeFileSetsResponse, FileSetSortFields>
    {
        public ListGenomeFileSetsRequest(GenomeCompact obj)
        {
            _href = obj.HrefFileSets;
        }
        
        Uri _href;

        protected override string GetUrl()
        {
            return _href.ToString();
        }
        
    }

    public class ListGenomeAnnotationFileSetsRequest : AbstractResourceListRequest<ListGenomeAnnotationFileSetsResponse, FileSetSortFields>
    {
        public ListGenomeAnnotationFileSetsRequest(GenomeAnnotation obj)
        {
            _href = obj.HrefFileSets;
        }
        
        Uri _href;

        protected override string GetUrl()
        {
            return _href.ToString();
        }
    
    }
}
