using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEVAR_AQC.Core.Enums;
using NEVAR_AQC.Core.Models.ReceptionDepartment;
using NEVAR_AQC.Core.Models.TestDepartment;
using NEVAR_AQC.Filters;
using NEVAR_AQC.Service.ReceptionDepartment;
using NEVAR_AQC.Service.Report;
using NEVAR_AQC.Service.TestDepartment;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NEVAR_AQC.Controllers
{
    //[SessionFilter]
    public class TestDepartmentController : Controller
    {
        private readonly ISYSRequirementInvoiceService _requirementInvoiceService;
        private readonly IImplementationPlanService _implementationPlanService;
        private readonly ITestPlanService _testPlanService;

        public TestDepartmentController(ISYSRequirementInvoiceService requirementInvoiceService,
            IImplementationPlanService implementationPlanService,
            ITestPlanService testPlanService)
        {
            _requirementInvoiceService = requirementInvoiceService;
            _implementationPlanService = implementationPlanService;
            _testPlanService = testPlanService;
        }

        [FunctionFilter((int)TestDepartmentFunction.INDEX)]
        public IActionResult Index()
        {
            return View();
        }

        [FunctionFilter((int)TestDepartmentFunction.MANAGER)]
        public IActionResult Manager()
        {
            return View();
        }

        [FunctionFilter((int)TestDepartmentFunction.ENGINEER)]
        public IActionResult Engineer()
        {
            return View();
        }

        [FunctionFilter((int)TestDepartmentFunction.MANAGER)]
        public async Task<IActionResult> GetInvoiceForManagerAsync(int pageIndex = 1,
            int pageSize = Constants.NumberOfRecordQueryDefault,
            string createdTime = null,
            string resultDay = null,
            int status = 0,
            string searchFilter = null)
        {
            var data = await _requirementInvoiceService.GetByDepartmentPagingAsync(pageIndex, pageSize, createdTime, resultDay, status, searchFilter);

            return View("PartialView/Manager-TablePartial", data);
        }

        [FunctionFilter((int)TestDepartmentFunction.ENGINEER)]
        public async Task<IActionResult> GetInvoiceForEngineerAsync(int pageIndex = 1,
            int pageSize = Constants.NumberOfRecordQueryDefault,
            DateTime? fromTime = null,
            DateTime? toTime = null,
            bool? acceptStatus = null,
            string searchFilter = null)
        {
            var userId = Convert.ToInt64(HttpContext.Session.GetString("user-session"));

            var data = await _requirementInvoiceService.GetDetailTestRequirementByImplementerAsync(userId, pageIndex, pageSize, fromTime, toTime, acceptStatus, searchFilter);

            return View("PartialView/Engineer-TablePartial", data);
        }

        [FunctionFilter((int)TestDepartmentFunction.RECEIVE_INVOICE)]
        public async Task<IActionResult> ChangeStatusAsync(SYSRequirementInvoiceUpdateStatusModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = Convert.ToInt64(HttpContext.Session.GetString("user-session"));

                model.ModifiedBy = userId;

                await _requirementInvoiceService.UpdateStatusAsync(model);

                return Ok();
            }
            return StatusCode(500);
        }

        [FunctionFilter((int)TestDepartmentFunction.CREATE_PLAN)]
        public async Task<IActionResult> GetDetailTestRequirementByIdAsync(long id)
        {
            if (ModelState.IsValid)
            {
                var data = await _requirementInvoiceService.GetDetailTestRequirementByIdAsync(id);
                return View("PartialView/PlanManagement", data);
            }
            return StatusCode(500);
        }

        //[FunctionFilter((int)TestDepartmentFunction.CREATE_PLAN)]
        public async Task<IActionResult> UpdatePlanAsync(IEnumerable<IDTRTestPropertyModel> model)
        {
            if (ModelState.IsValid)
            {
                var userId = 1;
                var datetimeNow = DateTime.Now;
                var idtrTestPropertyModels = model as IDTRTestPropertyModel[] ?? model.ToArray();
                foreach (var itemProperty in idtrTestPropertyModels)
                {
                    itemProperty.ModifiedBy = userId;
                    itemProperty.ModifiedTime = datetimeNow;
                    foreach (var itemImplementer in itemProperty.IDTRImplementerEntities)
                    {
                        itemImplementer.CreatedBy = userId;
                        itemImplementer.ModifiedTime = datetimeNow;
                    }
                }
                await _testPlanService.CreateAsync(idtrTestPropertyModels);
                return Ok();
            }
            return StatusCode(500);
        }

        [FunctionFilter((int)TestDepartmentFunction.ACCEPT_JOB)]
        public async Task<IActionResult> ImplementerAcceptAsync(long implementerId, long invoiceId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _testPlanService.ImplementerAcceptAsync(implementerId, invoiceId);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return StatusCode(500);
        }

        [FunctionFilter((int)TestDepartmentFunction.REPORT_PLAN)]
        public async Task<IActionResult> ImplementationPlanReport([FromRoute] long id, [FromQuery] int current_row = 11)
        {
            var data = await _requirementInvoiceService.GetDetailTestRequirementByIdAsync(id);
            var fileName = "KHTN_" + data.InvoiceNo + ".xlsx";
            var stream = _implementationPlanService.TestPlanReport(data, current_row);
            var buffer = (stream as MemoryStream)?.ToArray();

            MemoryStream ms = new MemoryStream();
            if (buffer != null) ms.Write(buffer, 0, buffer.Length);
            ms.Position = 0;

            return File(ms, "application/vnd.ms-excel", fileName);
        }

        [FunctionFilter((int)TestDepartmentFunction.SUMARIZE_THE_RESULTS)]
        public async Task<IActionResult> GetRequirementInvoiceForSummary(long invoiceId)
        {
            if (ModelState.IsValid)
            {
                var dataResult = await _requirementInvoiceService.GetByIdForSummaryAsync(invoiceId);
                return View("PartialView/SummaryOfResults", dataResult);
            }
            else
            {
                return BadRequest("Dữ liệu không đúng!");
            }
        }

        [FunctionFilter((int)TestDepartmentFunction.SUMARIZE_THE_RESULTS)]
        public async Task<IActionResult> SubmitSummaryOfResults(SYSRequirementInvoiceUpdateStatusModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _requirementInvoiceService.SubmitSummaryOfResults(model);
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

        [FunctionFilter((int)TestDepartmentFunction.SUMARIZE_THE_RESULTS)]
        public async Task<IActionResult> DeleteSummaryOfResultItemAsync(IDTRTestPropertyModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _testPlanService.DeleteSummaryOfResultItemAsync(model);
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
    }
}