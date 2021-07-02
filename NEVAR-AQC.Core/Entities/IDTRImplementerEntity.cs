using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEVAR_AQC.Core.Entities
{
   public class IDTRImplementerEntity : ExtensionEntity<long>
   {
      public long SpecimenPropertyId { get; set; }

      public long? UserId { get; set; }

      public bool IsAccept { get; set; }

      public DateTime? TimeToStart { get; set; }

      public DateTime? TimeToReport { get; set; }

      [ForeignKey("UserId")]
      public virtual SYSUserEntity SYSUserEntity { get; set; }

      [ForeignKey("CreatedBy")]
      public virtual SYSUserEntity CRESYSUserEntity { get; set; }

      [ForeignKey("ModifiedBy")]
      public virtual SYSUserEntity MODSYSUserEntity { get; set; }

      [ForeignKey("DeletedBy")]
      public virtual SYSUserEntity DELSYSUserEntity { get; set; }

      public virtual IDTRTestPropertyEntity IDTRTestPropertyEntity { get; set; }
   }
}