using NEVAR_AQC.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NEVAR_AQC.Core.Models.Managements
{
    public class CTGSystemFunctionModel : ExtensionEntity<long>
    {
        public int Key { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public int? Parent { get; set; }

        public bool Status { get; set; }

        public List<SYSRoleFunctionModel> SYSRoleFunctionEntities { get; set; }
    }
}