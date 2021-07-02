using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEVAR_AQC.Core.Entities
{
    public partial class CTGTestMethodEntity : ExtensionEntity<long>
    {
        [StringLength(200)]
        public string Name { get; set; }

        public long TestPropertyId { get; set; }

        [StringLength(10)]
        public string SymbolAttached { get; set; }

        public bool Status { get; set; } = true;

        public virtual CTGTestPropertyEntity CTGTestPropertyEntity { get; set; }

        [ForeignKey("TestMethodId")]
        public virtual ICollection<IDTRTestPropertyEntity> IDTRTestPropertyEntities { get; set; }
    }
}