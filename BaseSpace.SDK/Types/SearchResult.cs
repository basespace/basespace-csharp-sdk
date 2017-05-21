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
        public SampleFileCompact SampleFile { get; set; }

        [DataMember]
        public AppResultFileCompact AppResultFile { get; set; }

        //[DataMember]
        //public ApplicationCompact Application { get; set; }

        [DataMember]
        public AppResultCompact AppResult { get; set; }

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
                return SampleFile.ToString();
            
            //if (Application != null)
            //{
            //    return Application.ToString();
            //}

            if (AppResult != null)
                return AppResult.ToString();

            if (AppResultFile != null)
                return AppResultFile.ToString();
            
            return base.ToString();
        }
    }

    public enum SearchResultSortByParameters
    {
        Score,
        FileId,
        Project_Name,
        AppResult_Name,
        AppResultFile_Name,
        Sample_Name,
        SampleFile_Name,
        Run_Name,
        DateCreated,
        ModifiedOn,
        Uid
    }
}