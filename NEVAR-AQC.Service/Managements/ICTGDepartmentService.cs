using NEVAR_AQC.Core.Models.Managements;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NEVAR_AQC.Service.Managements
{
    public interface ICTGDepartmentService
    {
        Task<IEnumerable<CTGDepartmentViewModel>> GetAllAsync();
    }
}