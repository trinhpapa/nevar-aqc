// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions </Copyright>
//     <Url> http://lehoangtrinh.com </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC </Project>
//     <File>
//         <Name> UserController.cs </Name>
//         <Created> 9/5/2019 - 13:58 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//
//     </Summary>
// <License>

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using NEVAR_AQC.Core.Enums;
using NEVAR_AQC.Core.Models.User;
using NEVAR_AQC.Filters;
using NEVAR_AQC.Service.Managements;
using NEVAR_AQC.Service.User;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace NEVAR_AQC.Controllers
{
    [SessionFilter]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ISYSUserService _sYsUserService;
        private readonly ICTGDepartmentService _cTgDepartmentService;
        private readonly ICTGRoleService _cTgRoleService;

        public UserController(IConfiguration configuration,
            ISYSUserService sYsUserService,
            ICTGDepartmentService cTgDepartmentService,
            ICTGRoleService cTgRoleService)
        {
            _configuration = configuration;
            _sYsUserService = sYsUserService;
            _cTgDepartmentService = cTgDepartmentService;
            _cTgRoleService = cTgRoleService;
        }

        [FunctionFilter((int)ManagementFunction.USER_MANAGEMENT)]
        public async Task<IActionResult> Index()
        {
            ViewData["DepartmentList"] = new SelectList(await _cTgDepartmentService.GetAllAsync(), "Id", "Name");
            ViewData["RoleList"] = new SelectList(await _cTgRoleService.GetAllAsync(), "Id", "Name");
            return View();
        }

        [FunctionFilter((int)ManagementFunction.USER_MANAGEMENT)]
        public async Task<IActionResult> GetPagedAsync(
            int pageIndex = 1,
            int pageSize = Constants.NumberOfRecordQueryDefault,
            string searchString = null)
        {
            var data = await _sYsUserService.GetPagingAsync(pageIndex, pageSize, searchString);
            return View("PartialView/TablePartial", data);
        }

        [FunctionFilter((int)ManagementFunction.CREATE_USER)]
        public async Task<IActionResult> CreateAsync(SYSUserCreateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var createdResult = await _sYsUserService.CreateAsync(model);
                    return Ok(createdResult);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("Dữ liệu nhập vào không đúng!");
            }
        }

        [FunctionFilter((int)ManagementFunction.UPDATE_USER)]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var data = await _sYsUserService.GetByIdAsync(id);

            return Ok(JsonConvert.SerializeObject(data, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));
        }

        [FunctionFilter((int)ManagementFunction.UPDATE_USER)]
        public async Task<IActionResult> UpdateAsync(SYSUserUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _sYsUserService.UpdateAsync(model);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("Dữ liệu nhập vào không đúng!");
            }
        }

        [FunctionFilter((int)ManagementFunction.RESET_USER_PASSWORD)]
        public async Task<IActionResult> UpdatePasswordAsync(SYSUserUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (string.IsNullOrEmpty(model.PasswordOrigin))
                    {
                        model.PasswordOrigin = _configuration.GetValue<string>("SystemSettings:PasswordOriginDefault");
                    }
                    try
                    {
                        await _sYsUserService.UpdatePasswordAsync(model);

                        return Ok(model.PasswordOrigin);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("Dữ liệu nhập vào không đúng!");
            }
        }

        public async Task<IActionResult> GetAllAsync()
        {
            var data = await _sYsUserService.GetAllAsync(null, null);
            return Json(data);
        }
    }
}