using NEVAR_AQC.Core.Models.TestDepartment;
using NEVAR_AQC.Service.Report;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.IO;
using System.Linq;

namespace NEVAR_AQC.Service.Facade.Report
{
    public class TestProcessReportService : ITestProcessReportService
    {
        public Stream AASMethodReport(IDTRTestPropertyModel data)
        {
            using (var file = new FileStream(@"wwwroot\resources\PPAAS,UV-VIS,AES.xlsx", FileMode.Open, FileAccess.Read))
            {
                using (var newFile = new MemoryStream())
                {
                    using (var excelPackage = new ExcelPackage(file))
                    {
                        var worksheet = excelPackage.Workbook.Worksheets.First();
                        ExcelRichText supScript;

                        worksheet.Cells["A9:H9"].Value = "Đối tượng mẫu: " + data.IDTestRequirementEntity.CTGTestObjectEntity.Name;
                        worksheet.Cells["I9:P9"].Value = "Mã số mẫu: " + data.IDTestRequirementEntity.SpecimenCode;

                        worksheet.Cells["A10:F10"].Value = "Tên phép thử: ";

                        var propertyNameData = Core.StringHelper.ChemicalSymbolsHelper.FormatToElementArray(data.CTGTestPropertyEntity.Name);
                        foreach (var item in propertyNameData)
                        {
                            if (item.Key == "content")
                            {
                                worksheet.Cells["A10:F10"].RichText.Add(item.Value);
                            }
                            if (item.Key == "sub")
                            {
                                var subScript = worksheet.Cells["A10:F10"].RichText.Add(item.Value);
                                subScript.VerticalAlign = ExcelVerticalAlignmentFont.Subscript;
                            }
                            if (item.Key == "sup")
                            {
                                supScript = worksheet.Cells["A10:F10"].RichText.Add(item.Value);
                                supScript.VerticalAlign = ExcelVerticalAlignmentFont.Superscript;
                            }
                        }

                        if (data.TestMethodId != null)
                        {
                            worksheet.Cells["G10:P10"].RichText.Add(data.CTGTestMethodEntity.Name);
                            supScript = worksheet.Cells["G10:P10"].RichText.Add(data.CTGTestMethodEntity.SymbolAttached);
                            supScript.VerticalAlign = ExcelVerticalAlignmentFont.Superscript;
                        }
                        var method = data.IDTRTestProcessAASUCVISAESMethodEntities.ToList()[0];

                        var implementerAcceptInfo = data.IDTRImplementerEntities?.Where(w => w.IsAccept).FirstOrDefault();
                        worksheet.Cells["B11:P11"].Value = "Từ ngày: " + Convert.ToDateTime(implementerAcceptInfo?.TimeToStart).ToString("dd/MM/yyyy") + " đến ngày: " + Convert.ToDateTime(method.TimeReportResults).ToString("dd/MM/yyyy");

                        worksheet.Cells["C12:O12"].Value = @"- Khối lượng/Thể tích ban đầu: Lần 1 (L1): " + (method.QuantumL1 ?? "...............") + " g hoặc mL; Lần 2 (L2): " + (method.QuantumL2 ?? "...............") + " g hoặc mL";

                        if (method.SymbolC != null)
                        {
                            worksheet.Cells["C14:C14"].Value = "C(" + method.SymbolC + ")";
                        }
                        if (method.ValueC1 != null)
                        {
                            worksheet.Cells["D14:D14"].Value = method.ValueC1;
                        }
                        if (method.ValueC2 != null)
                        {
                            worksheet.Cells["E14:F14"].Value = method.ValueC2;
                        }
                        if (method.ValueC3 != null)
                        {
                            worksheet.Cells["G14:G14"].Value = method.ValueC3;
                        }
                        if (method.ValueC4 != null)
                        {
                            worksheet.Cells["H14:J14"].Value = method.ValueC4;
                        }
                        if (method.ValueC5 != null)
                        {
                            worksheet.Cells["K14:L14"].Value = method.ValueC5;
                        }
                        if (method.ValueC6 != null)
                        {
                            worksheet.Cells["M14:N14"].Value = method.ValueC6;
                        }
                        if (method.ValueC7 != null)
                        {
                            worksheet.Cells["O14:O14"].Value = method.ValueC7;
                        }

                        if (method.AbsorbanceC1 != null)
                        {
                            worksheet.Cells["D15:D15"].Value = method.AbsorbanceC1;
                        }
                        if (method.AbsorbanceC2 != null)
                        {
                            worksheet.Cells["E15:F15"].Value = method.AbsorbanceC2;
                        }
                        if (method.AbsorbanceC3 != null)
                        {
                            worksheet.Cells["G15:G15"].Value = method.AbsorbanceC3;
                        }
                        if (method.AbsorbanceC4 != null)
                        {
                            worksheet.Cells["H15:J15"].Value = method.AbsorbanceC4;
                        }
                        if (method.AbsorbanceC5 != null)
                        {
                            worksheet.Cells["K15:L15"].Value = method.AbsorbanceC5;
                        }
                        if (method.AbsorbanceC6 != null)
                        {
                            worksheet.Cells["M15:N15"].Value = method.AbsorbanceC6;
                        }
                        if (method.AbsorbanceC7 != null)
                        {
                            worksheet.Cells["O15:O15"].Value = method.AbsorbanceC7;
                        }

                        if (method.StandardLineEquation != null)
                        {
                            worksheet.Cells["C16:C16"].Value = @"phương trình đường chuẩn: " + method.StandardLineEquation + " Hệ số R2 = " + (method.CoefficientR2 ?? ".........................");
                        }

                        if (method.T1 != null)
                        {
                            worksheet.Cells["C17:O17"].Value = @"- Nồng độ thêm chuẩn (nếu có):"
                                                                + "............... Thêm chuẩn 1 (T1): "
                                                                + method.T1
                                                                + "; Thêm chuẩn 2 (T2): "
                                                                + (method.T2 ?? "....................");
                        }

                        if (method.MeasurementResultsL1 != null)
                        {
                            worksheet.Cells["F20:H20"].Value = method.MeasurementResultsL1;
                        }
                        if (method.MeasurementResultsL2 != null)
                        {
                            worksheet.Cells["I20:K20"].Value = method.MeasurementResultsL2;
                        }
                        if (method.MeasurementResultsT1 != null)
                        {
                            worksheet.Cells["L20:M20"].Value = method.MeasurementResultsT1;
                        }
                        if (method.MeasurementResultsT2 != null)
                        {
                            worksheet.Cells["N20:O20"].Value = method.MeasurementResultsT2;
                        }

                        if (method.DilutionCoefficientL1 != null)
                        {
                            worksheet.Cells["F23:H23"].Value = method.DilutionCoefficientL1;
                        }
                        if (method.DilutionCoefficientL2 != null)
                        {
                            worksheet.Cells["I23:K23"].Value = method.DilutionCoefficientL2;
                        }
                        if (method.DilutionCoefficientT1 != null)
                        {
                            worksheet.Cells["L23:M23"].Value = method.DilutionCoefficientT1;
                        }
                        if (method.DilutionCoefficientT2 != null)
                        {
                            worksheet.Cells["N23:O23"].Value = method.DilutionCoefficientT2;
                        }

                        if (method.CalculationFormula != null)
                        {
                            worksheet.Cells["C25:O27"].Value = method.CalculationFormula;
                        }

                        if (method.PercentOfRevoke != null)
                        {
                            worksheet.Cells["C29:O29"].Value = "R% = " + method.PercentOfRevoke;
                        }

                        if (method.ResultL1 != null)
                        {
                            worksheet.Cells["F32:H32"].Value = method.ResultL1;
                        }
                        if (method.ResultL2 != null)
                        {
                            worksheet.Cells["I32:K32"].Value = method.ResultL2;
                        }
                        if (method.ResultT1 != null)
                        {
                            worksheet.Cells["L32:M32"].Value = method.ResultT1;
                        }
                        if (method.ResultT2 != null)
                        {
                            worksheet.Cells["N32:O32"].Value = method.ResultT2;
                        }

                        if (method.AverageResultsL != null)
                        {
                            worksheet.Cells["F33:K33"].Value = method.AverageResultsL;
                        }
                        if (method.AverageResultsT != null)
                        {
                            worksheet.Cells["L33:O33"].Value = method.AverageResultsT;
                        }

                        if (method.ReportResults != null)
                        {
                            worksheet.Cells["B35:P35"].Value = method.ReportResults;
                        }

                        if (method.TimeReportResults != null)
                        {
                            worksheet.Cells["B36:P36"].Value = method.TimeReportResults.ToString("dd/MM/yyyy");
                        }

                        worksheet.Cells["A37:H38"].Value = "Người thực hiện phép thử: " + data.IDTRImplementerEntities.FirstOrDefault(w => w.IsAccept)?.SYSUserEntity.DisplayName;

                        excelPackage.SaveAs(newFile);

                        return excelPackage.Stream;
                    }
                }
            }
        }

