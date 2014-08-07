using System;

namespace Illumina.BaseSpace.SDK.Models
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ApiCategoryAttribute : Attribute
    {
        public string Category { get; set; }
        public ApiCategoryAttribute(string category) { Category = category; }
    }
}
