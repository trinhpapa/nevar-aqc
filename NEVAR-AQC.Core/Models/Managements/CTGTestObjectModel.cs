using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.ReceptionDepartment;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NEVAR_AQC.Core.Models.Managements
{
    public class CTGTestObjectModel : ExtensionEntity<long>
    {
        [StringLength(200)]
        public string Name { get; set; }

        public long FieldId { get; set; }

        public bool Status { get; set; } = true;

        public CTGFieldModel CTGFieldEntity { get; set; }
        public List<IDTestRequirementModel> IDTestRequirementEntities { get; set; }
        public List<CTGTestPropertyModel> CTGTestPropertyEntities { get; set; }
    }
}