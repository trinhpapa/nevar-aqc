using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEVAR_AQC.Core.Entities
{
   public partial class SYSCustomerEntity : ExtensionEntity<long>
   {
      [StringLength(500)]
      public string Name { get; set; }

      public int? CustomerTypeId { get; set; }

      public int? ProvinceId { get; set; }

      public int? DistrictId { get; set; }

      public int? WardsId { get; set; }

      [StringLength(1000)]
      public string Address { get; set; }

      [StringLength(150)]
      public string Email { get; set; }

      [StringLength(20)]
      public string PhoneNumber { get; set; }

      [StringLength(50)]
      public string Fax { get; set; }

      [ForeignKey("CreatedBy")]
      public virtual SYSUserEntity CRESYSUserEntity { get; set; }

      [ForeignKey("ModifiedBy")]
      public virtual SYSUserEntity MODSYSUserEntity { get; set; }

      [ForeignKey("DeletedBy")]
      public virtual SYSUserEntity DELSYSUserEntity { get; set; }

      public virtual CTGCustomerTypeEntity CTGCustomerTypeEntity { get; set; }

      [ForeignKey("CustomerId")]
      public virtual ICollection<SYSRequirementInvoiceEntity> SYSRequirementInvoiceEntities { get; set; }
   }
}