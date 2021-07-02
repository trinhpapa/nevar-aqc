using NEVAR_AQC.Core.Models.ReceptionDepartment;
using NEVAR_AQC.Core.Models.TestDepartment;
using System.IO;

namespace NEVAR_AQC.Service.Report
{
    public interface ITestProcessReportService
    {
        Stream WeightMethodReport(IDTRTestPropertyModel data);

        Stream VolumeMethodReport(IDTRTestPropertyModel data);

        Stream AASMethodReport(IDTRTestPropertyModel data);

        Stream OtherMethodReport(IDTRTestPropertyModel data);
    }
}