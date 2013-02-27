using System;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListGenomeRequest : AbstractResourceListRequest<ListGenomeResponse, GenomeSortByParameters>
    {
		protected override string GetUrl()
		{
			return String.Format("{0}/genomes", Version);
		}
	}
}