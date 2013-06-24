using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK.Extensions
{
    public static class ResourcesExtensions
    {
        public static Dictionary<string, string> OfTypeString(this IEnumerable<IResource> references)
        {
            return references.ReferencesOfValueType<string>().ToDictionary(r => r.Name, r => r.Content);
        }

        public static Dictionary<string, string[]> OfTypeStringArray(this IEnumerable<IResource> references)
        {
            return references.ReferencesOfValueType<string[]>().ToDictionary(r => r.Name, r => r.Content);
        }

        public static Dictionary<string, T> OfTypeEntity<T>(this IEnumerable<IResource> references) where T: IAbstractResource
        {
            return references.ReferencesOfRefType<T>().ToDictionary(r => r.Name, r => r.Content);
        }

        private static IEnumerable<IContentReferenceResource<T>> ReferencesOfRefType<T>(this IEnumerable<IResource> references) where T : IAbstractResource
        {
            if (references == null)
                return null;
            return references.OfType<IContentReferenceResource<T>>();
        }

        private static IEnumerable<IContentValueResource<T>> ReferencesOfValueType<T>(this IEnumerable<IResource> references)
        {
            if (references == null)
                return null;
            return references.OfType<IContentValueResource<T>>();
        }
    }
}
