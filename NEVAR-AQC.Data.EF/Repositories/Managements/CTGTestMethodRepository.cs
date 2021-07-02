using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Data.Managements;

namespace NEVAR_AQC.Data.EF.Repositories.Managements
{
    public class CTGTestMethodRepository : RepositoryBase<CTGTestMethodEntity, NEVARDbContext>, ICTGTestMethodRepository
    {
        public CTGTestMethodRepository(NEVARDbContext context) : base(context)
        {
        }
    }
}