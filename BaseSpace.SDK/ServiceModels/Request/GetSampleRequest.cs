using System;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetSampleRequest : AbstractResourceRequest<GetSampleResponse>
    {
        /// <summary>
        /// Get specific sample
        /// </summary>
        /// <param name="id">Sample Id</param>
       public GetSampleRequest(string id)
		   : base(id)
       {
            Id = id;
       }

	   protected override string GetUrl()
	   {
		   return string.Format("{0}/samples/{1}", Version, Id);
	   }

       internal override string GetLogMessage()
       {
           return "";
       }
	}
}
