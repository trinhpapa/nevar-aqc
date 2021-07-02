using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.TestDepartment;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NEVAR_AQC.Core.Models.Managements
{
    public class CTGTestMethodModel : ExtensionEntity<long>
    {
        [StringLength(200)]
        public string Name { get; set; }

        public long TestPropertyId { get; set; }

        [StringLength(10)]
        public string SymbolAttached { get; set; }

        public bool Status { get; set; } = true;

        public CTGTestPropertyModel CTGTestPropertyEntity { get; set; }

        public List<IDTRTestPropertyModel> IDTRTestPropertyEntities { get; set; }
    }
}