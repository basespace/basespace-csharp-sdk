namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetCoverageRequest : AbstractResourceRequest<GetCoverageResponse>
    {
        /// <summary>
        /// Get Coverage by Id and Chromosome
        /// </summary>
        /// <param name="id">File Id</param>
        /// <param name="chrom">Chromosome</param>
        /// <param name="startPos">Chromosome start position</param>
        /// <param name="endPos">Chromosome end position</param>
        public GetCoverageRequest(string id, string chrom, string startPos, string endPos)
			: base(id)
        {
            Chrom = chrom;
            StartPos = startPos;
            EndPos = endPos;
        }

        public string Chrom { get; set; }

        public string StartPos { get; set; }

        public string EndPos { get; set; }

		protected override string GetUrl()
		{
			return string.Format("{0}/coverag/{1}/{2}", Version, Id, Chrom);
		}
	}
}