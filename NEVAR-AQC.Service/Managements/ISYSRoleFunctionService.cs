using NEVAR_AQC.Core.Models.Managements;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NEVAR_AQC.Service.Managements
{
    public interface ISYSRoleFunctionService
    {
        Task<IEnumerable<SYSRoleFunctionViewModel>> GetByRole(int roleId);

        Task<IEnumerable<SYSRoleFunctionViewModel>> GetAllAsync();
    }
}