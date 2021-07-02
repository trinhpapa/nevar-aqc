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
    public class TestPropertyController : Controller
    {
        private readonly ICTGTestPropertyService _cTgTestPropertyService;

        private readonly ICTGTestObjectService _cTgTestObjectService;

        public TestPropertyController(ICTGTestPropertyService cTgTestPropertyService,
            ICTGTestObjectService cTgTestObjectService)
        {
            _cTgTestPropertyService = cTgTestPropertyService;
            _cTgTestObjectService = cTgTestObjectService;
        }

        [FunctionFilter((int)ManagementFunction.TEST_PROPERTY_MANAGEMENT)]
        public async Task<IActionResult> Index()
        {
            ViewData["CTGTestObject"] = new SelectList(await _cTgTestObjectService.GetAllAsync(), "Id", "Name");
            return View();
        }

        public async Task<IActionResult> GetTestPropertyAsync()
        {
            var data = await _cTgTestPropertyService.GetAllAsync();
            return Json(data);
        }

        [FunctionFilter((int)ManagementFunction.TEST_PROPERTY_MANAGEMENT)]
        public async Task<IActionResult> GetPagedAsync(int pageIndex = 1,
           int pageSize = Constants.NumberOfRecordQueryDefault,
           string searchString = null)
        {
            var data = await _cTgTestPropertyService.GetPagedAsync(pageIndex, pageSize, searchString);
            return View("PartialView/TablePartial", data);
        }

        [FunctionFilter((int)ManagementFunction.CREATE_TEST_PROPERTY)]
        public async Task<IActionResult> CreateAsync(CTGTestPropertyModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreatedTime = DateTime.Now;
                    model.CreatedBy = Convert.ToInt64(HttpContext.Session.GetString("user-session"));
                    await _cTgTestPropertyService.CreateAsync(model);
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

        [FunctionFilter((int)ManagementFunction.DELETE_TEST_PROPERTY)]
        public async Task<IActionResult> DeleteAsync(CTGTestPropertyModel model)
        {
            try
            {
                model.DeletedTime = DateTime.Now;
                model.DeletedBy = Convert.ToInt64(HttpContext.Session.GetString("user-session"));
                await _cTgTestPropertyService.DeleteAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [FunctionFilter((int)ManagementFunction.UPDATE_TEST_PROPERTY)]
        public async Task<IActionResult> GetByIdAsync(long propertyId)
        {
            if (!ModelState.IsValid) return BadRequest();
            var fieldResult = await _cTgTestPropertyService.GetByIdAsync(propertyId);
            return Json(fieldResult);
        }

        [FunctionFilter((int)ManagementFunction.UPDATE_TEST_PROPERTY)]
        public async Task<IActionResult> UpdateAsync(CTGTestPropertyModel model)
        {
            try
            {
                model.ModifiedTime = DateTime.Now;
                model.ModifiedBy = Convert.ToInt64(HttpContext.Session.GetString("user-session"));
                await _cTgTestPropertyService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}