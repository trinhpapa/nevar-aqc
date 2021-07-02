using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEVAR_AQC.Core.Entities
{
    public partial class CTGRequirementTypeEntity : ExtensionEntity<int>
    {
        [StringLength(200)]
        public string Vietnamese { get; set; }

        [StringLength(200)]
        public string English { get; set; }

        [StringLength(10)]
        public string Alias { get; set; }

        [StringLength(10)]
        public string Symbol { get; set; }

        public int DepartmentId { get; set; }

        public virtual CTGDepartmentEntity CTGDepartmentEntity { get; set; }

        [ForeignKey("RequirementTypeId")]
        public virtual ICollection<SYSRequirementInvoiceEntity> SYSRequirementInvoiceEntities { get; set; }
    }
}