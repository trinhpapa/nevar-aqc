// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions </Copyright>
//     <Url> http://lehoangtrinh.com </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Core </Project>
//     <File>
//         <Name> IDTRTestProcessOtherMethodEntity.cs </Name>
//         <Created> 19/6/2019 - 14:52 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//          Bảng Báo cáo tiến trình - Phương pháp khác
//     </Summary>
// <License>

using System;
using System.ComponentModel;

namespace NEVAR_AQC.Core.Entities
{
    public class IDTRTestProcessOtherMethodEntity : ExtensionEntity<long>
    {
        [DefaultValue(false)]
        public bool IsSubmitReport { get; set; } = false;

        public long SpecimenPropertyId { get; set; }

        public string MonitoringData { get; set; }

        public string ReportResults { get; set; }

        public DateTime TimeReportResults { get; set; }

        public virtual IDTRTestPropertyEntity IDTRTestPropertyEntity { get; set; }
    }
}