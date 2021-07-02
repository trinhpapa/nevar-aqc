using NEVAR_AQC.Core.Models.ReceptionDepartment;
using System.IO;

namespace NEVAR_AQC.Service.Report
{
    public interface IImplementationPlanService
    {
        Stream TestPlanReport(SYSRequirementInvoiceModel data, int currentRow = 11);
    }
}