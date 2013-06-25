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
            var refs = references.ReferencesOfValueType<string>();
            if (refs == null)
                return null;

            return refs.ToDictionary(r => r.Name, r => r.Content);
        }

        public static Dictionary<string, string[]> OfTypeStringArray(this IResource[] references)
        {
            var refs = references.ReferencesOfValueType<string[]>();
            if (refs == null)
                return null;

            return refs.ToDictionary(r => r.Name, r => r.Content);
        }

        public static IEnumerable<KeyValuePair<string, IContentReferenceResource<T>>> OfTypeEntityWithName<T>(this IResource[] references) where T : IAbstractResource
        {
            var refs = references.ReferencesOfRefType<T>();
            if (refs == null)
                return null;

            return refs.Select(r => new KeyValuePair<string, IContentReferenceResource<T>>(r.Name, r));
        }

        public static IEnumerable<IResource> OfTypeEntity<T>(this IResource[] references) where T: IAbstractResource
        {
            return references.OfTypeEntityWithName<T>().Select(kvp => kvp.Value);
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
