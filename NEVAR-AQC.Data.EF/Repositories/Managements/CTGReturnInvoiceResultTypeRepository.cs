using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Data.Managements;

namespace NEVAR_AQC.Data.EF.Repositories.Managements
{
    public class CTGReturnInvoiceResultTypeRepository : RepositoryBase<CTGReturnInvoiceResultTypeEntity, NEVARDbContext>, ICTGReturnInvoiceResultTypeRepository
    {
        public CTGReturnInvoiceResultTypeRepository(NEVARDbContext context) : base(context)
        {
        }
    }
}