using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Data.Managements;

namespace NEVAR_AQC.Data.EF.Repositories.Managements
{
    public class CTGRequirementProcessStatusRepository : RepositoryBase<CTGRequirementStatusEntity, NEVARDbContext>, ICTGRequirementProcessStatusRepository
    {
        public CTGRequirementProcessStatusRepository(NEVARDbContext context) : base(context)
        {
        }
    }
}