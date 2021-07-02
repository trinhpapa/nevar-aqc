using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Data.Managements;

namespace NEVAR_AQC.Data.EF.Repositories.Managements
{
    public class CTGSystemFunctionRepository : RepositoryBase<CTGSystemFunctionEntity, NEVARDbContext>, ICTGSystemFunctionRepository
    {
        public CTGSystemFunctionRepository(NEVARDbContext context) : base(context)
        {
        }
    }
}