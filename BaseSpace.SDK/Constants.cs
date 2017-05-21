namespace Illumina.BaseSpace.SDK
{
    public static class PropertyTypes
    {
        public const string STRING = "string";
        public const string RUN = "run";
        public const string PROJECT = "project";
        public const string SAMPLE = "sample";
        public const string APPRESULT = "appresult";
        public const string FILE = "file";
        public const string SAMPLE_FILE = "samplefile";
        public const string APPRESULT_FILE = "appresultfile";
        public const string APPSESSION = "appsession";
        public const string MAP = "map";
        public const string LIST_SUFFIX = "[]";
    } 

    public static class SearchScopes
    {
        public const string RUNS = "runs";
        public const string SAMPLES = "samples";
        public const string APPRESULTS = "appresults";
        public const string PROJECTS = "projects";
        public const string FILES_APPRESULT = "appresult_files";
        public const string FILES_SAMPLE = "sample_files";
        public const string FILES_BOTH_SAMPLE_AND_APPRESULT = "sample_files,appresult_files";
    }

    /// <summary>
    /// Include the const strings to pass to the query
    /// </summary>
    public static class ProjectIncludes
    {
        public const string PERMISSIONS = "Permissions";
    }
}
