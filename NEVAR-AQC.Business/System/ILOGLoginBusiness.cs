using NEVAR_AQC.Core.Models.System;
using NEVAR_AQC.Core.PagingHelper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.SystemLog
{
    public interface ILOGLoginBusiness
    {
        Task CreateAsync(LOGLoginModel model, CancellationToken cancellationToken = default);

        Task<PagedResult<LOGLoginModel>> GetPagedAsync(int pageIndex, int pageSize, string searchString);

        Task<IEnumerable<LOGLoginModel>> GetTopAsync(int record = 10);
    }
}