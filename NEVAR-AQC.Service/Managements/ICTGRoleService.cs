using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Core.PagingHelper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Service.Managements
{
    public interface ICTGRoleService
    {
        Task<IEnumerable<CTGRoleModel>> GetAllAsync();

        Task<PagedResult<CTGRoleModel>> GetPagedAsync(int pageIndex, int pageSize, string searchString = null);

        Task<CTGRoleModel> GetByIdAsync(int id);

        Task<CTGRoleModel> CreateAsync(CTGRoleModel model, CancellationToken cancellationToken = default);

        Task UpdateAsync(CTGRoleModel model, CancellationToken cancellationToken = default);

        Task DeleteAsync(CTGRoleModel model, CancellationToken cancellationToken = default);
    }
}