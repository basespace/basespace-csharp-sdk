using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK
{
    public static class PropertyExtensions
    {
        public static Property[] FilterByLiteral(this PropertyContainer propertyContainer)
        {
            return null;
        }

        public static T PropertyGetReference<T>(this PropertyContainer propertyContainer, string name) where T : class, IPropertyContent
        {
            propertyContainer.Items.Where(p => p.Name == name);
            return null;
        }

        public static bool IsFullList(this PropertyContainer propertyContainer)
        {
            return propertyContainer.DisplayedCount == propertyContainer.TotalCount;
        }


        public static bool IsFullList(this Property property)
        {
            return property.ItemsDisplayedCount  == property.ItemsTotalCount; // true for single-value properties.
        }

        public static string GetSimpleType(this Property property)
        {
            return property.Type.Replace(Property.TYPE_LIST_SUFFIX, string.Empty);
        }
    }
}
