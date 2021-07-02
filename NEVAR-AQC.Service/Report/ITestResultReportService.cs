using NEVAR_AQC.Core.Models.ReceptionDepartment;
using System.IO;

namespace NEVAR_AQC.Service.Report
{
    public interface ITestResultReportService
    {
        Stream SummaryOfTestResult(SYSRequirementInvoiceModel data);

        Stream TestResultBySpeciment(IDTestRequirementModel data);
    }
}