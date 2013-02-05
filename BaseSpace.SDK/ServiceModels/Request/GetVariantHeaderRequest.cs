namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetVariantHeaderRequest : AbstractResourceRequest
    {
        /// <summary>
        /// Get variant header by Id
        /// </summary>
        /// <param name="id">Variant Id</param>
        public GetVariantHeaderRequest(string id)
        {
            Id = id;
        }
    }
}

