using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK
{
    public enum ApiNames
    {
        /// <summary>
        /// The standard API for BaseSpace resources. Generally located at HTTPS://api.basespace.illumina.com/
        /// </summary>
        BASESPACE,

        /// <summary>
        /// The BaseSpace Billing API exposing billing related resources. Generally located at HTTPS://store.basespace.illumina.com/
        /// </summary>
        BASESPACE_BILLING
    }
}
