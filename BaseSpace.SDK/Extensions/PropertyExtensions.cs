using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK
{
    public static class PropertyContainerExtensions
    {

        public static T PropertyGetReference<T>(this PropertyContainer propertyContainer, string name)
            where T : class, IPropertyContent
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
            return property.ItemsDisplayedCount == property.ItemsTotalCount; // true for single-value properties.
        }

        public static string GetSimpleType(this Property property)
        {
            return property.Type.Replace(Property.TYPE_LIST_SUFFIX, string.Empty);
        }

        public static string[] ToStringArray(this Property property)
        {
            if (property.Type == Property.TYPE_STRING + Property.TYPE_LIST_SUFFIX)
            {
                return property.Items.Select(i => i.ToString()).ToArray();
            }
            return null;
        }

        public static int[] ToIntArray(this Property property)
        {
            if (property.Type == Property.TYPE_STRING + Property.TYPE_LIST_SUFFIX)
            {
                return property.Items.Select(i => i.ToInt()).Where(i => i.HasValue).Select(i => i.Value).ToArray();
            }
            return null;
        }

        public static long[] ToLongArray(this Property property)
        {
            if (property.Type == Property.TYPE_STRING + Property.TYPE_LIST_SUFFIX)
            {
                return property.Items.Select(i => i.ToLong()).Where(i => i.HasValue).Select(i => i.Value).ToArray();
            }
            return null;
        }

        public static TResourceType[] ToResourceArray<TResourceType>(this Property property)
            where TResourceType : class, IPropertyContent
        {
            // TODO (maxm): type check
            if (property.Items != null)
            {
                return property.Items.Select(i => i.ToResource<TResourceType>()).Where(i => i != null).ToArray();
            }
            return null;
        }
    }

    public static class PropertyContentExtensions
    {
        public static bool IsLiteral(this IPropertyContent propertyContent)
        {
            return propertyContent as PropertyContentLiteral != null;
        }

        public static int? ToInt(this IPropertyContent propertyContent)
        {
            if (!propertyContent.IsLiteral())
            {
                return null;
            }
            return ((PropertyContentLiteral) propertyContent).ToInt();
        }

        public static long? ToLong(this IPropertyContent propertyContent)
        {
            if (!propertyContent.IsLiteral())
            {
                return null;
            }
            return ((PropertyContentLiteral) propertyContent).ToLong();
        }

        public static DateTime? ToDateTime(this IPropertyContent propertyContent)
        {
            if (!propertyContent.IsLiteral())
            {
                return null;
            }
            return ((PropertyContentLiteral) propertyContent).ToDateTime();
        }

        /// <summary>
        /// Convert to a compact resource
        /// </summary>
        /// <remarks>
        /// TResourceType must be a compact resource object 
        /// </remarks>
        public static TResourceType ToResource<TResourceType>(this IPropertyContent propertyContent)
            where TResourceType : class, IPropertyContent
        {
            if (propertyContent.IsLiteral())
            {
                return null;
            }
            return propertyContent as TResourceType;
        }

    }
}
