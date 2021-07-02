using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEVAR_AQC.Core.Entities
{
    public class CTGReturnInvoiceResultTypeEntity : ExtensionEntity<int>
    {
        [StringLength(50)]
        public string Name { get; set; }

        public bool? Status { get; set; } = true;

        [ForeignKey("ReturnInvoiceResultTypeId")]
        public virtual ICollection<SYSRequirementInvoiceEntity> SYSRequirementInvoiceEntities { get; set; }
    }
}