using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class GetLibraryPrepKitIdRequest : AbstractResourceRequest<GetLibraryPrepKitIdResponse>
    {
        /// <summary>
        /// Get specific Library Prep Kit
        /// </summary>
        /// <param name="id">Library Prep Kit Id</param>
        public GetLibraryPrepKitIdRequest(string id)
            : base(id)
        {
        }

        protected override string GetUrl()
        {
            return string.Format("{0}/libraryprepkits/{1}", Version, Id);
        }
    }
}
