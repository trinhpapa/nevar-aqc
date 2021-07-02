using NEVAR_AQC.Business.Managerment;
using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Core.PagingHelper;
using NEVAR_AQC.Service.Managements;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Service.Facade.Managements
{
    public class CTGFieldService : ICTGFieldService
    {
        private ICTGFieldBusiness _cTGFieldBusiness;

        public CTGFieldService(ICTGFieldBusiness cTGFieldBusiness)
        {
            _cTGFieldBusiness = cTGFieldBusiness;
        }

        public async Task<CTGFieldModel> CreateAsync(CTGFieldModel model, CancellationToken cancellationToken = default)
        {
            var checkExistSymbol = await _cTGFieldBusiness.GetBySymbolAsync(model.Symbol);
            if (checkExistSymbol != null)
            {
                throw new System.Exception("Ký hiệu đã tồn tại");
            }
            return await _cTGFieldBusiness.CreateAsync(model, cancellationToken);
        }

        public async Task DeleteAsync(CTGFieldModel model, CancellationToken cancellationToken = default)
        {
            var canBeDelete = await _cTGFieldBusiness.CanBeDeleteAsync(model.Id);
            if (!canBeDelete)
            {
                throw new System.Exception("Lĩnh vực này đã được sử dụng!");
            }
            await _cTGFieldBusiness.DeleteAsync(model, cancellationToken);
        }

        public async Task<IEnumerable<CTGFieldModel>> GetAllAsync()
        {
            return await _cTGFieldBusiness.GetAllAsync();
        }

        public Task<CTGFieldModel> GetByIdAsync(long id)
        {
            return _cTGFieldBusiness.GetByIdAsync(id);
        }

        public async Task<PagedResult<CTGFieldModel>> GetPagedAsync(int pageIndex, int pageSize, string searchString = null)
        {
            return await _cTGFieldBusiness.GetPagedAsync(pageIndex, pageSize, searchString);
        }

        public async Task UpdateAsync(CTGFieldModel model, CancellationToken cancellationToken = default)
        {
            var checkExistSymbol = await _cTGFieldBusiness.GetBySymbolAsync(model.Symbol);
            if (checkExistSymbol != null && checkExistSymbol.Id != model.Id)
            {
                throw new System.Exception("Ký hiệu đã tồn tại");
            }
            await _cTGFieldBusiness.UpdateAsync(model, cancellationToken);
        }
    }
}