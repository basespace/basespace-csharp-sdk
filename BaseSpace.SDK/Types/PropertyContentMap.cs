using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK.Types
{
    public class PropertyContentMap : List<MapTuple>, IPropertyContent
    {
        public string Type
        {
            get { return PropertyTypes.MAP; }
        }

        public Uri Href
        {
            get { return new Uri(""); }
        }

        public PropertyContentMap Add(string key, params string[] values)
        {
            Add(new MapTuple() {Key = key, Values = values});
            return this;
        }

        public MapTuple this[string key]
        {
            get { return this.FirstOrDefault(x => x.Key == key); }
        }
    }

    public class MapTuple
    {
        public string Key { get; set; }
        public string[] Values { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Key, string.Join(",", Values.Select(v => string.Format("'{0}'", v))));
        }
    }
}