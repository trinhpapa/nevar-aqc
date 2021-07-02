using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.TestDepartment;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NEVAR_AQC.Core.Models.Managements
{
    public class CTGTestPropertyModel : ExtensionEntity<long>
    {
        [StringLength(200)]
        public string Name { get; set; }

        public long ObjectId { get; set; }

        [StringLength(100)]
        public string Unit { get; set; }

        public bool Status { get; set; } = true;

        public CTGTestObjectModel CTGTestObjectEntity { get; set; }

        public List<CTGTestMethodModel> CTGTestMethodEntities { get; set; }

        public List<IDTRTestPropertyModel> IDTRTestPropertyEntities { get; set; }

    }
}