using NEVAR_AQC.Core.Entities;
using System;
using System.ComponentModel;

namespace NEVAR_AQC.Core.Models.TestDepartment
{
    public class IDTRTestProcessAASUCVISAESMethodModel : ExtensionEntity<long>
    {
        [DefaultValue(false)]
        public bool IsSubmitReport { get; set; } = false;

        public long SpecimenPropertyId { get; set; }

        public string QuantumL1 { get; set; }

        public string QuantumL2 { get; set; }

        public string SymbolC { get; set; }

        public string ValueC1 { get; set; }

        public string ValueC2 { get; set; }

        public string ValueC3 { get; set; }

        public string ValueC4 { get; set; }

        public string ValueC5 { get; set; }

        public string ValueC6 { get; set; }

        public string ValueC7 { get; set; }

        public string AbsorbanceC1 { get; set; }

        public string AbsorbanceC2 { get; set; }

        public string AbsorbanceC3 { get; set; }

        public string AbsorbanceC4 { get; set; }

        public string AbsorbanceC5 { get; set; }

        public string AbsorbanceC6 { get; set; }

        public string AbsorbanceC7 { get; set; }

        public string StandardLineEquation { get; set; }

        public string CoefficientR2 { get; set; }

        public string ExtraStandardConcentration { get; set; }

        public string T1 { get; set; }

        public string T2 { get; set; }

        public string MeasurementResultsL1 { get; set; }

        public string MeasurementResultsL2 { get; set; }

        public string MeasurementResultsT1 { get; set; }

        public string MeasurementResultsT2 { get; set; }

        public string DilutionCoefficientL1 { get; set; }

        public string DilutionCoefficientL2 { get; set; }

        public string DilutionCoefficientT1 { get; set; }

        public string DilutionCoefficientT2 { get; set; }

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

        public IDTRTestPropertyModel IDTRTestPropertyEntity { get; set; }
    }
}