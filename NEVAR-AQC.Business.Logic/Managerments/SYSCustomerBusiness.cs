using AutoMapper;
using NEVAR_AQC.Business.Managerment;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Core.PagingHelper;
using NEVAR_AQC.Data.Managements;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.Logic.Managerments
{
    public class SYSCustomerBusiness : ISYSCustomerBusiness
    {
        private IMapper _mapper;
        private ISYSCustomerRepository _customerRepository;
        private ICTGCustomerTypeRepository _cTGCustomerTypeRepository;

        public SYSCustomerBusiness(IMapper mapper,
            ISYSCustomerRepository customerRepository,
            ICTGCustomerTypeRepository cTGCustomerTypeRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _cTGCustomerTypeRepository = cTGCustomerTypeRepository;
        }

        public Task<IEnumerable<SYSCustomerViewModel>> GetAllAsync()
        {
            //Get data
            var data = _customerRepository.Find(x => (x.IsDeleted == false || x.IsDeleted == null));

            var result = _mapper.Map<IEnumerable<SYSCustomerViewModel>>(data);

            return Task.FromResult(result);
        }

        public Task<PagedResult<SYSCustomerViewModel>> GetPagedAsync(int pageIndex, int pageSize, string searchString = null)
        {
            var query = _customerRepository.Find(w => w.IsDeleted == false || w.IsDeleted == null, w => w.CTGCustomerTypeEntity);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(w => w.Name.Contains(searchString));
            }
            var totalRow = query.Count();

            query = query.OrderBy(w => w.Name).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            var result = _mapper.Map<IList<SYSCustomerViewModel>>(query);

            return Task.FromResult(new PagedResult<SYSCustomerViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                RowCount = totalRow,
                Results = result,
            });
        }

        public Task<SYSCustomerViewModel> GetByNameAsync(string customerName)
        {
            if (string.IsNullOrEmpty(customerName) || string.IsNullOrWhiteSpace(customerName))
            {
                return null;
            }

            //Get data
            var data = _customerRepository.FindSingle(x => x.Name == customerName && (x.IsDeleted == false || x.IsDeleted == null));

            var result = _mapper.Map<SYSCustomerViewModel>(data);

            return Task.FromResult(result);
        }

        public bool CheckIsExistByName(string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return true;
            }
            var isExist = _customerRepository.Find(w => w.Name.Contains(name)).AsEnumerable().Any();
            return isExist;
        }

        public Task<SYSCustomerCreateResultModel> CreateAsync(SYSCustomerCreateModel model, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<SYSCustomerEntity>(model);

            var createdResult = _customerRepository.Create(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _customerRepository.SaveChange();

            var result = _mapper.Map<SYSCustomerCreateResultModel>(createdResult);

            return Task.FromResult(result);
        }

        public Task UpdateAsync(SYSCustomerUpdateModel model, CancellationToken cancellationToken = default)
        {
            var entity = _customerRepository.FindSingle(w => w.Id == model.Id);
            entity.Name = model.Name;
            entity.CustomerTypeId = model.CustomerTypeId;
            entity.Address = model.Address;
            entity.Email = model.Email;
            entity.PhoneNumber = model.PhoneNumber;
            entity.Fax = model.Fax;

            _customerRepository.Update(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _customerRepository.SaveChange();

            return Task.CompletedTask;
        }

        public Task<SYSCustomerViewModel> GetByIdAsync(long customerId)
        {
            var entity = _customerRepository.FindSingle(w => w.Id == customerId);
            var result = _mapper.Map<SYSCustomerViewModel>(entity);
            return Task.FromResult(result);
        }

        public Task<IEnumerable<CTGCustomerTypeModel>> GetCustomerType()
        {
            var data = _cTGCustomerTypeRepository.Find(w => w.IsDeleted == false || w.IsDeleted == null);
            var result = _mapper.Map<IEnumerable<CTGCustomerTypeModel>>(data);
            return Task.FromResult(result);
        }
    }
}