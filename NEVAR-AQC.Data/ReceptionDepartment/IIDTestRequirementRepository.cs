using NEVAR_AQC.Core.Entities;
using System.Linq;

namespace NEVAR_AQC.Data.ReceptionDepartment
{
    public interface IIDTestRequirementRepository : IRepositoryBase<IDTestRequirementEntity>
    {
        IQueryable<IDTestRequirementEntity> TestRequirementReport(long specimentNo);
    }
}