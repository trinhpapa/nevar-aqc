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
    public class TestMethodController : Controller
    {
        private readonly ICTGTestMethodService _cTgTestMethodService;
        private readonly ICTGTestPropertyService _cTgTestPropertyService;
        private readonly ICTGTestObjectService _cTgTestObjectService;

        public TestMethodController(ICTGTestMethodService cTgTestMethodService,
            ICTGTestPropertyService cTgTestPropertyService,
            ICTGTestObjectService cTgTestObjectService)
        {
            _cTgTestMethodService = cTgTestMethodService;
            _cTgTestPropertyService = cTgTestPropertyService;
            _cTgTestObjectService = cTgTestObjectService;
        }

        [FunctionFilter((int)ManagementFunction.TEST_METHOD_MANAGEMENT)]
        public async Task<IActionResult> Index()
        {
            ViewData["CTGTestObject"] = new SelectList(await _cTgTestObjectService.GetAllAsync(), "Id", "Name");
            return View();
        }

        public async Task<IActionResult> GetTestPropertyByObject(long objectId)
        {
            var data = await _cTgTestPropertyService.GetByObjectIdAsync(objectId);
            return Json(data);
        }

        public async Task<IActionResult> GetTestMethodAsync()
        {
            var data = await _cTgTestMethodService.GetAllAsync();
            return Json(data);
        }

        [FunctionFilter((int)ManagementFunction.TEST_METHOD_MANAGEMENT)]
        public async Task<IActionResult> GetPagedAsync(int pageIndex = 1,
           int pageSize = Constants.NumberOfRecordQueryDefault,
           string searchString = null)
        {
            var data = await _cTgTestMethodService.GetPagedAsync(pageIndex, pageSize, searchString);
            return View("PartialView/TablePartial", data);
        }

        [FunctionFilter((int)ManagementFunction.CREATE_TEST_METHOD)]
        public async Task<IActionResult> CreateAsync(CTGTestMethodModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreatedTime = DateTime.Now;
                    model.CreatedBy = Convert.ToInt64(HttpContext.Session.GetString("user-session"));
                    await _cTgTestMethodService.CreateAsync(model);
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

        [FunctionFilter((int)ManagementFunction.DELETE_TEST_METHOD)]
        public async Task<IActionResult> DeleteAsync(CTGTestMethodModel model)
        {
            try
            {
                model.DeletedTime = DateTime.Now;
                model.DeletedBy = Convert.ToInt64(HttpContext.Session.GetString("user-session"));
                await _cTgTestMethodService.DeleteAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [FunctionFilter((int)ManagementFunction.UPDATE_TEST_METHOD)]
        public async Task<IActionResult> GetByIdAsync(long methodId)
        {
            if (ModelState.IsValid)
            {
                var fieldResult = await _cTgTestMethodService.GetByIdAsync(methodId);
                return Json(fieldResult);
            }
            return BadRequest();
        }

        [FunctionFilter((int)ManagementFunction.UPDATE_TEST_METHOD)]
        public async Task<IActionResult> UpdateAsync(CTGTestMethodModel model)
        {
            try
            {
                model.ModifiedTime = DateTime.Now;
                model.ModifiedBy = Convert.ToInt64(HttpContext.Session.GetString("user-session"));
                await _cTgTestMethodService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}