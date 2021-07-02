using Microsoft.AspNetCore.Mvc;
using NEVAR_AQC.Core.Enums;
using NEVAR_AQC.Core.Models.TestDepartment;
using NEVAR_AQC.Filters;
using NEVAR_AQC.Service.ReceptionDepartment;
using NEVAR_AQC.Service.Report;
using NEVAR_AQC.Service.TestDepartment;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NEVAR_AQC.Controllers
{
    [SessionFilter]
    public class TestProcessController : Controller
    {
        private readonly ITestPlanService _testPlanService;
        private readonly ISYSRequirementInvoiceService _sYsRequirementInvoiceService;
        private readonly ITestProcessReportService _testProcessReportService;
        private readonly ITestResultReportService _testResultReportService;

        public TestProcessController(ITestPlanService testPlanService,
            ITestProcessReportService testProcessReportService,
            ITestResultReportService testResultReportService,
            ISYSRequirementInvoiceService sYsRequirementInvoiceService)
        {
            _testPlanService = testPlanService;
            _testProcessReportService = testProcessReportService;
            _testResultReportService = testResultReportService;
            _sYsRequirementInvoiceService = sYsRequirementInvoiceService;
        }

        [FunctionFilter((int)TestDepartmentFunction.ENGINEER)]
        public IActionResult WeightMethodTemplate()
        {
            return View("PartialView/WeightMethod");
        }

        [FunctionFilter((int)TestDepartmentFunction.ENGINEER)]
        public IActionResult VolumeMethodTemplate()
        {
            return View("PartialView/VolumeMethod");
        }

        [FunctionFilter((int)TestDepartmentFunction.ENGINEER)]
        public IActionResult OtherMethodTemplate()
        {
            return View("PartialView/OtherMethod");
        }

        [FunctionFilter((int)TestDepartmentFunction.ENGINEER)]
        public IActionResult AASMethodTemplate()
        {
            return View("PartialView/AASMethod");
        }

        [FunctionFilter((int)TestDepartmentFunction.REPORT_TEST_PROCESS)]
        public async Task<IActionResult> UpdateTestProcessAsync(IDTRTestPropertyModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                await _testPlanService.UpdateTestProcess(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> TestProcessReport([FromQuery]long property_id)
        {
            var data = await _testPlanService.GetPropertyForReportAsync(property_id);
            if (data.IDTRTestProcessWeightMethodEntities.Any())
            {
                var stream = _testProcessReportService.WeightMethodReport(data);
                var buffer = (stream as MemoryStream)?.ToArray();

                MemoryStream ms = new MemoryStream();
                if (buffer != null) ms.Write(buffer, 0, buffer.Length);
                ms.Position = 0;

                return File(ms, "application/vnd.ms-excel", "PPTRONGLUONG-" + data.Id + ".xlsx");
            }
            if (data.IDTRTestProcessVolumeMethodEntities.Any())
            {
                var stream = _testProcessReportService.VolumeMethodReport(data);
                var buffer = (stream as MemoryStream)?.ToArray();

                MemoryStream ms = new MemoryStream();
                if (buffer != null) ms.Write(buffer, 0, buffer.Length);
                ms.Position = 0;

                return File(ms, "application/vnd.ms-excel", "PPTHETICH-" + data.Id + ".xlsx");
            }
            if (data.IDTRTestProcessOtherMethodEntities.Any())
            {
                var stream = _testProcessReportService.OtherMethodReport(data);
                var buffer = (stream as MemoryStream)?.ToArray();

                MemoryStream ms = new MemoryStream();
                if (buffer != null) ms.Write(buffer, 0, buffer.Length);
                ms.Position = 0;

                return File(ms, "application/vnd.ms-excel", "PPKHAC-" + data.Id + ".xlsx");
            }
            if (data.IDTRTestProcessAASUCVISAESMethodEntities.Any())
            {
                var stream = _testProcessReportService.AASMethodReport(data);
                var buffer = (stream as MemoryStream)?.ToArray();

                MemoryStream ms = new MemoryStream();
                if (buffer != null) ms.Write(buffer, 0, buffer.Length);
                ms.Position = 0;

                return File(ms, "application/vnd.ms-excel", "PPAAS,UC-VIS,AES-" + data.Id + ".xlsx");
            }
            return BadRequest();
        }

        public async Task<IActionResult> SummaryTestResultReport([FromQuery]long invoice_id)
        {
            var data = await _sYsRequirementInvoiceService.GetByIdForSummaryAsync(invoice_id);

            var stream = _testResultReportService.SummaryOfTestResult(data);
            var buffer = (stream as MemoryStream)?.ToArray();

            MemoryStream ms = new MemoryStream();
            if (buffer != null) ms.Write(buffer, 0, buffer.Length);
            ms.Position = 0;

            return File(ms, "application/vnd.ms-excel", "KQTN-TONGHOP-" + data.Id + ".xlsx");
        }

        public async Task<IActionResult> ViewReport(long propertyId)
        {
            if (!ModelState.IsValid) return BadRequest();
            var data = await _testPlanService.GetPropertyForReportAsync(propertyId);
            return Ok(JsonConvert.SerializeObject(data, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));
        }
    }
}