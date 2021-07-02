using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Core.PagingHelper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.Managerment
{
    public interface ICTGRoleBusiness
    {
        Task<IEnumerable<CTGRoleModel>> GetAllAsync();

        Task<CTGRoleModel> CreateAsync(CTGRoleModel model, CancellationToken cancellationToken = default);

        Task<bool> CanBeDeleteAsync(int id);

        Task DeleteAsync(CTGRoleModel model, CancellationToken cancellationToken = default);

        Task<CTGRoleModel> GetByIdAsync(int id);

        Task<PagedResult<CTGRoleModel>> GetPagedAsync(int pageIndex, int pageSize, string searchString = null);

        Task UpdateAsync(CTGRoleModel model, CancellationToken cancellationToken = default);
    }
}