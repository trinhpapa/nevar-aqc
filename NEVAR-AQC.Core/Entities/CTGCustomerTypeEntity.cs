using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEVAR_AQC.Core.Entities
{
    public partial class CTGCustomerTypeEntity : ExtensionEntity<int>
    {
        [StringLength(200)]
        public string Name { get; set; }

        public bool? Status { get; set; }

        [ForeignKey("CustomerTypeId")]
        public virtual ICollection<SYSCustomerEntity> SYSCustomerEntities { get; set; }
    }
}