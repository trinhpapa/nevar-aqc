using Microsoft.AspNetCore.Mvc;
using NEVAR_AQC.Filters;
using NEVAR_AQC.Service.Statistic;
using System.Threading.Tasks;

namespace NEVAR_AQC.Controllers
{
    [SessionFilter]
    public class StatisticController : Controller
    {
        private readonly IHomeStatisticService _homeStatisticService;

        public StatisticController(IHomeStatisticService homeStatisticService)
        {
            _homeStatisticService = homeStatisticService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> HomeStatisticAsync()
        {
            var data = await _homeStatisticService.GetHomeStatisticAsync();
            return Json(data);
        }

        public async Task<IActionResult> LineStatisticAsync()
        {
            var data = await _homeStatisticService.GetLineStatisticAsync();

            return Json(data);
        }
    }
}