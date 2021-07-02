using NEVAR_AQC.Business.Managerment;
using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Service.Managements;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NEVAR_AQC.Service.Facade.Managements
{
    public class CTGSystemFunctionService : ICTGSystemFunctionService
    {
        private ICTGSystemFunctionBusiness _cTGSystemFunctionBusiness;

        public CTGSystemFunctionService(ICTGSystemFunctionBusiness cTGSystemFunctionBusiness)
        {
            _cTGSystemFunctionBusiness = cTGSystemFunctionBusiness;
        }

        public async Task<IEnumerable<CTGSystemFunctionModel>> GetAllAsync()
        {
            return await _cTGSystemFunctionBusiness.GetAllAsync();
        }
    }
}
