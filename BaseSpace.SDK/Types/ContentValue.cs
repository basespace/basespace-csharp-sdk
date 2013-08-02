using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract]
    public class ContentValueResource<T> : IContentValueResource<T>
    {
        public ContentValueResource(T resource, string name, string relation, string type=null)
        {
            Rel = relation;
            Name = name;
            Content = resource;

            if (type == null)
            {
                switch (typeof(T).ToString())
                {
                    case "System.String":
                        Type = "String";
                        break;
                    case "System.UInt64":
                    case "System.Int64":
                    case "System.UInt32":
                    case "System.Int32":
                    case "System.Numerics.BigInteger":
                        Type = "Numeric";
                        break;
                    case "System.String[]":
                        Type = "String[]";
                        break;
                }
            }
            else
            {
                Type = type;
            }
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public T Content { get; set; }

        [DataMember]
        public string Rel { get; set; }

        [DataMember]
        public string Type { get; set; }
    }


}
