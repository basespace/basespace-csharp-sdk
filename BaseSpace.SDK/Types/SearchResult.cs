using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract]
    public class SearchResult
    {
        [DataMember]
        public string Type { get; set; }

        //[DataMember]
        //public List<ProjectCompact> ParentProjects { get; set; }

        //[DataMember]
        //public AppResult ParentAppResult { get; set; }

        //[DataMember]
        //public SampleCompact Sample { get; set; }

        [DataMember]
        public FileCompact SampleFile { get; set; }
        [DataMember]
        public FileCompact AppResultFile { get; set; }

        //[DataMember]
        //public ApplicationCompact Application { get; set; }

        public override string ToString()
        {
            //if (ParentProjects != null)
            //{
            //    return ParentProjects.Aggregate("", (current, project) => current + project.ToString());
            //}
            //if (ParentAppResult != null)
            //{
            //    return ParentAppResult.ToString();
            //}
            //if (Sample != null)
            //{
            //    return Sample.ToString();
            //}
            if (SampleFile != null)
            {
                return SampleFile.ToString();
            }
            //if (Application != null)
            //{
            //    return Application.ToString();
            //}
            return base.ToString();
        }
    }

    public enum SearchResultSortByParameters { Id, Name, DateCreated }
}