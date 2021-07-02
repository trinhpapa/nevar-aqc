using NEVAR_AQC.Core.Entities;

namespace NEVAR_AQC.Core.Models.Managements
{
    public class SYSRoleFunctionCreateModel : LogEntity<long>
    {
        public int RoleId { get; set; }

        public long FunctionId { get; set; }
    }

    public class SYSRoleFunctionUpdateModel : SYSRoleFunctionCreateModel
    {
        public long Id { get; set; }
    }

    public class SYSRoleFunctionViewModel : SYSRoleFunctionUpdateModel
    {
    }

    public class SYSRoleFunctionModel : ExtensionEntity<long>
    {
        public int RoleId { get; set; }

        public long FunctionId { get; set; }

        public CTGRoleModel CTGRoleEntity { get; set; }

        public CTGSystemFunctionModel CTGSystemFunctionEntity { get; set; }
    }
}