using NEVAR_AQC.Core.Models.ReceptionDepartment;
using System.IO;

namespace NEVAR_AQC.Service.Report
{
    public interface ITestRequirementReportService
    {
        Stream RequirementInvoiceReport(SYSRequirementInvoiceViewModel data);
    }
}