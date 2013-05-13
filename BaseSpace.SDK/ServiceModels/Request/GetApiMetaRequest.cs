using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetApiMeta : AbstractResourceRequest<ApiMeta>
    {
        protected override string GetUrl()
        {
            return string.Format("{0}/meta", Version);
        }

        internal override string GetLogMessage()
        {
            return "";
        }
    }
}
