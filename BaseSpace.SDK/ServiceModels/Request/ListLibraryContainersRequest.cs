using System;
using Illumina.BaseSpace.SDK.Types;
using System.ComponentModel;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class ListLibraryContainersRequest : AbstractResourceListRequest<ListLibraryContainersResponse, LibraryContainerSortFields>
    {
        [Description("The PrepRunId to filter by.")]
		public string PrepRunId { get; set; }

        protected override string GetUrl()
        {
            return String.Format("{0}/users/current/librarycontainers", Version);
        }
	}
}
