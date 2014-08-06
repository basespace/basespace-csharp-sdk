namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetGenomeRequest : AbstractResourceRequest<GetGenomeResponse>
    {
        /// <summary>
        /// Get specific Genome
        /// </summary>
        /// <param name="id">Genome Id</param>
        public GetGenomeRequest(string id)
            : base(id)
        {
        }

        protected override string GetUrl()
        {
            return string.Format("{0}/genomes/{1}", Version, Id);
        }
    }
}