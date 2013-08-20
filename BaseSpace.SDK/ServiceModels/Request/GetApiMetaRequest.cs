namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetApiMetaRequest : AbstractResourceRequest<GetApiMetaResponse>
    {
        protected override string GetUrl()
        {
            return string.Format("{0}/meta", Version);
        }
    }
}
