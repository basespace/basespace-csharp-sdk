using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class CreateLibraryPoolRequest : AbstractRequest<CreateLibraryPoolResponse>
    {
        public CreateLibraryPoolRequest(string userPoolId, IEnumerable<string>libraryIds, string notes = null)
        {
            LibraryIds = libraryIds;
            HttpMethod = HttpMethods.POST;
            UserPoolId = userPoolId;
            if (notes != null)
                Notes = notes;
        }
        public string UserPoolId { get; set; }
        public string Notes { get; set; }
        public IEnumerable<string> LibraryIds { get; set; }

        protected override string GetUrl()
        {
            return string.Format("{0}/librarypools", Version);
        }
    }
}
