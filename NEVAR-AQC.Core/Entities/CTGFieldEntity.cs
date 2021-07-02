using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEVAR_AQC.Core.Entities
{
    public class CTGFieldEntity : ExtensionEntity<long>
    {
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(10)]
        public string Symbol { get; set; }

        [ForeignKey("FieldId")]
        public virtual ICollection<SYSRequirementInvoiceEntity> RequirementInvoiceEntities { get; set; }

        [ForeignKey("FieldId")]
        public virtual ICollection<CTGTestObjectEntity> CTGTestObjectEntities { get; set; }
    }
}