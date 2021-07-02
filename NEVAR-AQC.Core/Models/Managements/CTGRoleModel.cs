using NEVAR_AQC.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NEVAR_AQC.Core.Models.Managements
{
    public class CTGRoleModel : ExtensionEntity<int>
    {
        [StringLength(100)]
        public string Name { get; set; }

        public bool Status { get; set; } = true;

        public List<SYSRoleFunctionModel> SYSRoleFunctionEntities { get; set; }
    }
}