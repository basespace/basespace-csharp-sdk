using System;

namespace Illumina.BaseSpace.SDK.Types
{
    public class PropertyContentLiteral : IPropertyContent
    {
        public PropertyContentLiteral() { }

        public PropertyContentLiteral(string type, string content)
        {
            Type = type;
            Content = content;
        }

        public string Type { get; set; }

        public Uri Href
        {
            get { return new Uri(""); }
        }

        public string Content { get; set; }

        public override string ToString()
        {
            return Content;
        }

        public int? ToInt()
        {
            int ret;
            if (int.TryParse(Content, out ret))
            {
                return ret;
            }
            return null;
        }

        public long? ToLong()
        {
            long ret;
            if (long.TryParse(Content, out ret))
            {
                return ret;
            }
            return null;
        }

        public DateTime? ToDateTime()
        {
            DateTime ret;
            if (DateTime.TryParse(Content, out ret))
            {
                return ret;
            }
            return null;
        }
    }
}
