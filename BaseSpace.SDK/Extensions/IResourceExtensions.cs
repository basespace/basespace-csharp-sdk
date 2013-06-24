using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK.Extensions
{
    public static class ResourcesExtensions
    {
        public static Dictionary<string, string> OfTypeString(this IResource[] references)
        {
            return references.ReferencesOfValueType<string>().ToDictionary(r => r.Name, r => r.Content);
        }

        public static Dictionary<string, string[]> OfTypeStringArray(this IResource[] references)
        {
            return references.ReferencesOfValueType<string[]>().ToDictionary(r => r.Name, r => r.Content);
        }

        public static IEnumerable<KeyValuePair<string, T>> OfTypeEntityWithName<T>(this IResource[] references) where T : IAbstractResource
        {
            return references.ReferencesOfRefType<T>().Select(r => new KeyValuePair<string, T>(r.Name, r.Content));
        }

        public static IEnumerable<T> OfTypeEntity<T>(this IResource[] references) where T : IAbstractResource
        {
            return references.OfTypeEntityWithName<T>().Select(kvp => kvp.Value);
        }

        private static IEnumerable<IContentReferenceResource<T>> ReferencesOfRefType<T>(this IResource[] references) where T : IAbstractResource
        {
            if (references == null)
                return null;
            return references.OfType<IContentReferenceResource<T>>();
        }

        private static IEnumerable<IContentValueResource<T>> ReferencesOfValueType<T>(this IResource[] references)
        {
            if (references == null)
                return null;
            return references.OfType<IContentValueResource<T>>();
        }
    }
}
