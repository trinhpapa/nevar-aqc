using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEVAR_AQC.Core.Entities
{
   public partial class IDCalibrationRequirementEntity : ExtensionEntity<long>
   {
      public long RequirementInvoiceId { get; set; }

      [StringLength(200)]
      public string NameOfMeasuringDevice { get; set; }

      [StringLength(20)]
      public string SerialNumber { get; set; }

      [StringLength(200)]
      public string TechnicalCharacteristics { get; set; }

      public int? Amount { get; set; }

      [StringLength(50)]
      public string AmountUnit { get; set; }

      public bool Status { get; set; }

      [ForeignKey("CreatedBy")]
      public virtual SYSUserEntity CRESYSUserEntity { get; set; }

      [ForeignKey("ModifiedBy")]
      public virtual SYSUserEntity MODSYSUserEntity { get; set; }

      [ForeignKey("DeletedBy")]
      public virtual SYSUserEntity DELSYSUserEntity { get; set; }

      public virtual SYSRequirementInvoiceEntity SYSRequirementInvoiceEntity { get; set; }
   }
}