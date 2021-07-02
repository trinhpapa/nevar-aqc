using NEVAR_AQC.Core.Models.TestDepartment;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.TestDepartment
{
    public interface IIDTRTestPropertyBusiness
    {
        bool CheckHasProcess(long propertyId);

        Task UpdatePlanAsync(IDTRTestPropertyModel model, CancellationToken cancellationToken = default);

        Task UpdateTestMethod(IDTRTestPropertyModel model, CancellationToken cancellationToken = default);

        Task UpdateTestProcessAsync(IDTRTestPropertyModel model, CancellationToken cancellationToken = default);

        Task DeleteSummaryOfResultItemAsync(IDTRTestPropertyModel model, CancellationToken cancellationToken = default);

        Task<IDTRTestPropertyModel> GetPropertyForReportAsync(long propertyId);

        Task<IDTRTestPropertyModel> GetInvoiceByTestPropertyAsync(long propertyId);

        Task<IDTRTestPropertyModel> GetPropertyForResultAsync(long propertyId);
    }
}