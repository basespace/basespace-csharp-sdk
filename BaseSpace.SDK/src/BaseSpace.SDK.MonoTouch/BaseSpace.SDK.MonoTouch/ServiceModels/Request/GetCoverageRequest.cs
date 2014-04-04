namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetCoverageRequest
    {
        /// <summary>
        /// Get Coverage by Id and Chromosome
        /// </summary>
        /// <param name="id">File Id</param>
        /// <param name="chrom">Chromosome</param>
        /// <param name="startPos">Chromosome start position</param>
        /// <param name="endPos">Chromosome end position</param>
        public GetCoverageRequest(string id, string chrom, string startPos, string endPos)
        {
            Id = id;
            Chrom = chrom;
            StartPos = startPos;
            EndPos = endPos;
        }

        public string Id { get; set; }
        public string Chrom { get; set; }
        public string StartPos { get; set; }
        public string EndPos { get; set; }
    }
}