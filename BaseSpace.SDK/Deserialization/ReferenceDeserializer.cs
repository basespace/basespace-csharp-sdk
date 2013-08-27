using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumina.BaseSpace.SDK.Types;
using ServiceStack.Text;

namespace Illumina.BaseSpace.SDK.Deserialization
{
    public static class ReferenceDeserializer
    {
        public static IContentReference<IAbstractResource> JsonToReference(string jsonString)
        {
            //determine type, then use appropriate deserializer
            var asValues = JsonSerializer.DeserializeFromString<Dictionary<string, string>>(jsonString);

            string type = asValues["Type"];

            switch (type.ToLower())
            {
                case "file":
                    return JsonSerializer.DeserializeFromString<ContentReference<FileCompact>>(jsonString);

                case "appresult":
                    return JsonSerializer.DeserializeFromString<ContentReference<AppResultCompact>>(jsonString);

                case "sample":
                    return JsonSerializer.DeserializeFromString<ContentReference<SampleCompact>>(jsonString);

                case "project":
                    return JsonSerializer.DeserializeFromString<ContentReference<ProjectCompact>>(jsonString);

                case "run":
                    return JsonSerializer.DeserializeFromString<ContentReference<RunCompact>>(jsonString);
            }

            return null;
        }

    }
}
