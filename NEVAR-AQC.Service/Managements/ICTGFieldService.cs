using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Core.PagingHelper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Service.Managements
{
    public interface ICTGFieldService
    {
        Task<IEnumerable<CTGFieldModel>> GetAllAsync();

        Task<PagedResult<CTGFieldModel>> GetPagedAsync(int pageIndex, int pageSize, string searchString = null);

        Task<CTGFieldModel> GetByIdAsync(long id);

        Task<CTGFieldModel> CreateAsync(CTGFieldModel model, CancellationToken cancellationToken = default);

        Task UpdateAsync(CTGFieldModel model, CancellationToken cancellationToken = default);

        Task DeleteAsync(CTGFieldModel model, CancellationToken cancellationToken = default);
    }
}