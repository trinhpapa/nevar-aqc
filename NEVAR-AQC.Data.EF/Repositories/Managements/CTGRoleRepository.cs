using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Data.Managements;

namespace NEVAR_AQC.Data.EF.Repositories.Managements
{
    public class CTGRoleRepository : RepositoryBase<CTGRoleEntity, NEVARDbContext>, ICTGRoleRepository
    {
        public CTGRoleRepository(NEVARDbContext context) : base(context)
        {
        }
    }
}