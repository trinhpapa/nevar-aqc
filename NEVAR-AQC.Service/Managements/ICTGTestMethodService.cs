using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Core.PagingHelper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Service.Managements
{
    public interface ICTGTestMethodService
    {
        Task<IEnumerable<CTGTestMethodModel>> GetAllAsync();

        Task<PagedResult<CTGTestMethodModel>> GetPagedAsync(int pageIndex, int pageSize, string searchString = null);

        Task<CTGTestMethodModel> GetByIdAsync(long id);

        Task<CTGTestMethodModel> CreateAsync(CTGTestMethodModel model, CancellationToken cancellationToken = default);

        Task UpdateAsync(CTGTestMethodModel model, CancellationToken cancellationToken = default);

        Task DeleteAsync(CTGTestMethodModel model, CancellationToken cancellationToken = default);
    }
}
