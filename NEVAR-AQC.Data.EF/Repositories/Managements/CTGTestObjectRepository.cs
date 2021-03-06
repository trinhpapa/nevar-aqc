using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Data.Managements;

namespace NEVAR_AQC.Data.EF.Repositories.Managements
{
    public class CTGTestObjectRepository : RepositoryBase<CTGTestObjectEntity, NEVARDbContext>, ICTGTestObjectRepository
    {
        public CTGTestObjectRepository(NEVARDbContext context) : base(context)
        {
        }
    }
}