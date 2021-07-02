using NEVAR_AQC.Core.Models.TestDepartment;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Service.TestDepartment
{
    public interface ITestPlanService
    {
        Task CreateAsync(IEnumerable<IDTRTestPropertyModel> model, CancellationToken cancellationToken = default);

        Task ImplementerAcceptAsync(long implementerId, long invoiceId, CancellationToken cancellationToken = default);

        Task UpdateTestProcess(IDTRTestPropertyModel model, CancellationToken cancellationToken = default);

        Task DeleteSummaryOfResultItemAsync(IDTRTestPropertyModel model, CancellationToken cancellationToken = default);

        Task<IDTRTestPropertyModel> GetPropertyForReportAsync(long propertyId);

        Task<IDTRTestPropertyModel> GetPropertyForResultAsync(long propertyId);
    }
}