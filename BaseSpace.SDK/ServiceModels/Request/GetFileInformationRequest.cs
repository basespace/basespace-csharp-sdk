namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetFileInformationRequest : AbstractResourceRequest
    {
        /// <summary>
        /// Get file info
        /// </summary>
        /// <param name="id">File Id</param>
        public GetFileInformationRequest(string id)
        {
            Id = id;
        }
    }
}