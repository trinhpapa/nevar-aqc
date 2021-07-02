using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEVAR_AQC.Core.Entities
{
    public partial class CTGTestPropertyEntity : ExtensionEntity<long>
    {
        [StringLength(200)]
        public string Name { get; set; }

        public long ObjectId { get; set; }

        [StringLength(100)]
        public string Unit { get; set; }

        public bool Status { get; set; } = true;

        public virtual CTGTestObjectEntity CTGTestObjectEntity { get; set; }

        [ForeignKey("TestPropertyId")]
        public virtual ICollection<CTGTestMethodEntity> CTGTestMethodEntities { get; set; }

        [ForeignKey("TestPropertyId")]
        public virtual ICollection<IDTRTestPropertyEntity> IDTRTestPropertyEntities { get; set; }
    }
}