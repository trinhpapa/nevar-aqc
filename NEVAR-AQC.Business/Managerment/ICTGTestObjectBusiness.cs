using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Core.PagingHelper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.Managerment
{
    public interface ICTGTestObjectBusiness
    {
        Task<bool> CanBeDeleteAsync(long id);

        Task<IEnumerable<CTGTestObjectModel>> GetAllAsync();

        Task<PagedResult<CTGTestObjectModel>> GetPagedAsync(int pageIndex, int pageSize, string searchString = null);

        Task<CTGTestObjectModel> GetByIdAsync(long id);

        Task<CTGTestObjectModel> GetByNameAsync(string name);

        Task<CTGTestObjectModel> CreateAsync(CTGTestObjectModel model, CancellationToken cancellationToken = default);

        Task UpdateAsync(CTGTestObjectModel model, CancellationToken cancellationToken = default);

        Task DeleteAsync(CTGTestObjectModel model, CancellationToken cancellationToken = default);
    }
}
