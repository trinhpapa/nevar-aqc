using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEVAR_AQC.Core.Entities
{
    public partial class CTGRequirementStatusEntity : ExtensionEntity<int>
    {
        [StringLength(100)]
        public string ProcessStatus { get; set; }

        [StringLength(6)]
        public string HtmlColour { get; set; }

        public bool? Status { get; set; } = true;

        public virtual SYSUserEntity SYSUserEntity { get; set; }

        [ForeignKey("ProcessStatusId")]
        public virtual ICollection<SYSRequirementInvoiceEntity> SYSRequirementInvoiceEntities { get; set; }
    }
}