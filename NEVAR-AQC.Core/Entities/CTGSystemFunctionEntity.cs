using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEVAR_AQC.Core.Entities
{
    public class CTGSystemFunctionEntity : ExtensionEntity<long>
    {
        public int Key { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public int? Parent { get; set; }

        public bool Status { get; set; }

        [ForeignKey("FunctionId")]
        public virtual ICollection<SYSRoleFunctionEntity> SYSRoleFunctionEntities { get; set; }
    }
}