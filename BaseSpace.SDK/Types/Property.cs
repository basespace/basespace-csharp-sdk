using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract]
    public class Property
    {
        public const string TYPE_STRING = "string";
        public const string TYPE_RUN = "run";
        public const string TYPE_PROJECT = "project";
        public const string TYPE_SAMPLE = "sample";
        public const string TYPE_APPRESULT = "appresult";
        public const string TYPE_FILE = "file";
        public const string TYPE_USER = "user";
        public const string TYPE_APPSESSION = "appsession";
        public const string TYPE_APPLICATION = "application";
        public const string TYPE_LIST_SUFFIX = "[]";

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public Uri Href { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public Uri HrefItems { get; set; }

        [DataMember]
        public int? ItemsDisplayedCount { get; set; }

        [DataMember]
        public int? ItemsTotalCount { get; set; }

        [DataMember]
        public IPropertyContent Content { get; set; }

        [DataMember]
        public IPropertyContent[] Items { get; set; }

        public override string ToString()
        {
            return string.Format("Property: '{0}'; Type: {1}", Name, Type);
        }
    }
}
