using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumina.BaseSpace.SDK.Types;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListSupportedLibraryPrepKitsRequest : AbstractResourceListRequest<ListSupportedLibraryPrepKitsResponse, LibraryPrepKitSortFields>
    {
        [Description("The instrument type to filter by. NeoPrep, NextSeq")]
        public string PlatformName { get; set; }
        /// <summary>
        /// List supported library prep kits
        /// </summary>
        public ListSupportedLibraryPrepKitsRequest()
        {
            
        }

		protected override string GetUrl()
		{
			return String.Format("{0}/libraryprepkits", Version);
		}
	}
}
