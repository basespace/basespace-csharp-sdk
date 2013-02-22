using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListGenomeRequest : AbstractResourceListRequest<ListGenomeResponse, GenomeSortByParameters>
    {
		protected override string GetUrl()
		{
			var urlWithParameters = AddDefaultQueryParameters(string.Format("{0}/genomes", Version), Offset,
									 Limit, SortDir);
			if (SortBy.HasValue)
			{
				urlWithParameters = string.Format("{0}&{1}={2}", urlWithParameters, QueryParameters.SortBy, SortBy);
			}

			return urlWithParameters;
		}
	}
}