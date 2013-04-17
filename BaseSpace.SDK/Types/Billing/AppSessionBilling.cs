using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Illumina.BaseSpace.SDK.Types
{
    /// <summary>
    /// Lightweight representation of the AppSession providing the most basic details. An AppSession may optionally be included when a purchase is created. 
    /// </summary>
    [DataContract]
    public class AppSessionBilling
    {
        /// <summary>
        /// AppSession Id
        /// </summary>
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// Name of the AppSession
        /// </summary>
        [DataMember]
        public string Name { get; set; }
    }
}
