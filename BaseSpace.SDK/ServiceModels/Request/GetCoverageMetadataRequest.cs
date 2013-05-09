using System;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetCoverageMetadataRequest : AbstractResourceRequest<GetCoverageMetadataResponse>
    {
        /// <summary>
        /// Get Coverage Metadata by Id and Chromosome
        /// </summary>
        /// <param name="id">File Id</param>
        /// <param name="chrom">Chromosome</param>
        public GetCoverageMetadataRequest(string id, string chrom)
			: base(id)
        {
            Chrom = chrom;
        }

        public string Chrom { get; set; }

		protected override string GetUrl()
		{
			return String.Format("{0}/coverag/{1}/{2}/meta", Version, Id, Chrom);
		}

        internal override string GetLogMessage()
        {
            return "";
        }
	}
}