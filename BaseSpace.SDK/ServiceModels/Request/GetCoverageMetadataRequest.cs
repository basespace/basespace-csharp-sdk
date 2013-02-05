namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetCoverageMetadataRequest : AbstractResourceRequest
    {
        /// <summary>
        /// Get Coverage Metadata by Id and Chromosome
        /// </summary>
        /// <param name="id">File Id</param>
        /// <param name="chrom">Chromosome</param>
        public GetCoverageMetadataRequest(string id, string chrom)
        {
            Id = id;
            Chrom = chrom;
        }

        public string Chrom { get; set; }
    }
}