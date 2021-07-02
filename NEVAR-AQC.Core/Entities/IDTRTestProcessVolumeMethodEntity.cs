// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions </Copyright>
//     <Url> http://lehoangtrinh.com </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Core </Project>
//     <File>
//         <Name> IDTRTestProcessVolumeMethodEntity.cs </Name>
//         <Created> 19/6/2019 - 14:51 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//          Bảng Báo cáo tiến trình - Phương pháp thể tích
//     </Summary>
// <License>

using System;
using System.ComponentModel;

namespace NEVAR_AQC.Core.Entities
{
    public class IDTRTestProcessVolumeMethodEntity : ExtensionEntity<long>
    {
        [DefaultValue(false)]
        public bool IsSubmitReport { get; set; } = false;

        public long SpecimenPropertyId { get; set; }

        public string QuantumL1 { get; set; }

        public string QuantumL2 { get; set; }

        public string SolutionName1 { get; set; }

        public string ConcentrationOfSolution1 { get; set; }

        public string SolutionName2 { get; set; }

        public string ConcentrationOfSolution2 { get; set; }

        public string OtherMonitoringData { get; set; }

        public string DilutionCoefficient { get; set; }

        public string T1 { get; set; }

        public string T2 { get; set; }

        public string CalculationFormula { get; set; }

        public string ResultL1 { get; set; }

        public string ResultL2 { get; set; }

        public string ResultT1 { get; set; }

        public string ResultT2 { get; set; }

        public string AverageResultsL { get; set; }

        public string AverageResultsT { get; set; }

        public string PercentOfRevoke { get; set; }

        public string ReportResults { get; set; }

        public DateTime TimeReportResults { get; set; }

        public virtual IDTRTestPropertyEntity IDTRTestPropertyEntity { get; set; }
    }
}