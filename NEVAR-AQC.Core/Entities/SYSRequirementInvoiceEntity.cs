using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEVAR_AQC.Core.Entities
{
   public partial class SYSRequirementInvoiceEntity : ExtensionEntity<long>
   {
      public int RequirementTypeId { get; set; }

      public int? Serial { get; set; }

      public int? SerialYear { get; set; }

      [StringLength(50)]
      public string InvoiceNo { get; set; }

      public int Edition { get; set; }

      public long? FieldId { get; set; }

      public long CustomerId { get; set; }

      [StringLength(100)]
      public string Representative { get; set; }

      [StringLength(20)]
      public string RepresentativePhone { get; set; }

      [StringLength(200)]
      public string OtherInformation { get; set; }

      [StringLength(200)]
      public string OtherRequirement { get; set; }

      public bool IsSaveSpecimen { get; set; }

      public string SaveSpecimenTime { get; set; }

      public bool IsUseSubcontractors { get; set; }

      public DateTime? ResultDay { get; set; }

      public int ReturnInvoiceResultTypeId { get; set; }

      public int ResultInvoiceAmount { get; set; }

      public int ProcessStatusId { get; set; }

      public virtual CTGReturnInvoiceResultTypeEntity CTGReturnInvoiceResultTypeEntity { get; set; }
      public virtual CTGFieldEntity CTGFieldEntity { get; set; }
      public virtual CTGRequirementTypeEntity CTGRequirementTypeEntity { get; set; }
      public virtual SYSCustomerEntity SYSCustomerEntity { get; set; }
      public virtual CTGRequirementStatusEntity CTGRequirementStatusEntity { get; set; }

      [ForeignKey("CreatedBy")]
      public virtual SYSUserEntity CRESYSUserEntity { get; set; }

      [ForeignKey("ModifiedBy")]
      public virtual SYSUserEntity MODSYSUserEntity { get; set; }

      [ForeignKey("DeletedBy")]
      public virtual SYSUserEntity DELSYSUserEntity { get; set; }

      [ForeignKey("RequirementInvoiceId")]
      public virtual ICollection<IDCalibrationRequirementEntity> IDCalibrationRequirementEntities { get; set; }

      [ForeignKey("RequirementInvoiceId")]
      public virtual ICollection<IDTestRequirementEntity> IDTestRequirementEntities { get; set; }
   }
}