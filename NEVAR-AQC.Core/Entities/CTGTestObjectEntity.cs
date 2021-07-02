using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEVAR_AQC.Core.Entities
{
    public class CTGTestObjectEntity : ExtensionEntity<long>
    {
        [StringLength(200)]
        public string Name { get; set; }

        public long FieldId { get; set; }

        public bool Status { get; set; } = true;

        public virtual CTGFieldEntity CTGFieldEntity { get; set; }

        [ForeignKey("ObjectId")]
        public virtual ICollection<IDTestRequirementEntity> IDTestRequirementEntities { get; set; }

        [ForeignKey("ObjectId")]
        public virtual ICollection<CTGTestPropertyEntity> CTGTestPropertyEntities { get; set; }
    }
}