using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Core.PagingHelper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Service.Managements
{
    public interface ICTGTestPropertyService
    {
        Task<IEnumerable<CTGTestPropertyModel>> GetAllAsync();

        Task<PagedResult<CTGTestPropertyModel>> GetPagedAsync(int pageIndex, int pageSize, string searchString = null);

        Task<CTGTestPropertyModel> GetByIdAsync(long id);

        Task<CTGTestPropertyModel> CreateAsync(CTGTestPropertyModel model, CancellationToken cancellationToken = default);

        Task UpdateAsync(CTGTestPropertyModel model, CancellationToken cancellationToken = default);

        Task DeleteAsync(CTGTestPropertyModel model, CancellationToken cancellationToken = default);

        Task<IEnumerable<CTGTestPropertyModel>> GetByObjectIdAsync(long objectId);
    }
}
