using NEVAR_AQC.Core.Models.TestDepartment;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.TestDepartment
{
    public interface IIDTRImplementerBusiness
    {
        Task CreateAsync(IDTRImplementerModel model, CancellationToken cancellationToken = default);

        Task UpdateAcceptAsync(long implementerId, CancellationToken cancellationToken = default);

        Task DeleteAsync(IDTRImplementerModel model, CancellationToken cancellationToken = default);

        bool CheckImplementerHasAccept(long implementerId);

        bool CheckImplementerByProperty(long userId, long propertyId);

        bool CheckIsImplementer(long userId, long propertyId);

        Task UpdateTimeToStartAsync(long implementerId);

        Task UpdateTimeToReportAsync(long implementerId);
    }
}