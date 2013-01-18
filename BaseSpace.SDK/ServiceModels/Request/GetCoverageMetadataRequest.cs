namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetCoverageMetadataRequest
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

        public string Id { get; set; }
        public string Chrom { get; set; }
    }
}