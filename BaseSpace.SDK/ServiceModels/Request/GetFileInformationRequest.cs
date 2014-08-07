using System;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetFileInformationRequest : AbstractResourceRequest<GetFileInformationResponse>
    {
        /// <summary>
        /// Get file info
        /// </summary>
        /// <param name="id">File Id</param>
        public GetFileInformationRequest(string id)
            : base(id)
        {
        }

        protected override string GetUrl()
        {
            return String.Format("{0}/files/{1}", Version, Id);
        }
    }
}