using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public abstract class AbstractAppIdRequest<TResult, TSortFieldEnumType> : AbstractResourceListRequest<TResult, TSortFieldEnumType>
        where TResult : class
        where TSortFieldEnumType : struct
    {
        protected AbstractAppIdRequest()
        {
        }

        protected AbstractAppIdRequest(string appId)
        {
            AppId = appId;
        }

        [DataMember]
        public string AppId { get; set; }
    }
}