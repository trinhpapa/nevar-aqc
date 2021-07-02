using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Core.PagingHelper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.Managerment
{
    public interface ICTGTestPropertyBusiness
    {
        Task<bool> CanBeDeleteAsync(long id);

        Task<IEnumerable<CTGTestPropertyModel>> GetAllAsync();

        Task<PagedResult<CTGTestPropertyModel>> GetPagedAsync(int pageIndex, int pageSize, string searchString = null);

        Task<CTGTestPropertyModel> GetByIdAsync(long id);

        Task<CTGTestPropertyModel> GetByNameAsync(string name, long objectId);

        Task<CTGTestPropertyModel> CreateAsync(CTGTestPropertyModel model, CancellationToken cancellationToken = default);

        Task UpdateAsync(CTGTestPropertyModel model, CancellationToken cancellationToken = default);

        Task DeleteAsync(CTGTestPropertyModel model, CancellationToken cancellationToken = default);

        Task<IEnumerable<CTGTestPropertyModel>> GetByObjectIdAsync(long objectId);
    }
}
