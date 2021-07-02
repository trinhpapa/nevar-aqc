using NEVAR_AQC.Core.Models.Managements;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.Managerment
{
    public interface ICTGDepartmentBusiness
    {
        Task<IEnumerable<CTGDepartmentViewModel>> GetAllAsync();
    }
}