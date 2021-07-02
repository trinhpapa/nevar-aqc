using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Core.PagingHelper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.Managerment
{
    public interface ICTGTestMethodBusiness
    {
        Task<bool> CanBeDeleteAsync(long id);

        Task<IEnumerable<CTGTestMethodModel>> GetAllAsync();

        Task<PagedResult<CTGTestMethodModel>> GetPagedAsync(int pageIndex, int pageSize, string searchString = null);

        Task<CTGTestMethodModel> GetByIdAsync(long id);

        Task<CTGTestMethodModel> GetByNameAsync(string name, long propertyId);

        Task<CTGTestMethodModel> CreateAsync(CTGTestMethodModel model, CancellationToken cancellationToken = default);

        Task UpdateAsync(CTGTestMethodModel model, CancellationToken cancellationToken = default);

        Task DeleteAsync(CTGTestMethodModel model, CancellationToken cancellationToken = default);
    }
}
