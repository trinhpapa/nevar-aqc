using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEVAR_AQC.Core.Enums;
using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Filters;
using NEVAR_AQC.Service.Managements;
using System;
using System.Threading.Tasks;

namespace NEVAR_AQC.Controllers
{
    [SessionFilter]
    public class FieldController : Controller
    {
        private readonly ICTGFieldService _cTgFieldService;

        public FieldController(ICTGFieldService cTgFieldService)
        {
            _cTgFieldService = cTgFieldService;
        }
        [FunctionFilter((int)ManagementFunction.TEST_FIELD_MANAGEMENT)]
        public IActionResult Index()
        {
            return View();
        }

        [FunctionFilter((int)ManagementFunction.TEST_FIELD_MANAGEMENT)]
        public async Task<IActionResult> GetPagedAsync(int pageIndex = 1,
            int pageSize = Constants.NumberOfRecordQueryDefault,
            string searchString = null)
        {
            var data = await _cTgFieldService.GetPagedAsync(pageIndex, pageSize, searchString);
            return View("PartialView/TablePartial", data);
        }

        [FunctionFilter((int)ManagementFunction.CREATE_TEST_FIELD)]
        public async Task<IActionResult> CreateAsync(CTGFieldModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreatedTime = DateTime.Now;
                    model.CreatedBy = Convert.ToInt64(HttpContext.Session.GetString("user-session"));
                    await _cTgFieldService.CreateAsync(model);
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


        [FunctionFilter((int)ManagementFunction.DELETE_TEST_FIELD)]
        public async Task<IActionResult> DeleteAsync(CTGFieldModel model)
        {
            try
            {
                model.DeletedTime = DateTime.Now;
                model.DeletedBy = Convert.ToInt64(HttpContext.Session.GetString("user-session"));
                await _cTgFieldService.DeleteAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [FunctionFilter((int)ManagementFunction.UPDATE_TEST_FIELD)]
        public async Task<IActionResult> GetByIdAsync(long fieldId)
        {
            if (ModelState.IsValid)
            {
                var fieldResult = await _cTgFieldService.GetByIdAsync(fieldId);
                return Json(fieldResult);
            }
            return BadRequest();
        }

        [FunctionFilter((int)ManagementFunction.UPDATE_TEST_FIELD)]
        public async Task<IActionResult> UpdateAsync(CTGFieldModel model)
        {
            try
            {
                model.ModifiedTime = DateTime.Now;
                model.ModifiedBy = Convert.ToInt64(HttpContext.Session.GetString("user-session"));
                await _cTgFieldService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}