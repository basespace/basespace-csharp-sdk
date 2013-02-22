namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetVariantHeaderRequest : AbstractResourceRequest<GetVariantHeaderResponse>
    {
        /// <summary>
        /// Get variant header by Id
        /// </summary>
        /// <param name="id">Variant Id</param>
        public GetVariantHeaderRequest(string id)
			: base(id)
        {
        }

		protected override string GetUrl()
		{
			return string.Format("{0}/variantset/{1}", Version, Id);
		}
	}
}

