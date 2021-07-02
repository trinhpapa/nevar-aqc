using NEVAR_AQC.Business.Managerment;
using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Core.PagingHelper;
using NEVAR_AQC.Service.Managements;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Service.Facade.Managements
{
    public class CTGTestPropertyService : ICTGTestPropertyService
    {
        private ICTGTestPropertyBusiness _cTGTestPropertyBusiness;

        public CTGTestPropertyService(ICTGTestPropertyBusiness cTGTestPropertyBusiness)
        {
            _cTGTestPropertyBusiness = cTGTestPropertyBusiness;
        }

        public async Task<CTGTestPropertyModel> CreateAsync(CTGTestPropertyModel model, CancellationToken cancellationToken = default)
        {
            var checkExistEntity = await _cTGTestPropertyBusiness.GetByNameAsync(model.Name, model.ObjectId);
            if (checkExistEntity != null)
            {
                    throw new Exception("Tên chỉ tiêu đã tồn tại");
            }
            return await _cTGTestPropertyBusiness.CreateAsync(model, cancellationToken);
        }

        public async Task DeleteAsync(CTGTestPropertyModel model, CancellationToken cancellationToken = default)
        {
            var canDelete = await _cTGTestPropertyBusiness.CanBeDeleteAsync(model.Id);
            if (!canDelete)
            {
                throw new Exception("Chỉ tiêu đã được sử dụng!");
            }
            await _cTGTestPropertyBusiness.DeleteAsync(model, cancellationToken);
        }

        public async Task<IEnumerable<CTGTestPropertyModel>> GetAllAsync()
        {
            return await _cTGTestPropertyBusiness.GetAllAsync();
        }

        public Task<CTGTestPropertyModel> GetByIdAsync(long id)
        {
            return _cTGTestPropertyBusiness.GetByIdAsync(id);
        }

        public async Task<IEnumerable<CTGTestPropertyModel>> GetByObjectIdAsync(long objectId)
        {
            return await _cTGTestPropertyBusiness.GetByObjectIdAsync(objectId);
        }

        public async Task<PagedResult<CTGTestPropertyModel>> GetPagedAsync(int pageIndex, int pageSize, string searchString = null)
        {
            return await _cTGTestPropertyBusiness.GetPagedAsync(pageIndex, pageSize, searchString);
        }

        public async Task UpdateAsync(CTGTestPropertyModel model, CancellationToken cancellationToken = default)
        {
            var checkExistName = await _cTGTestPropertyBusiness.GetByNameAsync(model.Name, model.ObjectId);
            if (checkExistName != null && checkExistName.Id != model.Id)
            {
                throw new Exception("Tên chỉ tiêu đã tồn tại");
            }
            await _cTGTestPropertyBusiness.UpdateAsync(model, cancellationToken);
        }

    }
}