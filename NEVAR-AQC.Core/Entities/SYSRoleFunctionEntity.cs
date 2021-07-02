using System.ComponentModel.DataAnnotations.Schema;

namespace NEVAR_AQC.Core.Entities
{
   public class SYSRoleFunctionEntity : ExtensionEntity<long>
   {
      public int RoleId { get; set; }

      public long FunctionId { get; set; }

      [ForeignKey("CreatedBy")]
      public virtual SYSUserEntity CRESYSUserEntity { get; set; }

      [ForeignKey("ModifiedBy")]
      public virtual SYSUserEntity MODSYSUserEntity { get; set; }

      [ForeignKey("DeletedBy")]
      public virtual SYSUserEntity DELSYSUserEntity { get; set; }

      public virtual CTGRoleEntity CTGRoleEntity { get; set; }
      public virtual CTGSystemFunctionEntity CTGSystemFunctionEntity { get; set; }
   }
}