using Microsoft.EntityFrameworkCore;
using NEVAR_AQC.Business.Statistic;
using NEVAR_AQC.Core.Models.Statistic;
using NEVAR_AQC.Service.Statistic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NEVAR_AQC.Service.Facade.Statistic
{
   public class HomeStatisticService : IHomeStatisticService
   {
      private readonly IHomeStatisticBusiness _homeStatisticBusiness;

      public HomeStatisticService(IHomeStatisticBusiness homeStatisticBusiness)
      {
         _homeStatisticBusiness = homeStatisticBusiness;
      }

      public async Task<HomeStatisticModel> GetHomeStatisticAsync()
      {
         return await _homeStatisticBusiness.GetHomeStatisticAsync();
      }

      public async Task<List<LineStatistic>> GetLineStatisticAsync()
      {
         var data = await _homeStatisticBusiness.GetLineStatisticAsync();
         return await data.ToListAsync();
      }
   }
}