using Microsoft.AspNetCore.Mvc;
using NEVAR_AQC.Service.SystemLog;
using System.Threading.Tasks;

namespace NEVAR_AQC.Controllers
{
    public class SystemLogController : Controller
    {
        private readonly ILOGLoginService _lOgLoginService;

        public SystemLogController(ILOGLoginService lOgLoginService)
        {
            _lOgLoginService = lOgLoginService;
        }

        public async Task<IActionResult> GetLogLogin()
        {
            var data = await _lOgLoginService.GetTopAsync();
            return Json(data);
        }
    }
}