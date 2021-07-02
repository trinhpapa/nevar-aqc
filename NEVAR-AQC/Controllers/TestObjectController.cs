using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NEVAR_AQC.Core.Enums;
using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Filters;
using NEVAR_AQC.Service.Managements;
using System;
using System.Threading.Tasks;

namespace NEVAR_AQC.Controllers
{
    [SessionFilter]
    public class TestObjectController : Controller
    {
        private readonly ICTGTestObjectService _cTgTestObjectService;
        private readonly ICTGFieldService _cTgFieldService;

        public TestObjectController(ICTGTestObjectService cTgTestObjectService,
            ICTGFieldService cTgFieldService)
        {
            _cTgTestObjectService = cTgTestObjectService;
            _cTgFieldService = cTgFieldService;
        }

        [FunctionFilter((int)ManagementFunction.TEST_OBJECT_MANAGEMENT)]
        public async Task<IActionResult> Index()
        {
            ViewData["CTGField"] = new SelectList(await _cTgFieldService.GetAllAsync(), "Id", "Name");
            return View();
        }

        public async Task<IActionResult> GetTestObjectAsync()
        {
            var data = await _cTgTestObjectService.GetAllAsync();
            return Json(data);
        }

        [FunctionFilter((int)ManagementFunction.TEST_OBJECT_MANAGEMENT)]
        public async Task<IActionResult> GetPagedAsync(int pageIndex = 1,
            int pageSize = Constants.NumberOfRecordQueryDefault,
            string searchString = null)
        {
            var data = await _cTgTestObjectService.GetPagedAsync(pageIndex, pageSize, searchString);
            return View("PartialView/TablePartial", data);
        }

        [FunctionFilter((int)ManagementFunction.CREATE_TEST_OBJECT)]
        public async Task<IActionResult> CreateAsync(CTGTestObjectModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreatedTime = DateTime.Now;
                    model.CreatedBy = Convert.ToInt64(HttpContext.Session.GetString("user-session"));
                    await _cTgTestObjectService.CreateAsync(model);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("Dữ liệu nhập vào không đúng");
            }
        }

        [FunctionFilter((int)ManagementFunction.DELETE_TEST_OBJECT)]
        public async Task<IActionResult> DeleteAsync(CTGTestObjectModel model)
        {
            try
            {
                model.DeletedTime = DateTime.Now;
                model.DeletedBy = Convert.ToInt64(HttpContext.Session.GetString("user-session"));
                await _cTgTestObjectService.DeleteAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [FunctionFilter((int)ManagementFunction.UPDATE_TEST_OBJECT)]
        public async Task<IActionResult> GetByIdAsync(long objectId)
        {
            if (ModelState.IsValid)
            {
                var fieldResult = await _cTgTestObjectService.GetByIdAsync(objectId);
                return Json(fieldResult);
            }
            return BadRequest();
        }

        [FunctionFilter((int)ManagementFunction.UPDATE_TEST_OBJECT)]
        public async Task<IActionResult> UpdateAsync(CTGTestObjectModel model)
        {
            try
            {
                model.ModifiedTime = DateTime.Now;
                model.ModifiedBy = Convert.ToInt64(HttpContext.Session.GetString("user-session"));
                await _cTgTestObjectService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}