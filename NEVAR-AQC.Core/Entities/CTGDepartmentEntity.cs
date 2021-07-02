using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEVAR_AQC.Core.Entities
{
    public partial class CTGDepartmentEntity : ExtensionEntity<int>
    {
        [StringLength(200)]
        public string Name { get; set; }

        public bool Status { get; set; } = true;

        [ForeignKey("DepartmentId")]
        public virtual ICollection<CTGRequirementTypeEntity> CTGRequirementTypeEntities { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual ICollection<SYSUserEntity> SYSUserEntities { get; set; }
    }
}