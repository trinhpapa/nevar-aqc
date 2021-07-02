using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEVAR_AQC.Core.Entities
{
    public partial class CTGRoleEntity : ExtensionEntity<int>
    {
        [StringLength(100)]
        public string Name { get; set; }

        public bool? Status { get; set; }

        [ForeignKey("RoleId")]
        public virtual ICollection<SYSUserEntity> SYSUserEntities { get; set; }

        [ForeignKey("RoleId")]
        public virtual ICollection<SYSRoleFunctionEntity> SYSRoleFunctionEntities { get; set; }
    }
}