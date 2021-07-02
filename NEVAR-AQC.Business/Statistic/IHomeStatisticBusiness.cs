using System.Linq;
using NEVAR_AQC.Core.Models.Statistic;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.Statistic
{
   public interface IHomeStatisticBusiness
   {
      Task<HomeStatisticModel> GetHomeStatisticAsync();

      Task<IQueryable<LineStatistic>> GetLineStatisticAsync();
   }
}