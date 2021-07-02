using NEVAR_AQC.Core.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace NEVAR_AQC.Data.TestDepartment
{
    public interface IIDTRTestPropertyRepository : IRepositoryBase<IDTRTestPropertyEntity>
    {
        void DeleteSummaryOfResultItem(long propertyId);

        IQueryable<IDTRTestPropertyEntity> GetTestPropertyForReport(long propertyId);

        IQueryable<IDTRTestPropertyEntity> GetTestPropertyForResult(long propertyId);

        IQueryable<IDTRTestPropertyEntity> GetInvoiceByTestProperty(long propertyId);
    }
}