using NEVAR_AQC.Core.Models.Managements;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.Managerment
{
    public interface ICTGRequirementTypeBusiness
    {
        Task<IEnumerable<CTGRequirementTypeViewModel>> GetAllAsync();

        Task<CTGRequirementTypeViewModel> GetByIdAsync(int id);
    }
}