        public Stream OtherMethodReport(IDTRTestPropertyModel data)
        {
            using (var file = new FileStream(@"wwwroot\resources\PPK.xlsx", FileMode.Open, FileAccess.Read))
            {
                using (var newFile = new MemoryStream())
                {
                    using (var excelPackage = new ExcelPackage(file))
                    {
                        var worksheet = excelPackage.Workbook.Worksheets.First();
                        ExcelRichText supScript;

                        worksheet.Cells["A9:G9"].Value = "Đối tượng mẫu: " + data.IDTestRequirementEntity.CTGTestObjectEntity.Name;
                        worksheet.Cells["H9:L9"].Value = "Mã số mẫu: " + data.IDTestRequirementEntity.SpecimenCode;

                        worksheet.Cells["A10:E10"].Value = "Tên phép thử: ";

                        var propertyNameData = Core.StringHelper.ChemicalSymbolsHelper.FormatToElementArray(data.CTGTestPropertyEntity.Name);
                        foreach (var (key, value) in propertyNameData)
                        {
                            switch (key)
                            {
                                case "content":
                                    worksheet.Cells["A10:E10"].RichText.Add(value);
                                    break;

                                case "sub":
                                    var subScript = worksheet.Cells["A10:E10"].RichText.Add(value);
                                    subScript.VerticalAlign = ExcelVerticalAlignmentFont.Subscript;
                                    break;

                                case "sup":
                                    supScript = worksheet.Cells["A10:E10"].RichText.Add(value);
                                    supScript.VerticalAlign = ExcelVerticalAlignmentFont.Superscript;
                                    break;
                            }
                        }

                        if (data.TestMethodId != null)
                        {
                            worksheet.Cells["F10:L10"].RichText.Add(data.CTGTestMethodEntity.Name);
                            supScript = worksheet.Cells["F10:L10"].RichText.Add(data.CTGTestMethodEntity.SymbolAttached);
                            supScript.VerticalAlign = ExcelVerticalAlignmentFont.Superscript;
                        }
                        var otherMethod = data.IDTRTestProcessOtherMethodEntities.ToList()[0];

                        var implementerAcceptInfo = data.IDTRImplementerEntities?.Where(w => w.IsAccept).FirstOrDefault();
                        if (implementerAcceptInfo != null)
                            worksheet.Cells["B11:L11"].Value =
                                "Từ ngày: " +
                                Convert.ToDateTime(implementerAcceptInfo.TimeToStart).ToString("dd/MM/yyyy") +
                                " đến ngày: " +
                                Convert.ToDateTime(otherMethod.TimeReportResults).ToString("dd/MM/yyyy");

                        if (otherMethod.MonitoringData != null)
                        {
                            worksheet.Cells["C12:K35"].Value = otherMethod.MonitoringData;
                        }

                        if (otherMethod.ReportResults != null)
                        {
                            worksheet.Cells["B36:L36"].Value = otherMethod.ReportResults;
                        }

                        if (otherMethod.TimeReportResults != null)
                        {
                            worksheet.Cells["B37:L37"].Value = otherMethod.TimeReportResults.ToString("dd/MM/yyyy");
                        }

                        worksheet.Cells["A38:G39"].Value = "Người thực hiện phép thử: " + (data.IDTRImplementerEntities ?? throw new InvalidOperationException()).FirstOrDefault(w => w.IsAccept)?.SYSUserEntity.DisplayName;

                        excelPackage.SaveAs(newFile);

                        return excelPackage.Stream;
                    }
                }
            }
        }

