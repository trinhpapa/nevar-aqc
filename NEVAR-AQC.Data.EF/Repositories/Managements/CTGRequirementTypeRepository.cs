using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Data.Managements;

namespace NEVAR_AQC.Data.EF.Repositories.Managements
{
    public class CTGRequirementTypeRepository : RepositoryBase<CTGRequirementTypeEntity, NEVARDbContext>, ICTGRequirementTypeRepository
    {
        public CTGRequirementTypeRepository(NEVARDbContext context) : base(context)
        {
        }
    }
}