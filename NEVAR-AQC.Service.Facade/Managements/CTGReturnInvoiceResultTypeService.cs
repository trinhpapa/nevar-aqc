using NEVAR_AQC.Business.Managerment;
using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Service.Managements;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NEVAR_AQC.Service.Facade.Managements
{
    public class CTGReturnInvoiceResultTypeService : ICTGReturnInvoiceResultTypeService
    {
        private readonly ICTGReturnInvoiceResultTypeBusiness _cTGReturnInvoiceResultTypeBusiness;

        public CTGReturnInvoiceResultTypeService(ICTGReturnInvoiceResultTypeBusiness cTGReturnInvoiceResultTypeBusiness)
        {
            _cTGReturnInvoiceResultTypeBusiness = cTGReturnInvoiceResultTypeBusiness;
        }

        public async Task<IEnumerable<CTGReturnInvoiceResultTypeModel>> GetStatusOnAsync()
        {
            return await _cTGReturnInvoiceResultTypeBusiness.GetStatusOnAsync();
        }
    }
}