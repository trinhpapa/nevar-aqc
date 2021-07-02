// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions </Copyright>
//     <Url> http://lehoangtrinh.com </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC </Project>
//     <File>
//         <Name> ReceptionDepartmentController.cs </Name>
//         <Created> 9/5/2019 - 13:58 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//
//     </Summary>
// <License>

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NEVAR_AQC.Core.Enums;
using NEVAR_AQC.Core.Models.ReceptionDepartment;
using NEVAR_AQC.Filters;
using NEVAR_AQC.Service.Managements;
using NEVAR_AQC.Service.ReceptionDepartment;
using NEVAR_AQC.Service.Report;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NEVAR_AQC.Controllers
{
    [SessionFilter]
    public class ReceptionDepartmentController : Controller
    {
        private readonly ISYSRequirementInvoiceService _requirementInvoiceService;
        private readonly ICTGRequirementTypeService _requirementTypeService;
        private readonly ICTGReturnInvoiceResultTypeService _cTgReturnInvoiceResultTypeService;
        private readonly ICTGFieldService _cTgFieldService;
        private readonly ITestRequirementReportService _testRequirementReportService;
        private readonly ITestResultReportService _testResultReportService;

        public ReceptionDepartmentController(ISYSRequirementInvoiceService requirementInvoiceService,
            ICTGRequirementTypeService requirementTypeService,
            ICTGReturnInvoiceResultTypeService cTgReturnInvoiceResultTypeService,
            ICTGFieldService cTgFieldService,
            ITestRequirementReportService testRequirementReportService,
            ITestResultReportService testResultReportService
            )
        {
            _requirementInvoiceService = requirementInvoiceService;
            _requirementTypeService = requirementTypeService;
            _cTgReturnInvoiceResultTypeService = cTgReturnInvoiceResultTypeService;
            _cTgFieldService = cTgFieldService;
            _testRequirementReportService = testRequirementReportService;
            _testResultReportService = testResultReportService;
        }

        [FunctionFilter((int)ReceptionDepartmentFunction.INDEX)]
        public async Task<IActionResult> Index()
        {
            ViewData["ReturnInvoiceResultType"] = await _cTgReturnInvoiceResultTypeService.GetStatusOnAsync();
            ViewData["CTGField"] = new SelectList(await _cTgFieldService.GetAllAsync(), "Id", "Name");
            return View();
        }

        [FunctionFilter((int)ReceptionDepartmentFunction.INDEX)]
        public async Task<IActionResult> GetRequirementTypeAsync()
        {
            var data = await _requirementTypeService.GetAllAsync();
            return Json(data);
        }

        [FunctionFilter((int)ReceptionDepartmentFunction.INDEX)]
        public async Task<IActionResult> GetInvoiceAsync(int pageIndex = 1,
            int pageSize = Constants.NumberOfRecordQueryDefault,
            int requirementType = 0,
            string createdTime = null,
            string resultDay = null,
            int status = 0,
            string searchFilter = null)
        {
            var userId = Convert.ToInt64(HttpContext.Session.GetString("user-session"));

            var data = await _requirementInvoiceService.GetByUserPagingAsync(userId, pageIndex, pageSize, requirementType, createdTime, resultDay, status, searchFilter);

            return View("PartialView/TablePartial", data);
        }

        [FunctionFilter((int)ReceptionDepartmentFunction.SUMMARY)]
        public async Task<IActionResult> GetSummaryAsync(int pageIndex = 1,
            int pageSize = Constants.NumberOfRecordQueryDefault,
            int requirementType = 0,
            string createdTime = null,
            string resultDay = null,
            int status = 0,
            string searchFilter = null)
        {
            var data = await _requirementInvoiceService.GetAllPagingAsync(pageIndex, pageSize, requirementType, createdTime, resultDay, status, searchFilter);

            return View("PartialView/SummaryTablePartial", data);
        }

        [FunctionFilter((int)ReceptionDepartmentFunction.CREATE)]
        public async Task<IActionResult> CreateAsync(SYSRequirementInvoiceModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreatedBy = Convert.ToInt64(HttpContext.Session.GetString("user-session"));
                    var createResult = await _requirementInvoiceService.CreateAsync(model);

                    return Ok(JsonConvert.SerializeObject(createResult, Formatting.None,
                            new JsonSerializerSettings()
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            }));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest("Model error");
        }

        [FunctionFilter((int)ReceptionDepartmentFunction.UPDATE)]
        public async Task<IActionResult> GetByIdAsync(long invoiceId)
        {
            if (ModelState.IsValid)
            {
                var getResult = await _requirementInvoiceService.GetByIdAsync(invoiceId);

                return Ok(JsonConvert.SerializeObject(getResult, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));
            }
            return StatusCode(500);
        }

        [FunctionFilter((int)ReceptionDepartmentFunction.DELETE)]
        public async Task<IActionResult> RemoveAsync(long invoiceId)
        {
            if (ModelState.IsValid)
            {
                var userId = Convert.ToInt64(HttpContext.Session.GetString("user-session"));
                await _requirementInvoiceService.RemoveAsync(invoiceId, userId);
                return Ok();
            }
            return BadRequest();
        }

        [FunctionFilter((int)ReceptionDepartmentFunction.REPORT)]
        public async Task<IActionResult> RequirementInvoiceReport([FromQuery] string no, [FromQuery] int edition)
        {
            var data = await _requirementInvoiceService.RequirementInvoiceReportAsync(no, edition);
            var fileName = data.InvoiceNo + ".xlsx";
            var stream = _testRequirementReportService.RequirementInvoiceReport(data);
            var buffer = (stream as MemoryStream)?.ToArray();

            MemoryStream ms = new MemoryStream();
            if (buffer != null) ms.Write(buffer, 0, buffer.Length);
            ms.Position = 0;

            return File(ms, "application/vnd.ms-excel", fileName);
        }

        [FunctionFilter((int)ReceptionDepartmentFunction.REPORT)]
        public async Task<IActionResult> TestResultReport([FromQuery] int id)
        {
            var data = await _requirementInvoiceService.GetByIdForReportAsync(id);

            if (string.IsNullOrEmpty(data.InvoiceResultNo))
            {
                var currentTime = DateTime.Now;
                var currentSerial = (await _requirementInvoiceService.GetCurrentResultSerial(currentTime.Year)) + 1;
                var resultNo = currentSerial.ToString("D3") + "/" + currentTime.ToString("yy") + "/" + data.SpecimenCode.Split("/")[0];

                await _requirementInvoiceService.UpdateInvoiceResultNo(data.Id, currentSerial,  currentTime.Year, resultNo, currentTime);

                data.InvoiceResultNo = resultNo;
                data.InvoiceResultDate = currentTime;
            }

            var lastEditionInvoice = await _requirementInvoiceService.GetCurrentEditionByInvoiceNo(data.SYSRequirementInvoiceEntity.InvoiceNo);

            if (lastEditionInvoice > 0)
            {
                var lastEditionData = await _requirementInvoiceService.GetByNoAndEditionAsync(data.SYSRequirementInvoiceEntity.InvoiceNo, lastEditionInvoice);
                data.SYSRequirementInvoiceEntity.SYSCustomerEntity.Name = lastEditionData.SYSCustomerEntity.Name;
                data.SYSRequirementInvoiceEntity.Representative = lastEditionData.Representative;
                data.SpecimenName = lastEditionData.IDTestRequirementEntities.Where(w => w.SpecimenCode == data.SpecimenCode).FirstOrDefault()?.SpecimenName;
                data.SpecimenSymbol = lastEditionData.IDTestRequirementEntities.Where(w => w.SpecimenCode == data.SpecimenCode).FirstOrDefault()?.SpecimenSymbol;
            }
            var fileName = "KQTN_" + data.SpecimenCode + ".xlsx";
            var stream = _testResultReportService.TestResultBySpeciment(data);
            var buffer = (stream as MemoryStream)?.ToArray();

            MemoryStream ms = new MemoryStream();
            if (buffer != null) ms.Write(buffer, 0, buffer.Length);
            ms.Position = 0;

            return File(ms, "application/vnd.ms-excel", fileName);
        }

        [FunctionFilter((int)ReceptionDepartmentFunction.CREATE)]
        public async Task<IActionResult> GetByIdForUpdateAsync(long invoiceId)
        {
            if (ModelState.IsValid)
            {
                var data = await _requirementInvoiceService.GetByIdForUpdateAsync(invoiceId);
                return View("PartialView/EditInvoicePartial", data);
            }
            else
            {
                return BadRequest();
            }
        }

        [FunctionFilter((int)ReceptionDepartmentFunction.CREATE)]
        public async Task<IActionResult> UpdateInvoiceAsync(SYSRequirementInvoiceModel model)
        {
            if (ModelState.IsValid)
            {
                await _requirementInvoiceService.UpdateInvoiceAsync(model);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> GetListEditionByInvoiceNo(string invoiceNo)
        {
            if (ModelState.IsValid)
            {
                var edtion = await _requirementInvoiceService.GetCurrentEditionByInvoiceNo(invoiceNo);
                return Ok(edtion);
            }
            else
            {
                return BadRequest("Model error!");
            }
        }
    }
}