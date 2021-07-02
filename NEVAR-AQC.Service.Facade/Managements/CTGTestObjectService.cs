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
    public class CTGTestObjectService : ICTGTestObjectService
    {
        private ICTGTestObjectBusiness _cTGTestObjectBusiness;

        public CTGTestObjectService(ICTGTestObjectBusiness cTGTestObjectBusiness)
        {
            _cTGTestObjectBusiness = cTGTestObjectBusiness;
        }

        public async Task<CTGTestObjectModel> CreateAsync(CTGTestObjectModel model, CancellationToken cancellationToken = default)
        {
            var checkExistName = await _cTGTestObjectBusiness.GetByNameAsync(model.Name);
            if (checkExistName != null)
            {
                throw new Exception("Tên đối tượng đã tồn tại");
            }
            return await _cTGTestObjectBusiness.CreateAsync(model, cancellationToken);
        }

        public async Task DeleteAsync(CTGTestObjectModel model, CancellationToken cancellationToken = default)
        {
            var canDelete = await _cTGTestObjectBusiness.CanBeDeleteAsync(model.Id);
            if (!canDelete)
            {
                throw new Exception("Đối tượng này đã được sử dụng!");
            }
            await _cTGTestObjectBusiness.DeleteAsync(model, cancellationToken);
        }

        public async Task<IEnumerable<CTGTestObjectModel>> GetAllAsync()
        {
            return await _cTGTestObjectBusiness.GetAllAsync();
        }

        public Task<CTGTestObjectModel> GetByIdAsync(long id)
        {
            return _cTGTestObjectBusiness.GetByIdAsync(id);
        }

        public async Task<PagedResult<CTGTestObjectModel>> GetPagedAsync(int pageIndex, int pageSize, string searchString = null)
        {
            return await _cTGTestObjectBusiness.GetPagedAsync(pageIndex, pageSize, searchString);
        }

        public async Task UpdateAsync(CTGTestObjectModel model, CancellationToken cancellationToken = default)
        {
            var checkExistName = await _cTGTestObjectBusiness.GetByNameAsync(model.Name);
            if (checkExistName != null && checkExistName.Id != model.Id)
            {
                throw new Exception("Tên đối tượng đã tồn tại");
            }
            await _cTGTestObjectBusiness.UpdateAsync(model, cancellationToken);
        }
    }
}