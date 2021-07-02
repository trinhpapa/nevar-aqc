using NEVAR_AQC.Core.Models.User;
using NEVAR_AQC.Core.PagingHelper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.User
{
    public interface ISYSUserBusiness
    {
        Task<SYSUserModel> GetByUsernameAsync(string username);

        Task<SYSUserViewModel> GetByIdAsync(long id);

        Task<IEnumerable<SYSUserModel>> GetAllAsync(string username, string searchName);

        Task<PagedResult<SYSUserModel>> GetPagingAsync(int pageIndex, int pageSize, string searchString);

        Task<SYSUserViewModel> CreateAsync(SYSUserCreateModel model, CancellationToken cancellationToken = default);

        Task UpdateAsync(SYSUserUpdateModel model, CancellationToken cancellationToken = default);

        Task UpdatePasswordAsync(SYSUserUpdateModel model, CancellationToken cancellationToken = default);
    }
}