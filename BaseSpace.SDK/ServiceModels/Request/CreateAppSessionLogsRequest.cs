using Illumina.BaseSpace.SDK.Types;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    [DataContract]
    public class CreateAppSessionLogsRequest : AbstractRequest<CreateAppSessionLogsResponse>
    {
        string _logsHref;
        public CreateAppSessionLogsRequest(AppSession session)
        {
            _logsHref = string.Format("{0}/logs", session.Href);
            HttpMethod = HttpMethods.POST;
        }
        protected override string GetUrl()
        {
            return _logsHref;
        }
    }
}
