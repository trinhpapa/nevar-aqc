using System.Linq;
using NEVAR_AQC.Business.Statistic;
using NEVAR_AQC.Core.Models.Statistic;
using NEVAR_AQC.Data.Statistic;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.Logic.Statistic
{
   public class HomeStatisticBusiness : IHomeStatisticBusiness
   {
      private readonly IHomeStatisticRepository _homeStatisticRepository;

      public HomeStatisticBusiness(IHomeStatisticRepository homeStatisticRepository)
      {
         _homeStatisticRepository = homeStatisticRepository;
      }

      public Task<HomeStatisticModel> GetHomeStatisticAsync()
      {
         return Task.FromResult(_homeStatisticRepository.GetHomeStatistic());
      }

      public Task<IQueryable<LineStatistic>> GetLineStatisticAsync()
      {
         return _homeStatisticRepository.GetLineStatistic();
      }
   }
}