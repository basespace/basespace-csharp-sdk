using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumina.BaseSpace.SDK.Types;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListSupportedLibraryPrepKitsRequest : AbstractResourceListRequest<ListSupportedLibraryPrepKitsResponse, LibraryPrepKitSortFields>
    {
        /// <summary>
        /// List supported library prep kits
        /// </summary>
        public ListSupportedLibraryPrepKitsRequest()
        {
        }

		protected override string GetUrl()
		{
			return String.Format("{0}/librarypreps", Version);
		}
	}
}
