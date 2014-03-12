using System.Runtime.Serialization;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
   
    [DataContract]
    public abstract class AbstractResourceListResponse<TCompactResourceType, TSortFieldEnumType> : ApiResponse<GenericResourceList<TCompactResourceType, TSortFieldEnumType>> where TSortFieldEnumType : struct
    {
        [DataMember(IsRequired = true)]
        public override GenericResourceList<TCompactResourceType, TSortFieldEnumType> Response { get; set; }
    }
}
