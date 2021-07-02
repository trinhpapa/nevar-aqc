#region	License
// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions</Copyright>
//     <Url> https://github.com/trinhpapa </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC </Project>
//     <File>
//         <Name> LoginController.cs </Name>
//         <Created> 27/2/2019 - 22:28 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//         LoginController.cs
//     </Summary>
// <License>
#endregion License

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NEVAR_AQC.Core.Models.System;
using NEVAR_AQC.Core.Models.User;
using NEVAR_AQC.Service.SystemLog;
using NEVAR_AQC.Service.User;
using System;
using System.Threading.Tasks;

namespace NEVAR_AQC.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ISYSUserService _userService;
        private readonly ILOGLoginService _lOgLoginService;

        public LoginController(ILogger<LoginController> logger,
            ISYSUserService userService,
            ILOGLoginService lOgLoginService)
        {
            _logger = logger;
            _userService = userService;
            _lOgLoginService = lOgLoginService;
        }

        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("user-session")))
            {
                return Redirect("/");
            }
            return View();
        }

        public async Task<IActionResult> LoginNow(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userData = await _userService.LoginByCredential(model);
                    if (userData != null)
                    {
                        await WriteLog(userData);

                        HttpContext.Session.SetString("user-session", userData.Id.ToString());
                        HttpContext.Session.SetString("username-session", userData.Username);
                        HttpContext.Session.SetString("user-function", string.Join("-", userData.FunctionKeys));
                        _logger.LogInformation("User Login: " + userData.Username);
                        return Json(userData);
                    }
                    else
                    {
                        return BadRequest("Tài khoản hoặc mật khẩu không đúng");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest("Dữ liệu nhập vào không chính xác");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/login");
        }

        private async Task WriteLog(UserSessionModel model)
        {
            var logLoginModel = new LOGLoginModel()
            {
                Username = model.Username,
                LoginTime = DateTime.Now,
                Browser = Request.Headers["User-Agent"].ToString()
            };
            await _lOgLoginService.CreateAsync(logLoginModel);
        }
    }
}