using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Data.Managements;

namespace NEVAR_AQC.Data.EF.Repositories.Managements
{
    public class CTGTestPropertyRepository : RepositoryBase<CTGTestPropertyEntity, NEVARDbContext>, ICTGTestPropertyRepository
    {
        public CTGTestPropertyRepository(NEVARDbContext context) : base(context)
        {
        }
    }
}