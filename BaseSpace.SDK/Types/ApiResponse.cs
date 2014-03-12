using System.Collections.Generic;
using System.Runtime.Serialization;
using ServiceStack.ServiceInterface.ServiceModel;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract]
    public class ApiResponse<TResponse> : IApiResponse<TResponse>
    {
        private IList<INotification<object>> notifications = new List<INotification<object>>();
        private ResponseStatus status = new ResponseStatus();

        public ApiResponse() { }

        public ApiResponse(TResponse responseContent)
        {
            Response = responseContent;
        }

        [DataMember(IsRequired = true)]
        public virtual TResponse Response { get; set; }

        [DataMember(IsRequired = true)]
        public ResponseStatus ResponseStatus
        {
            get { return status; }
            set { status = value; }
        }

        [DataMember(IsRequired = true)]
        public IList<INotification<object>> Notifications
        {
            get { return notifications; }
            set { notifications = value; }
        }
    }
}
