using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Data.Managements;

namespace NEVAR_AQC.Data.EF.Repositories.Managements
{
    public class SYSRoleFunctionRepository : RepositoryBase<SYSRoleFunctionEntity, NEVARDbContext>, ISYSRoleFunctionrepository
    {
        public SYSRoleFunctionRepository(NEVARDbContext context) : base(context)
        {
        }
    }
}