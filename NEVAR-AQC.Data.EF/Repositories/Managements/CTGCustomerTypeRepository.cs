using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Data.Managements;

namespace NEVAR_AQC.Data.EF.Repositories.Managements
{
    public class CTGCustomerTypeRepository : RepositoryBase<CTGCustomerTypeEntity, NEVARDbContext>, ICTGCustomerTypeRepository
    {
        public CTGCustomerTypeRepository(NEVARDbContext context) : base(context)
        {
        }
    }
}