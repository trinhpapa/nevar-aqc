using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Core.PagingHelper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.Managerment
{
    public interface ISYSCustomerBusiness
    {
        Task<IEnumerable<SYSCustomerViewModel>> GetAllAsync();

        Task<PagedResult<SYSCustomerViewModel>> GetPagedAsync(int pageIndex, int pageSize, string searchString = null);

        Task<SYSCustomerViewModel> GetByNameAsync(string customerName);

        Task<SYSCustomerViewModel> GetByIdAsync(long customerId);

        Task<SYSCustomerCreateResultModel> CreateAsync(SYSCustomerCreateModel model, CancellationToken cancellationToken = default);

        Task UpdateAsync(SYSCustomerUpdateModel model, CancellationToken cancellationToken = default);

        bool CheckIsExistByName(string name);

        Task<IEnumerable<CTGCustomerTypeModel>> GetCustomerType();
    }
}