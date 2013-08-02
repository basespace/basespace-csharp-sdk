using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK.Types
{
    public class PropertyContentLiteral : IPropertyContent
    {
        public PropertyContentLiteral() { }

        public PropertyContentLiteral(string type, string content)
        {
            this.Type = type;
            this.Content = content;
        }

        public string Type { get; set; }

        public Uri Href
        {
            get { return new Uri(""); }
        }

        public string Content { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Type, Content);
        }
    }
}
