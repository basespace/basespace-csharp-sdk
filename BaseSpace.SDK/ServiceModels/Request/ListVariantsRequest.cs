using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListVariantsRequest : AbstractResourceListRequest<ListVariantsResponse, VariantSortByParameters>
    {
        /// <summary>
        /// Get variants by variant Id and chromosome
        /// </summary>
        /// <param name="id">Variant Id</param>
        /// <param name="chrom">Chromosome (ex. chr2)</param>
        public ListVariantsRequest(string id, string chrom) : base(id)
        {
            Chrom = chrom;
        }
        
        public string Chrom { get; set; }

        public string StartPos { get; set; }

        public string EndPos { get; set; }

		protected override string GetUrl()
		{
			return string.Format("{0}/variantset/{1}/variants/{2}", Version, Id, Chrom);
		}
	}
}
