using NEVAR_AQC.Business.Managerment;
using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Core.PagingHelper;
using NEVAR_AQC.Service.Managements;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Service.Facade.Managements
{
    public class CTGRoleService : ICTGRoleService
    {
        private ICTGRoleBusiness _cTGRoleBusiness;
        private ISYSRoleFunctionBusiness _sYSRoleFunctionBusiness;

        public CTGRoleService(ICTGRoleBusiness cTGRoleBusiness,
            ISYSRoleFunctionBusiness sYSRoleFunctionBusiness)
        {
            _cTGRoleBusiness = cTGRoleBusiness;
            _sYSRoleFunctionBusiness = sYSRoleFunctionBusiness;
        }

        public async Task<CTGRoleModel> CreateAsync(CTGRoleModel model, CancellationToken cancellationToken = default)
        {
            return await _cTGRoleBusiness.CreateAsync(model, cancellationToken);
        }

        public async Task DeleteAsync(CTGRoleModel model, CancellationToken cancellationToken = default)
        {
            var canBeDelete = await _cTGRoleBusiness.CanBeDeleteAsync(model.Id);
            if (!canBeDelete)
            {
                throw new System.Exception("Quyền này đã được sử dụng!");
            }
            await _cTGRoleBusiness.DeleteAsync(model, cancellationToken);
        }

        public async Task<IEnumerable<CTGRoleModel>> GetAllAsync()
        {
            return await _cTGRoleBusiness.GetAllAsync();
        }

        public async Task<CTGRoleModel> GetByIdAsync(int id)
        {
            return await _cTGRoleBusiness.GetByIdAsync(id);
        }

        public async Task<PagedResult<CTGRoleModel>> GetPagedAsync(int pageIndex, int pageSize, string searchString = null)
        {
            return await _cTGRoleBusiness.GetPagedAsync(pageIndex, pageSize, searchString);
        }

        public async Task UpdateAsync(CTGRoleModel model, CancellationToken cancellationToken = default)
        {
            _sYSRoleFunctionBusiness.DeleteByRoleAsync(model.Id);
            await _cTGRoleBusiness.UpdateAsync(model, cancellationToken);
        }
    }
}