        public Stream VolumeMethodReport(IDTRTestPropertyModel data)
        {
            using (var file = new FileStream(@"wwwroot\resources\PPTT.xlsx", FileMode.Open, FileAccess.Read))
            {
                using (var newFile = new MemoryStream())
                {
                    using (var excelPackage = new ExcelPackage(file))
                    {
                        var worksheet = excelPackage.Workbook.Worksheets.First();
                        ExcelRichText supScript;

                        worksheet.Cells["A9:G9"].Value = "Đối tượng mẫu: " + data.IDTestRequirementEntity.CTGTestObjectEntity.Name;
                        worksheet.Cells["H9:L9"].Value = "Mã số mẫu: " + data.IDTestRequirementEntity.SpecimenCode;

                        worksheet.Cells["A10:E10"].Value = "Tên phép thử: ";

                        var propertyNameData = Core.StringHelper.ChemicalSymbolsHelper.FormatToElementArray(data.CTGTestPropertyEntity.Name);
                        foreach (var (key, value) in propertyNameData)
                        {
                            switch (key)
                            {
                                case "content":
                                    worksheet.Cells["A10:E10"].RichText.Add(value);
                                    break;

                                case "sub":
                                    var subScript = worksheet.Cells["A10:E10"].RichText.Add(value);
                                    subScript.VerticalAlign = ExcelVerticalAlignmentFont.Subscript;
                                    break;

                                case "sup":
                                    supScript = worksheet.Cells["A10:E10"].RichText.Add(value);
                                    supScript.VerticalAlign = ExcelVerticalAlignmentFont.Superscript;
                                    break;
                            }
                        }

                        if (data.TestMethodId != null)
                        {
                            worksheet.Cells["F10:L10"].RichText.Add(data.CTGTestMethodEntity.Name);
                            supScript = worksheet.Cells["F10:L10"].RichText.Add(data.CTGTestMethodEntity.SymbolAttached);
                            supScript.VerticalAlign = ExcelVerticalAlignmentFont.Superscript;
                        }
                        var volumeMethod = data.IDTRTestProcessVolumeMethodEntities.ToList()[0];

                        var implementerAcceptInfo = data.IDTRImplementerEntities?.Where(w => w.IsAccept).FirstOrDefault();
                        worksheet.Cells["B11:L11"].Value = "Từ ngày: " + Convert.ToDateTime(implementerAcceptInfo.TimeToStart).ToString("dd/MM/yyyy") + " đến ngày: " + Convert.ToDateTime(volumeMethod.TimeReportResults).ToString("dd/MM/yyyy");

                        worksheet.Cells["C12:K12"].Value = @"- Khối lượng/Thể tích ban đầu: Lần 1 (L1): " + (volumeMethod.QuantumL1 ?? "...............") + " g hoặc mL; Lần 2 (L2): " + (volumeMethod.QuantumL2 ?? "...............") + " g hoặc mL";

                        if (volumeMethod.SolutionName1 != null)
                        {
                            worksheet.Cells["C13:K13"].Value = @"- Nồng độ dung dịch chuẩn: " + volumeMethod.SolutionName1 + " - " + (volumeMethod.ConcentrationOfSolution1 ?? "....................");
                        }

                        if (volumeMethod.SolutionName2 != null)
                        {
                            worksheet.Cells["C14:K14"].Value = @"- Nồng độ dung dịch chuẩn: " + volumeMethod.SolutionName2 + " - " + (volumeMethod.ConcentrationOfSolution2 ?? "....................");
                        }

                        if (volumeMethod.OtherMonitoringData != null)
                        {
                            worksheet.Cells["C16:K20"].Value = volumeMethod.OtherMonitoringData;
                        }

                        if (volumeMethod.T1 != null)
                        {
                            worksheet.Cells["C21:K23"].Value = @"- Nồng độ thêm chuẩn (nếu có):"
                                                                + "............... Thêm chuẩn 1 (T1): "
                                                                + volumeMethod.T1
                                                                + "; Thêm chuẩn 2 (T2): "
                                                                + (volumeMethod.T2 ?? "....................");
                        }

                        if (volumeMethod.CalculationFormula != null)
                        {
                            worksheet.Cells["C25:K28"].Value = volumeMethod.CalculationFormula;
                        }

                        if (volumeMethod.ResultL1 != null)
                        {
                            worksheet.Cells["E31:F31"].Value = volumeMethod.ResultL1;
                        }
                        if (volumeMethod.ResultL2 != null)
                        {
                            worksheet.Cells["G31:H31"].Value = volumeMethod.ResultL2;
                        }
                        if (volumeMethod.ResultT1 != null)
                        {
                            worksheet.Cells["I31:J31"].Value = volumeMethod.ResultT1;
                        }
                        if (volumeMethod.ResultT2 != null)
                        {
                            worksheet.Cells["K31:K31"].Value = volumeMethod.ResultT2;
                        }

                        if (volumeMethod.AverageResultsL != null)
                        {
                            worksheet.Cells["E32:H32"].Value = volumeMethod.AverageResultsL;
                        }
                        if (volumeMethod.AverageResultsT != null)
                        {
                            worksheet.Cells["I32:K32"].Value = volumeMethod.AverageResultsT;
                        }

                        if (volumeMethod.PercentOfRevoke != null)
                        {
                            worksheet.Cells["C34:K35"].Value = "R% = " + volumeMethod.PercentOfRevoke;
                        }

                        if (volumeMethod.ReportResults != null)
                        {
                            worksheet.Cells["B36:L36"].Value = volumeMethod.ReportResults;
                        }

                        if (volumeMethod.TimeReportResults != null)
                        {
                            worksheet.Cells["B37:L37"].Value = volumeMethod.TimeReportResults.ToString("dd/MM/yyyy");
                        }

                        worksheet.Cells["A38:G39"].Value = "Người thực hiện phép thử: " + data.IDTRImplementerEntities.FirstOrDefault(w => w.IsAccept).SYSUserEntity.DisplayName;

                        excelPackage.SaveAs(newFile);

                        return excelPackage.Stream;
                    }
                }
            }
        }

