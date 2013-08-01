using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK
{
    public interface IPropertyContent
    {
        string Type { get; }
    }

    public class PropertyContentLiteral : IPropertyContent
    {
        public PropertyContentLiteral(){}

        public PropertyContentLiteral(string type, string content)
        {
            this.Type = type;
            this.Content = content;
        }

        public string Type { get; set; }

        public string Content { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Type, Content);
        }
    }
}
