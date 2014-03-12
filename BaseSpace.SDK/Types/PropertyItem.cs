using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract]
    public class PropertyItem
    {
        public PropertyItem() { }

        public PropertyItem(string id, IPropertyContent content)
        {
            Id = id;
            Content = content;
        }

        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public IPropertyContent Content { get; set; }

        public override string ToString()
        {
            return Content != null ? Content.ToString() : null;
        }
    }
}
