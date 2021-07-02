using NEVAR_AQC.Core.Entities;
using System;
using System.ComponentModel;

namespace NEVAR_AQC.Core.Models.TestDepartment
{
    public class IDTRTestProcessWeightMethodModel : ExtensionEntity<long>
    {
        [DefaultValue(false)]
        public bool IsSubmitReport { get; set; } = false;

        public long SpecimenPropertyId { get; set; }

        public string QuantumL1 { get; set; }

        public string QuantumL2 { get; set; }

        public string WeightOfScaleSymbolL1 { get; set; }

        public string WeightOfCupL1 { get; set; }

        public string WeightOfCupAndSpecimenL1 { get; set; }

        public string WeightOfScaleSymbolL2 { get; set; }

        public string WeightOfCupL2 { get; set; }

        public string WeightOfCupAndSpecimenL2 { get; set; }

        public string SymbolT1 { get; set; }

        public string WeightOfCupT1 { get; set; }

        public string WeightOfCupAndSpecimenT1 { get; set; }

        public string SymbolT2 { get; set; }

        public string WeightOfCupT2 { get; set; }

        public string WeightOfCupAndSpecimenT2 { get; set; }

        public string DilutionCoefficientL1 { get; set; }

        public string DilutionCoefficientSymbolL1 { get; set; }

        public string DilutionCoefficientL2 { get; set; }

        public string DilutionCoefficientSymbolL2 { get; set; }

        public string DilutionCoefficientT1 { get; set; }

        public string DilutionCoefficientSymbolT1 { get; set; }

        public string DilutionCoefficientT2 { get; set; }

        public string DilutionCoefficientSymbolT2 { get; set; }

        public string CalculationFormula { get; set; }

        public string ResultSymbolL1 { get; set; }

        public string ResultL1 { get; set; }

        public string ResultSymbolL2 { get; set; }

        public string ResultL2 { get; set; }

        public string ResultSymbolT1 { get; set; }

        public string ResultT1 { get; set; }

        public string ResultSymbolT2 { get; set; }

        public string ResultT2 { get; set; }

        public string AverageResultsL { get; set; }

        public string AverageResultsT { get; set; }

        public string PercentOfRevoke { get; set; }

        public string ReportResults { get; set; }

        public DateTime TimeReportResults { get; set; }

        public IDTRTestPropertyModel IDTRTestPropertyEntity { get; set; }
    }
}