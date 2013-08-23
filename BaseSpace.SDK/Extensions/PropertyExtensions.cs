using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK
{
    public static class PropertyContainerExtensions
    {
        public static bool TryGetProperty(this PropertyContainer propertyContainer, string name, out PropertyCompact property)
        {
            property = propertyContainer.Items.FirstOrDefault(p => p.Name == name);
            return property != null;
        }

        /// <summary>
        /// Returns true if not all properties for the resource have been returned in this request due to paging
        /// </summary>
        public static bool IsTruncated(this PropertyContainer propertyContainer)
        {
            return propertyContainer.DisplayedCount != propertyContainer.TotalCount;
        }

        /// <summary>
        /// Returns true if not all property items for the property have been returned in this request due to paging
        /// </summary>
        public static bool IsTruncated(this PropertyCompact property)
        {
            if (property.Content != null)
            {
                // single-value properties aren't truncated
                return false;
            }

            return property.ItemsDisplayedCount < property.ItemsTotalCount;
        }

        /// <summary>
        /// Returns the property's type for single-value properties, and the underlying type for multi-value properties.
        /// </summary>
        public static string GetUnderlyingType(this PropertyCompact property)
        {
            return (property.Type ?? string.Empty).Replace(PropertyTypes.LIST_SUFFIX, string.Empty);
        }

        /// <summary>
        /// For properties of type 'string[]', returns property items as a string[].
        /// </summary>
        public static string[] ToStringArray(this PropertyCompact property)
        {
            if ((property.Type ?? string.Empty) == PropertyTypes.STRING + PropertyTypes.LIST_SUFFIX)
            {
                return property.Items.Select(i => i.ToString()).ToArray();
            }
            return null;
        }

        /// <summary>
        /// For properties of type 'string[]', returns property items that may be converted to int as a int[]. 
        /// </summary>
        public static int[] ToIntArray(this PropertyCompact property)
        {
            if ((property.Type ?? string.Empty) == PropertyTypes.STRING + PropertyTypes.LIST_SUFFIX)
            {
                return property.Items.Select(i => i.ToInt()).Where(i => i.HasValue).Select(i => i.Value).ToArray();
            }
            return null;
        }

        /// <summary>
        /// For properties of type 'string[]', returns property items that may be converted to long as a long[]. 
        /// </summary>
        public static long[] ToLongArray(this PropertyCompact property)
        {
            if ((property.Type ?? string.Empty) == PropertyTypes.STRING + PropertyTypes.LIST_SUFFIX)
            {
                return property.Items.Select(i => i.ToLong()).Where(i => i.HasValue).Select(i => i.Value).ToArray();
            }
            return null;
        }

        public static PropertyContentMap[] ToMapArray(this PropertyCompact property)
        {
            if ((property.Type ?? string.Empty) == PropertyTypes.MAP + PropertyTypes.LIST_SUFFIX)
            {
                return property.Items.Select(i => i.ToMap()).Where(i => i != null).ToArray();
            }
            return null;
        }

        /// <summary>
        /// For multi-value properties containing resource references, returns property items referencing the given type as an array
        /// </summary>
        public static TResourceType[] ToResourceArray<TResourceType>(this PropertyCompact property)
            where TResourceType : class, IPropertyContent
        {
            if (property.Items != null)
            {
                return property.Items.Select(i => i.ToResource<TResourceType>()).Where(i => i != null).ToArray();
            }
            return null;
        }

        /// <summary>
        /// For multi-value properties of type 'sample[]', returns property items as SampleCompact[]
        /// </summary>
        public static SampleCompact[] ToSampleArray(this PropertyCompact property)
        {
            if ((property.Type ?? string.Empty) != PropertyTypes.SAMPLE + PropertyTypes.LIST_SUFFIX)
            {
                return null;
            }
            return property.ToResourceArray<SampleCompact>();
        }

        /// <summary>
        /// For multi-value properties of type 'appresult[]', returns property items as AppResultCompact[]
        /// </summary>
        public static AppResultCompact[] ToAppResultArray(this PropertyCompact property)
        {
            if ((property.Type ?? string.Empty) != PropertyTypes.APPRESULT + PropertyTypes.LIST_SUFFIX)
            {
                return null;
            }
            return property.ToResourceArray<AppResultCompact>();
        }

        /// <summary>
        /// For multi-value properties of type 'project[]', returns property items as ProjectCompact[]
        /// </summary>
        public static ProjectCompact[] ToProjectArray(this PropertyCompact property)
        {
            if ((property.Type ?? string.Empty) != PropertyTypes.PROJECT + PropertyTypes.LIST_SUFFIX)
            {
                return null;
            }
            return property.ToResourceArray<ProjectCompact>();
        }

        /// <summary>
        /// For multi-value properties of type 'appsession[]', returns property items as AppSessionCompact[]
        /// </summary>
        public static AppSessionCompact[] ToAppSessionArray(this PropertyCompact property)
        {
            if ((property.Type ?? string.Empty) != PropertyTypes.APPSESSION + PropertyTypes.LIST_SUFFIX)
            {
                return null;
            }
            return property.ToResourceArray<AppSessionCompact>();
        }

        /// <summary>
        /// For multi-value properties of type 'run[]', returns property items as RunCompact[]
        /// </summary>
        public static RunCompact[] ToRunsArray(this PropertyCompact property)
        {
            if ((property.Type ?? string.Empty) != PropertyTypes.RUN + PropertyTypes.LIST_SUFFIX)
            {
                return null;
            }
            return property.ToResourceArray<RunCompact>();
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

        public static AppSessionCompact ToAppSession(this IPropertyContent propertyContent)
        {
            return propertyContent.ToResource<AppSessionCompact>();
        }

        public static ProjectCompact ToProject(this IPropertyContent propertyContent)
        {
            return propertyContent.ToResource<ProjectCompact>();
        }

        public static SampleCompact ToSample(this IPropertyContent propertyContent)
        {
            return propertyContent.ToResource<SampleCompact>();
        }

        public static AppResultCompact ToAppResult(this IPropertyContent propertyContent)
        {
            return propertyContent.ToResource<AppResultCompact>();
        }

        public static RunCompact ToRun(this IPropertyContent propertyContent)
        {
            return propertyContent.ToResource<RunCompact>();
        }

        public static PropertyContentMap ToMap(this IPropertyContent propertyContent)
        {
            return propertyContent as PropertyContentMap;
        }
    }

    public static class PropertyItemsResourceListExtensions
    {
        /// <summary>
        /// For multi-value properties containing resource references, returns property items referencing the given type as an array
        /// </summary>
        public static TResourceType[] ToResourceArray<TResourceType>(this PropertyItemsResourceList propertyItemsResourceList)
            where TResourceType : class, IPropertyContent
        {
            if (propertyItemsResourceList.Items != null)
            {
                return propertyItemsResourceList.Items.Select(i => i.Content.ToResource<TResourceType>()).Where(i => i != null).ToArray();
            }
            return null;
        }

        /// <summary>
        /// For multi-value properties of type 'sample[]', returns property items as SampleCompact[]
        /// </summary>
        public static SampleCompact[] ToSampleArray(this PropertyItemsResourceList propertyItemsResourceList)
        {
            if ((propertyItemsResourceList.Type ?? string.Empty) != PropertyTypes.SAMPLE + PropertyTypes.LIST_SUFFIX)
            {
                return null;
            }
            return propertyItemsResourceList.ToResourceArray<SampleCompact>();
        }

        /// <summary>
        /// For multi-value properties of type 'appresult[]', returns property items as AppResultCompact[]
        /// </summary>
        public static AppResultCompact[] ToAppResultArray(this PropertyItemsResourceList propertyItemsResourceList)
        {
            if ((propertyItemsResourceList.Type ?? string.Empty) != PropertyTypes.APPRESULT + PropertyTypes.LIST_SUFFIX)
            {
                return null;
            }
            return propertyItemsResourceList.ToResourceArray<AppResultCompact>();
        }

        /// <summary>
        /// For multi-value properties of type 'project[]', returns property items as ProjectCompact[]
        /// </summary>
        public static ProjectCompact[] ToProjectArray(this PropertyItemsResourceList propertyItemsResourceList)
        {
            if ((propertyItemsResourceList.Type ?? string.Empty) != PropertyTypes.PROJECT + PropertyTypes.LIST_SUFFIX)
            {
                return null;
            }
            return propertyItemsResourceList.ToResourceArray<ProjectCompact>();
        }

        /// <summary>
        /// For multi-value properties of type 'appsession[]', returns property items as AppSessionCompact[]
        /// </summary>
        public static AppSessionCompact[] ToAppSessionArray(this PropertyItemsResourceList propertyItemsResourceList)
        {
            if ((propertyItemsResourceList.Type ?? string.Empty) != PropertyTypes.APPSESSION + PropertyTypes.LIST_SUFFIX)
            {
                return null;
            }
            return propertyItemsResourceList.ToResourceArray<AppSessionCompact>();
        }

        /// <summary>
        /// For multi-value properties of type 'run[]', returns property items as RunCompact[]
        /// </summary>
        public static RunCompact[] ToRunsArray(this PropertyItemsResourceList propertyItemsResourceList)
        {
            if ((propertyItemsResourceList.Type ?? string.Empty) != PropertyTypes.RUN + PropertyTypes.LIST_SUFFIX)
            {
                return null;
            }
            return propertyItemsResourceList.ToResourceArray<RunCompact>();
        }
    }
}
