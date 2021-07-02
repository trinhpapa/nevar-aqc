using NEVAR_AQC.Core.Models.Managements;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.Managerment
{
    public interface ISYSRoleFunctionBusiness
    {
        Task<IEnumerable<SYSRoleFunctionViewModel>> GetAllAsync();

        Task<IEnumerable<SYSRoleFunctionViewModel>> GetByRole(int roleId);

        Task<List<long>> GetFunctionIdByRole(int roleId);

        void DeleteByRoleAsync(int roleId);
    }
}