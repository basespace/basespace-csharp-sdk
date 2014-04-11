using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.ServiceModels
{
    public class CreatePlannedRunRequest : AbstractRequest<CreatePlannedRunResponse>
    {
        public CreatePlannedRunRequest(IEnumerable<PlannedRunPoolMapping> poolMappings, int numCyclesRead1, string name, string platformName, string analysisWorkFlowType, string indexingStrategy)
        {
            ExperimentName = name;
            PlatformName = platformName;
            AnalysisWorkflowType = analysisWorkFlowType;
            NumCyclesRead1 = numCyclesRead1;
            IndexingStrategy = indexingStrategy;
            PoolMappings = poolMappings;
            HttpMethod = HttpMethods.POST;
        }

        public string ExperimentName { get; set; }
        public string ReagentBarcode { get; set; }
        public string FlowcellBarcode { get; set; }
        public string AnalysisWorkflowType { get; set; }
        public string PlatformName { get; set; }
        public string IndexingStrategy { get; set; }
        public int NumCyclesRead1 { get; set; }
        public int NumCyclesRead2 { get; set; }
        public int NumCyclesIndex1 { get; set; }
        public int NumCyclesIndex2 { get; set; }
        public bool CustomPrimerR1 { get; set; }
        public bool CustomPrimerR2 { get; set; }
        public bool CustomPrimerIndex { get; set; }
        public bool AutoRetireOnCompletion { get; set; }
        public string FlowcellFormat { get; set; }
        public string InstrumentRunMode { get; set; }
        public bool OnBoardClusterGeneration { get; set; }
        public bool ConfirmFirstBase { get; set; }
        public UInt16 ControlLane { get; set; }
        public bool OverrideIndexCycles { get; set; }
        public bool Rehyb { get; set; }
        public bool Ready { get; set; }
        public IEnumerable<PlannedRunPoolMapping> PoolMappings { get; set; }

        protected override string GetUrl()
        {
            return string.Format("{0}/plannedruns", Version);
        }
    }
}
