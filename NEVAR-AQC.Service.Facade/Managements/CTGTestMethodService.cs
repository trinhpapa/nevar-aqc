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
    public class CTGTestMethodService : ICTGTestMethodService
    {
        private ICTGTestMethodBusiness _cTGTestMethodBusiness;

        public CTGTestMethodService(ICTGTestMethodBusiness cTGTestMethodBusiness)
        {
            _cTGTestMethodBusiness = cTGTestMethodBusiness;
        }

        public async Task<CTGTestMethodModel> CreateAsync(CTGTestMethodModel model, CancellationToken cancellationToken = default)
        {
            var checkExistEntity = await _cTGTestMethodBusiness.GetByNameAsync(model.Name, model.TestPropertyId);
            if (checkExistEntity != null)
            {
                throw new Exception("Tên phương pháp thử đã tồn tại");
            }
            return await _cTGTestMethodBusiness.CreateAsync(model, cancellationToken);
        }

        public async Task DeleteAsync(CTGTestMethodModel model, CancellationToken cancellationToken = default)
        {
            var canDelete = await _cTGTestMethodBusiness.CanBeDeleteAsync(model.Id);
            if (!canDelete)
            {
                throw new Exception("Phương pháp thử đã được sử dụng!");
            }
            await _cTGTestMethodBusiness.DeleteAsync(model, cancellationToken);
        }

        public async Task<IEnumerable<CTGTestMethodModel>> GetAllAsync()
        {
            return await _cTGTestMethodBusiness.GetAllAsync();
        }

        public Task<CTGTestMethodModel> GetByIdAsync(long id)
        {
            return _cTGTestMethodBusiness.GetByIdAsync(id);
        }

        public async Task<PagedResult<CTGTestMethodModel>> GetPagedAsync(int pageIndex, int pageSize, string searchString = null)
        {
            return await _cTGTestMethodBusiness.GetPagedAsync(pageIndex, pageSize, searchString);
        }

        public async Task UpdateAsync(CTGTestMethodModel model, CancellationToken cancellationToken = default)
        {
            var checkExistName = await _cTGTestMethodBusiness.GetByNameAsync(model.Name, model.TestPropertyId);
            if (checkExistName != null && checkExistName.Id != model.Id)
            {
                throw new Exception("Tên phương pháp thử đã tồn tại");
            }
            await _cTGTestMethodBusiness.UpdateAsync(model, cancellationToken);
        }
    }
}