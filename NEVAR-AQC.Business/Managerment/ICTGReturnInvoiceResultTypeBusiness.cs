using NEVAR_AQC.Core.Models.Managements;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.Managerment
{
    public interface ICTGReturnInvoiceResultTypeBusiness
    {
        Task<IEnumerable<CTGReturnInvoiceResultTypeModel>> GetStatusOnAsync();
    }
}