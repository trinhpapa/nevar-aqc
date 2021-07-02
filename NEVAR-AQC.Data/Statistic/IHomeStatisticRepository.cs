using NEVAR_AQC.Core.Models.Statistic;
using System.Linq;
using System.Threading.Tasks;

namespace NEVAR_AQC.Data.Statistic
{
   public interface IHomeStatisticRepository
   {
      Task<IQueryable<LineStatistic>> GetLineStatistic();

      HomeStatisticModel GetHomeStatistic();
   }
}