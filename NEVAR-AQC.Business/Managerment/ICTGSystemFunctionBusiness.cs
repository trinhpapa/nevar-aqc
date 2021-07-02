using NEVAR_AQC.Core.Models.Managements;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.Managerment
{
    public interface ICTGSystemFunctionBusiness
    {
        Task<IEnumerable<CTGSystemFunctionModel>> GetAllAsync();

        Task<List<int>> GetKeyById(List<long> functionId);
    }
}