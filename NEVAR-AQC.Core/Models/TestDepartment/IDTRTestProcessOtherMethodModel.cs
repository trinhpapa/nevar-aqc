using NEVAR_AQC.Core.Entities;
using System;
using System.ComponentModel;

namespace NEVAR_AQC.Core.Models.TestDepartment
{
    public class IDTRTestProcessOtherMethodModel : ExtensionEntity<long>
    {
        [DefaultValue(false)]
        public bool IsSubmitReport { get; set; } = false;

        public long SpecimenPropertyId { get; set; }

        public string MonitoringData { get; set; }

        public string ReportResults { get; set; }

        public DateTime TimeReportResults { get; set; }

        public IDTRTestPropertyModel IDTRTestPropertyEntity { get; set; }
    }
}