using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEVAR_AQC.Core.Enums;
using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Filters;
using NEVAR_AQC.Service.Managements;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace NEVAR_AQC.Controllers
{
    [SessionFilter]
    public class RoleController : Controller
    {
        private readonly ICTGRoleService _cTgRoleService;
        private readonly ICTGSystemFunctionService _cTgSystemFunctionService;

        public RoleController(ICTGRoleService cTgRoleService,
             ICTGSystemFunctionService cTgSystemFunctionService)
        {
            _cTgRoleService = cTgRoleService;
            _cTgSystemFunctionService = cTgSystemFunctionService;
        }

        [FunctionFilter((int)ManagementFunction.ROLE_MANAGEMENT)]
        public IActionResult Index()
        {
            return View();
        }

        [FunctionFilter((int)ManagementFunction.ROLE_MANAGEMENT)]
        public async Task<IActionResult> GetPagedAsync(int pageIndex = 1,
            int pageSize = Constants.NumberOfRecordQueryDefault,
            string searchString = null)
        {
            var data = await _cTgRoleService.GetPagedAsync(pageIndex, pageSize, searchString);
            return View("PartialView/TablePartial", data);
        }

        public async Task<IActionResult> GetAllSystemFunction()
        {
            var data = await _cTgSystemFunctionService.GetAllAsync();
            return Json(data);
        }

        [FunctionFilter((int)ManagementFunction.CREATE_ROLE)]
        public async Task<IActionResult> CreateAsync(CTGRoleModel model)
        {
            if (!ModelState.IsValid) return BadRequest("Dữ liệu nhập vào không đúng");
            try
            {
                model.CreatedTime = DateTime.Now;
                model.CreatedBy = Convert.ToInt64(HttpContext.Session.GetString("user-session"));
                await _cTgRoleService.CreateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [FunctionFilter((int)ManagementFunction.DELETE_ROLE)]
        public async Task<IActionResult> DeleteAsync(CTGRoleModel model)
        {
            try
            {
                model.DeletedTime = DateTime.Now;
                model.DeletedBy = Convert.ToInt64(HttpContext.Session.GetString("user-session"));
                await _cTgRoleService.DeleteAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [FunctionFilter((int)ManagementFunction.UPDATE_ROLE)]
        public async Task<IActionResult> GetByIdAsync(int roleId)
        {
            if (!ModelState.IsValid) return BadRequest();
            var getResult = await _cTgRoleService.GetByIdAsync(roleId);
            return Ok(JsonConvert.SerializeObject(getResult, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));
        }

        [FunctionFilter((int)ManagementFunction.UPDATE_ROLE)]
        public async Task<IActionResult> UpdateAsync(CTGRoleModel model)
        {
            try
            {
                model.ModifiedTime = DateTime.Now;
                model.ModifiedBy = Convert.ToInt64(HttpContext.Session.GetString("user-session"));
                await _cTgRoleService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}