        public Stream WeightMethodReport(IDTRTestPropertyModel data)
        {
            using (var file = new FileStream(@"wwwroot\resources\PPTL.xlsx", FileMode.Open, FileAccess.Read))
            {
                using (var newFile = new MemoryStream())
                {
                    using (var excelPackage = new ExcelPackage(file))
                    {
                        var worksheet = excelPackage.Workbook.Worksheets.First();
                        ExcelRichText subScript;
                        ExcelRichText supScript;

                        worksheet.Cells["A9:G9"].Value = "Đối tượng mẫu: " + data.IDTestRequirementEntity.CTGTestObjectEntity.Name;
                        worksheet.Cells["H9:L9"].Value = "Mã số mẫu: " + data.IDTestRequirementEntity.SpecimenCode;

                        worksheet.Cells["A10:E10"].Value = "Tên phép thử: ";

                        var propertyNameData = Core.StringHelper.ChemicalSymbolsHelper.FormatToElementArray(data.CTGTestPropertyEntity.Name);
                        foreach (var (key, value) in propertyNameData)
                        {
                            switch (key)
                            {
                                case "content":
                                    worksheet.Cells["A10:E10"].RichText.Add(value);
                                    break;
                                case "sub":
                                    subScript = worksheet.Cells["A10:E10"].RichText.Add(value);
                                    subScript.VerticalAlign = ExcelVerticalAlignmentFont.Subscript;
                                    break;
                                case "sup":
                                    supScript = worksheet.Cells["A10:E10"].RichText.Add(value);
                                    supScript.VerticalAlign = ExcelVerticalAlignmentFont.Superscript;
                                    break;
                            }
                        }

                        if (data.TestMethodId != null)
                        {
                            worksheet.Cells["F10:L10"].RichText.Add(data.CTGTestMethodEntity.Name);
                            supScript = worksheet.Cells["F10:L10"].RichText.Add(data.CTGTestMethodEntity.SymbolAttached);
                            supScript.VerticalAlign = ExcelVerticalAlignmentFont.Superscript;
                        }

                        var weightMethod = data.IDTRTestProcessWeightMethodEntities.ToList()[0];

                        var implementerAcceptInfo = data.IDTRImplementerEntities?.Where(w => w.IsAccept).FirstOrDefault();
                        worksheet.Cells["B11:L11"].Value = "Từ ngày: " + Convert.ToDateTime(implementerAcceptInfo.TimeToStart).ToString("dd/MM/yyyy") + " đến ngày: " + Convert.ToDateTime(weightMethod.TimeReportResults).ToString("dd/MM/yyyy");

                        worksheet.Cells["C12:K12"].Value = @"- Khối lượng/Thể tích ban đầu: Lần 1: " + (weightMethod.QuantumL1 ?? "...............") + " g hoặc mL; Lần 2: " + (weightMethod.QuantumL2 ?? "...............") + " g hoặc mL";

                        if (weightMethod.WeightOfScaleSymbolL1 != null)
                        {
                            worksheet.Cells["C15:E15"].Value = "";
                            worksheet.Cells["C15:E15"].RichText.Add("L");
                            subScript = worksheet.Cells["C15:E15"].RichText.Add("1");
                            subScript.VerticalAlign = ExcelVerticalAlignmentFont.Subscript;
                            worksheet.Cells["C15:E15"].RichText.Add("(" + weightMethod.WeightOfScaleSymbolL1 + ")");
                        }
                        if (weightMethod.WeightOfCupL1 != null)
                        {
                            worksheet.Cells["F15:H15"].Value = weightMethod.WeightOfCupL1;
                        }
                        if (weightMethod.WeightOfCupAndSpecimenL1 != null)
                        {
                            worksheet.Cells["I15:K15"].Value = weightMethod.WeightOfCupAndSpecimenL1;
                        }

                        if (weightMethod.WeightOfScaleSymbolL2 != null)
                        {
                            worksheet.Cells["C16:E16"].Value = "";
                            worksheet.Cells["C16:E16"].RichText.Add("L");
                            subScript = worksheet.Cells["C16:E16"].RichText.Add("2");
                            subScript.VerticalAlign = ExcelVerticalAlignmentFont.Subscript;
                            worksheet.Cells["C16:E16"].RichText.Add("(" + weightMethod.WeightOfScaleSymbolL2 + ")");
                        }
                        if (weightMethod.WeightOfCupL2 != null)
                        {
                            worksheet.Cells["F16:H16"].Value = weightMethod.WeightOfCupL2;
                        }
                        if (weightMethod.WeightOfCupAndSpecimenL2 != null)
                        {
                            worksheet.Cells["I16:K16"].Value = weightMethod.WeightOfCupAndSpecimenL2;
                        }

                        if (weightMethod.SymbolT1 != null)
                        {
                            worksheet.Cells["C19:E19"].Value = "";
                            worksheet.Cells["C19:E19"].RichText.Add("T");
                            subScript = worksheet.Cells["C19:E19"].RichText.Add("1");
                            subScript.VerticalAlign = ExcelVerticalAlignmentFont.Subscript;
                            worksheet.Cells["C19:E19"].RichText.Add("(" + weightMethod.SymbolT1 + ")");
                        }
                        if (weightMethod.WeightOfCupT1 != null)
                        {
                            worksheet.Cells["F19:H19"].Value = weightMethod.WeightOfCupT1;
                        }
                        if (weightMethod.WeightOfCupAndSpecimenT1 != null)
                        {
                            worksheet.Cells["I19:K19"].Value = weightMethod.WeightOfCupAndSpecimenT1;
                        }

                        if (weightMethod.SymbolT2 != null)
                        {
                            worksheet.Cells["C20:E20"].Value = "";
                            worksheet.Cells["C20:E20"].RichText.Add("T");
                            subScript = worksheet.Cells["C20:E20"].RichText.Add("2");
                            subScript.VerticalAlign = ExcelVerticalAlignmentFont.Subscript;
                            worksheet.Cells["C20:E20"].RichText.Add("(" + weightMethod.SymbolT2 + ")");
                        }
                        if (weightMethod.WeightOfCupT2 != null)
                        {
                            worksheet.Cells["F20:H20"].Value = weightMethod.WeightOfCupT2;
                        }
                        if (weightMethod.WeightOfCupAndSpecimenT2 != null)
                        {
                            worksheet.Cells["I20:K20"].Value = weightMethod.WeightOfCupAndSpecimenT2;
                        }

                        if (weightMethod.DilutionCoefficientSymbolL1 != null)
                        {
                            worksheet.Cells["E22:F22"].Value = "";
                            worksheet.Cells["E22:F22"].RichText.Add("L");
                            subScript = worksheet.Cells["E22:F22"].RichText.Add("1");
                            subScript.VerticalAlign = ExcelVerticalAlignmentFont.Subscript;
                            worksheet.Cells["E22:F22"].RichText.Add("(" + weightMethod.DilutionCoefficientSymbolL1 + ")");
                        }
                        if (weightMethod.DilutionCoefficientSymbolL2 != null)
                        {
                            worksheet.Cells["G22:H22"].Value = "";
                            worksheet.Cells["G22:H22"].RichText.Add("L");
                            subScript = worksheet.Cells["G22:H22"].RichText.Add("2");
                            subScript.VerticalAlign = ExcelVerticalAlignmentFont.Subscript;
                            worksheet.Cells["G22:H22"].RichText.Add("(" + weightMethod.DilutionCoefficientSymbolL2 + ")");
                        }
                        if (weightMethod.DilutionCoefficientSymbolT1 != null)
                        {
                            worksheet.Cells["I22:J22"].Value = "";
                            worksheet.Cells["I22:J22"].RichText.Add("T");
                            subScript = worksheet.Cells["I22:J22"].RichText.Add("1");
                            subScript.VerticalAlign = ExcelVerticalAlignmentFont.Subscript;
                            worksheet.Cells["I22:J22"].RichText.Add("(" + weightMethod.DilutionCoefficientSymbolT1 + ")");
                        }
                        if (weightMethod.DilutionCoefficientSymbolT2 != null)
                        {
                            worksheet.Cells["K22:K22"].Value = "";
                            worksheet.Cells["K22:K22"].RichText.Add("T");
                            subScript = worksheet.Cells["K22:K22"].RichText.Add("2");
                            subScript.VerticalAlign = ExcelVerticalAlignmentFont.Subscript;
                            worksheet.Cells["K22:K22"].RichText.Add("(" + weightMethod.DilutionCoefficientSymbolT2 + ")");
                        }

                        if (weightMethod.DilutionCoefficientL1 != null)
                        {
                            worksheet.Cells["E23:F23"].Value = weightMethod.DilutionCoefficientL1;
                        }
                        if (weightMethod.DilutionCoefficientL2 != null)
                        {
                            worksheet.Cells["G23:H23"].Value = weightMethod.DilutionCoefficientL2;
                        }
                        if (weightMethod.DilutionCoefficientT1 != null)
                        {
                            worksheet.Cells["I23:J23"].Value = weightMethod.DilutionCoefficientT1;
                        }
                        if (weightMethod.DilutionCoefficientT2 != null)
                        {
                            worksheet.Cells["K23:K23"].Value = weightMethod.DilutionCoefficientT2;
                        }

                        if (weightMethod.CalculationFormula != null)
                        {
                            worksheet.Cells["C25:K28"].Value = weightMethod.CalculationFormula;
                        }

                        if (weightMethod.ResultSymbolL1 != null)
                        {
                            worksheet.Cells["E30:F30"].Value = "";
                            worksheet.Cells["E30:F30"].RichText.Add("L");
                            subScript = worksheet.Cells["E30:F30"].RichText.Add("1");
                            subScript.VerticalAlign = ExcelVerticalAlignmentFont.Subscript;
                            worksheet.Cells["E30:F30"].RichText.Add("(" + weightMethod.ResultSymbolL1 + ")");
                        }
                        if (weightMethod.ResultSymbolL2 != null)
                        {
                            worksheet.Cells["G30:H30"].Value = "";
                            worksheet.Cells["G30:H30"].RichText.Add("L");
                            subScript = worksheet.Cells["G30:H30"].RichText.Add("2");
                            subScript.VerticalAlign = ExcelVerticalAlignmentFont.Subscript;
                            worksheet.Cells["G30:H30"].RichText.Add("(" + weightMethod.ResultSymbolL2 + ")");
                        }
                        if (weightMethod.ResultSymbolT1 != null)
                        {
                            worksheet.Cells["I30:J30"].Value = "";
                            worksheet.Cells["I30:J30"].RichText.Add("T");
                            subScript = worksheet.Cells["I30:J30"].RichText.Add("1");
                            subScript.VerticalAlign = ExcelVerticalAlignmentFont.Subscript;
                            worksheet.Cells["I30:J30"].RichText.Add("(" + weightMethod.ResultSymbolT1 + ")");
                        }
                        if (weightMethod.ResultSymbolT2 != null)
                        {
                            worksheet.Cells["K30:K30"].Value = "";
                            worksheet.Cells["K30:K30"].RichText.Add("T");
                            subScript = worksheet.Cells["K30:K30"].RichText.Add("2");
                            subScript.VerticalAlign = ExcelVerticalAlignmentFont.Subscript;
                            worksheet.Cells["K30:K30"].RichText.Add("(" + weightMethod.ResultSymbolT2 + ")");
                        }

                        if (weightMethod.ResultL1 != null)
                        {
                            worksheet.Cells["E31:F31"].Value = weightMethod.ResultL1;
                        }
                        if (weightMethod.ResultL2 != null)
                        {
                            worksheet.Cells["G31:H31"].Value = weightMethod.ResultL2;
                        }
                        if (weightMethod.ResultT1 != null)
                        {
                            worksheet.Cells["I31:J31"].Value = weightMethod.ResultT1;
                        }
                        if (weightMethod.ResultT2 != null)
                        {
                            worksheet.Cells["K31:K31"].Value = weightMethod.ResultT2;
                        }

                        if (weightMethod.AverageResultsL != null)
                        {
                            worksheet.Cells["E32:H32"].Value = weightMethod.AverageResultsL;
                        }
                        if (weightMethod.AverageResultsT != null)
                        {
                            worksheet.Cells["I32:K32"].Value = weightMethod.AverageResultsT;
                        }

                        if (weightMethod.PercentOfRevoke != null)
                        {
                            worksheet.Cells["C34:K35"].Value = "R% = " + weightMethod.PercentOfRevoke;
                        }

                        if (weightMethod.ReportResults != null)
                        {
                            worksheet.Cells["B36:L36"].Value = weightMethod.ReportResults;
                        }

                        if (weightMethod.TimeReportResults != null)
                        {
                            worksheet.Cells["B37:L37"].Value = weightMethod.TimeReportResults.ToString("dd/MM/yyyy");
                        }

                        worksheet.Cells["A38:G39"].Value = "Người thực hiện phép thử: " + data.IDTRImplementerEntities.FirstOrDefault(w => w.IsAccept).SYSUserEntity.DisplayName;

                        excelPackage.SaveAs(newFile);

                        return excelPackage.Stream;
                    }
                }
            }
        }
    }
}