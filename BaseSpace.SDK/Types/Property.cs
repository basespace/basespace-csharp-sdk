﻿using System;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract]
    public class PropertyCompact
    {
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
            string c = string.Empty;
            if (Content != null)
            {
                c = string.Format("Content: {0}", Content.ToString());
            }
            else if(Items != null)
            {
                c = string.Format("#Items: {0}/{1}", ItemsDisplayedCount ?? 0, ItemsTotalCount ?? 0);
            }
            return string.Format("Property: '{0}'; Type: {1}; {2};", Name, Type, c);
        }
    }

    [DataContract]
    public class Property : PropertyCompact
    {
        [DataMember]
        public ApplicationCompact ApplicationModifiedBy { get; set; }

        [DataMember]
        public UserCompact UserModifiedBy { get; set; }

        [DataMember]
        public DateTime DateModified { get; set; }
    }

    public enum PropertiesSortByParameters { Name }
    public enum PropertyItemsSortByParameters { DateCreated }
}
