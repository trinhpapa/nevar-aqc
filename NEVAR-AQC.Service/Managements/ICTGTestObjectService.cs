using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Core.PagingHelper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Service.Managements
{
    public interface ICTGTestObjectService
    {
        Task<IEnumerable<CTGTestObjectModel>> GetAllAsync();

        Task<PagedResult<CTGTestObjectModel>> GetPagedAsync(int pageIndex, int pageSize, string searchString = null);

        Task<CTGTestObjectModel> GetByIdAsync(long id);

        Task<CTGTestObjectModel> CreateAsync(CTGTestObjectModel model, CancellationToken cancellationToken = default);

        Task UpdateAsync(CTGTestObjectModel model, CancellationToken cancellationToken = default);

        Task DeleteAsync(CTGTestObjectModel model, CancellationToken cancellationToken = default);
    }
}