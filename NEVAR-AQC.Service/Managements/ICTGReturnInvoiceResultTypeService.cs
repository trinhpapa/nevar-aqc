using NEVAR_AQC.Core.Models.Managements;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NEVAR_AQC.Service.Managements
{
    public interface ICTGReturnInvoiceResultTypeService
    {
        Task<IEnumerable<CTGReturnInvoiceResultTypeModel>> GetStatusOnAsync();
    }
}