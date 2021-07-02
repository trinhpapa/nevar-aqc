// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions </Copyright>
//     <Url> http://lehoangtrinh.com </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Service.Facade </Project>
//     <File>
//         <Name> SYSCustomerService.cs </Name>
//         <Created> 27/2/2019 - 22:28 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//
//     </Summary>
// <License>

using NEVAR_AQC.Business.Managerment;
using NEVAR_AQC.Business.Notification;
using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Core.PagingHelper;
using NEVAR_AQC.Service.Managements;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Service.Facade.Managements
{
    public class SYSCustomerService : ISYSCustomerService
    {
        private ISYSCustomerBusiness _customerBusiness;

        private ICustomerNotification _customerNotification;

        public SYSCustomerService(ISYSCustomerBusiness customerBusiness,
            ICustomerNotification customerNotification)
        {
            _customerBusiness = customerBusiness;
            _customerNotification = customerNotification;
        }

        public async Task<IEnumerable<SYSCustomerViewModel>> GetAllAsync()
        {
            return await _customerBusiness.GetAllAsync();
        }

        public async Task<SYSCustomerCreateResultModel> CreateAsync(SYSCustomerCreateModel model, CancellationToken cancellationToken = default)
        {
            var checkExistSymbol = _customerBusiness.CheckIsExistByName(model.Name);
            if (checkExistSymbol)
            {
                throw new System.Exception("Khách hàng đã tồn tại");
            }
            var createdResult = await _customerBusiness.CreateAsync(model, cancellationToken);

            _customerNotification.SendNotificaion("customerUpdate");

            return createdResult;
        }

        public Task<SYSCustomerViewModel> GetByIdAsync(long id)
        {
            return _customerBusiness.GetByIdAsync(id);
        }

        public async Task<PagedResult<SYSCustomerViewModel>> GetPagedAsync(int pageIndex, int pageSize, string searchString = null)
        {
            return await _customerBusiness.GetPagedAsync(pageIndex, pageSize, searchString);
        }

        public async Task UpdateAsync(SYSCustomerUpdateModel model, CancellationToken cancellationToken = default)
        {
            var checkExistSymbol = await _customerBusiness.GetByNameAsync(model.Name);
            if (checkExistSymbol != null && checkExistSymbol.Id != model.Id)
            {
                throw new System.Exception("Khách hàng đã tồn tại");
            }
            await _customerBusiness.UpdateAsync(model, cancellationToken);

            _customerNotification.SendNotificaion("customerUpdate");
        }

        public async Task<IEnumerable<CTGCustomerTypeModel>> GetCustomerType()
        {
            return await _customerBusiness.GetCustomerType();
        }
    }
}