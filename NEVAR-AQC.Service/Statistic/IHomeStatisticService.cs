using System.Collections.Generic;
using NEVAR_AQC.Core.Models.Statistic;
using System.Threading.Tasks;

namespace NEVAR_AQC.Service.Statistic
{
    public interface IHomeStatisticService
    {
        Task<HomeStatisticModel> GetHomeStatisticAsync();

        Task<List<LineStatistic>> GetLineStatisticAsync();
    }
}