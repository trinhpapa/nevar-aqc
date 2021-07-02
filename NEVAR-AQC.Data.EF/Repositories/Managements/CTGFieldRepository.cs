using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Data.Managements;

namespace NEVAR_AQC.Data.EF.Repositories.Managements
{
    public class CTGFieldRepository : RepositoryBase<CTGFieldEntity, NEVARDbContext>, ICTGFieldRepository
    {
        public CTGFieldRepository(NEVARDbContext context) : base(context)
        {
        }
    }
}