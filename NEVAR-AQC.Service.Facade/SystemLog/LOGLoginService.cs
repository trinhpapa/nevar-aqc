using NEVAR_AQC.Business.SystemLog;
using NEVAR_AQC.Core.Models.System;
using NEVAR_AQC.Core.PagingHelper;
using NEVAR_AQC.Service.SystemLog;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Service.Facade.SystemLog
{
    public class LOGLoginService : ILOGLoginService
    {
        private ILOGLoginBusiness _lOGLoginBusiness;

        public LOGLoginService(ILOGLoginBusiness lOGLoginBusiness)
        {
            _lOGLoginBusiness = lOGLoginBusiness;
        }

        public async Task CreateAsync(LOGLoginModel model, CancellationToken cancellationToken = default)
        {
            await _lOGLoginBusiness.CreateAsync(model, cancellationToken);
        }

        public async Task<PagedResult<LOGLoginModel>> GetPagedAsync(int pageIndex, int pageSize, string searchString)
        {
            return await _lOGLoginBusiness.GetPagedAsync(pageIndex, pageSize, searchString);
        }

        public async Task<IEnumerable<LOGLoginModel>> GetTopAsync(int record = 10)
        {
            return await _lOGLoginBusiness.GetTopAsync(record);
        }
    }
}