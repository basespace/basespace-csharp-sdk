using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumina.BaseSpace.SDK.Types;
using ServiceStack.Text;

namespace Illumina.BaseSpace.SDK.Deserialization
{
    public static class MiscDeserializers
    {
        private static readonly JsonSerializer<Notification<Agreement>> agreementSerializer = new JsonSerializer<Notification<Agreement>>();

        private static readonly JsonSerializer<Notification<ScheduledDowntime>> scheduledSerializer = new JsonSerializer<Notification<ScheduledDowntime>>();

        public static INotification<object> NotificationDeserializer(string source)
        {
            //determine type, then use appropriate deserializer
            var asValues = JsonSerializer.DeserializeFromString<Dictionary<string, string>>(source);
            string type = asValues["Type"];

            object o = null;

            switch (type.ToLower())
            {
                case "agreement":
                    o = agreementSerializer.DeserializeFromString(source).Item;
                    break;
                case "scheduleddowntime":
                    o = scheduledSerializer.DeserializeFromString(source).Item;
                    break;
            }
            return new Notification<object> { Item = o, Type = type };
        }


    }
}
