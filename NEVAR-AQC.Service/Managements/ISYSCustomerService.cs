#region	License
// <License>
//     <Copyright> 2019 © Le Hoang Trinh </Copyright>
//     <Url> https://github.com/trinhpapa </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Service </Project>
//     <File>
//         <Name> ICustomerService.cs </Name>
//         <Created> 14/2/2019 - 22:42:02 </Created>
//     </File>
//     <Summary>
//
//     </Summary>
// </License>
#endregion License

using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Core.PagingHelper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Service.Managements
{
    public interface ISYSCustomerService
    {
        Task<IEnumerable<SYSCustomerViewModel>> GetAllAsync();

        Task<SYSCustomerCreateResultModel> CreateAsync(SYSCustomerCreateModel model, CancellationToken cancellationToken = default);

        Task<SYSCustomerViewModel> GetByIdAsync(long id);

        Task<PagedResult<SYSCustomerViewModel>> GetPagedAsync(int pageIndex, int pageSize, string searchString = null);

        Task UpdateAsync(SYSCustomerUpdateModel model, CancellationToken cancellationToken = default);

        Task<IEnumerable<CTGCustomerTypeModel>> GetCustomerType();
    }
}