using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class CustomerController : Controller
    {
        private readonly ISYSCustomerService _customerService;

        public CustomerController(ISYSCustomerService customerService)
        {
            _customerService = customerService;
        }

        [FunctionFilter((int)ManagementFunction.CUSTOMER_MANAGEMENT)]
        public async Task<IActionResult> Index()
        {
            ViewData["CustomerType"] = new SelectList(await _customerService.GetCustomerType(), "Id", "Name");
            return View();
        }

        public async Task<IActionResult> CustomerList()
        {
            var data = await _customerService.GetAllAsync();
            return Json(data);
        }

        [FunctionFilter((int)ManagementFunction.CUSTOMER_MANAGEMENT)]
        public async Task<IActionResult> GetPagedAsync(int pageIndex = 1,
            int pageSize = Constants.NumberOfRecordQueryDefault,
            string searchString = null)
        {
            var data = await _customerService.GetPagedAsync(pageIndex, pageSize, searchString);
            return View("PartialView/TablePartial", data);
        }

        [FunctionFilter((int)ManagementFunction.CREATE_CUSTOMER)]
        public async Task<IActionResult> CreateAsync(SYSCustomerCreateModel model)
        {
            if (!ModelState.IsValid) return BadRequest("Dữ liệu nhập vào không đúng");
            try
            {
                model.CreatedTime = DateTime.Now;
                model.CreatedBy = Convert.ToInt64(HttpContext.Session.GetString("user-session"));
                await _customerService.CreateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [FunctionFilter((int)ManagementFunction.UPDATE_CUSTOMER)]
        public async Task<IActionResult> GetByIdAsync(int customerId)
        {
            if (!ModelState.IsValid) return BadRequest();
            var getResult = await _customerService.GetByIdAsync(customerId);
            return Ok(JsonConvert.SerializeObject(getResult, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));
        }

        [FunctionFilter((int)ManagementFunction.UPDATE_CUSTOMER)]
        public async Task<IActionResult> UpdateAsync(SYSCustomerUpdateModel model)
        {
            try
            {
                model.ModifiedTime = DateTime.Now;
                model.ModifiedBy = Convert.ToInt64(HttpContext.Session.GetString("user-session"));
                await _customerService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}