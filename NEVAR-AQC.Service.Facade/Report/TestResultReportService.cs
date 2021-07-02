using NEVAR_AQC.Core.Models.ReceptionDepartment;
using NEVAR_AQC.Service.Report;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.IO;
using System.Linq;

namespace NEVAR_AQC.Service.Facade.Report
{
    public class TestResultReportService : ITestResultReportService
    {
        public Stream SummaryOfTestResult(SYSRequirementInvoiceModel data)
        {
            using (var file = new FileStream(@"wwwroot\resources\KQTN-TONGHOP.xlsx", FileMode.Open, FileAccess.Read))
            {
                using (var newFile = new MemoryStream())
                {
                    using (var excelPackage = new ExcelPackage(file))
                    {
                        var currentRow = 11;
                        var rowIndex = 1;
                        var worksheet = excelPackage.Workbook.Worksheets.First();
                        foreach (var specimen in data.IDTestRequirementEntities)
                        {
                            var propertiesList = specimen.IDTRTestPropertyEntities.OrderBy(w => w.OrderNumber);
                            foreach (var property in propertiesList)
                            {
                                worksheet.Cells["A" + currentRow + ":A" + currentRow].Value = rowIndex;

                                worksheet.Cells["B" + currentRow + ":C" + currentRow].Merge = true;
                                worksheet.Cells["B" + currentRow + ":C" + currentRow].Value = property.IDTestRequirementEntity.CTGTestObjectEntity.Name;

                                worksheet.Cells["D" + currentRow + ":D" + currentRow].Value = property.IDTestRequirementEntity.SpecimenCode;

                                var propertyNameData = Core.StringHelper.ChemicalSymbolsHelper.FormatToElementArray(property.CTGTestPropertyEntity.Name);
                                ExcelRichText supScript;
                                foreach (var (key, value) in propertyNameData)
                                {
                                    switch (key)
                                    {
                                        case "content":
                                            worksheet.Cells["E" + currentRow + ":E" + currentRow].RichText.Add(value);
                                            break;

                                        case "sub":
                                            var subScript = worksheet.Cells["E" + currentRow + ":E" + currentRow].RichText.Add(value);
                                            subScript.VerticalAlign = ExcelVerticalAlignmentFont.Subscript;
                                            break;

                                        case "sup":
                                            supScript = worksheet.Cells["E" + currentRow + ":E" + currentRow].RichText.Add(value);
                                            supScript.VerticalAlign = ExcelVerticalAlignmentFont.Superscript;
                                            break;
                                    }
                                }

                                if (property.TestMethodId != null)
                                {
                                    worksheet.Cells["F" + currentRow + ":F" + currentRow].RichText.Add(property.CTGTestMethodEntity.Name);
                                    supScript = worksheet.Cells["F" + currentRow + ":F" + currentRow].RichText.Add(property.CTGTestMethodEntity.SymbolAttached);
                                    supScript.VerticalAlign = ExcelVerticalAlignmentFont.Superscript;
                                }

                                worksheet.Cells["G" + currentRow + ":G" + currentRow].Value = string.Join(", ", property.IDTRImplementerEntities.Select(w => w.SYSUserEntity.DisplayName));

                                worksheet.Cells["H" + currentRow + ":H" + currentRow].Value = Convert.ToDateTime(property.PlanFromTime).ToString("dd/MM/yyyy") + " - " + Convert.ToDateTime(property.PlanToTime).ToString("dd/MM/yyyy");

                                if (property.IDTRTestProcessAASUCVISAESMethodEntities.Any())
                                {
                                    worksheet.Cells["I" + currentRow + ":I" + currentRow].Value = property.IDTRTestProcessAASUCVISAESMethodEntities.First().ReportResults;
                                }
                                if (property.IDTRTestProcessWeightMethodEntities.Any())
                                {
                                    worksheet.Cells["I" + currentRow + ":I" + currentRow].Value = property.IDTRTestProcessWeightMethodEntities.First().ReportResults;
                                }
                                if (property.IDTRTestProcessVolumeMethodEntities.Any())
                                {
                                    worksheet.Cells["I" + currentRow + ":I" + currentRow].Value = property.IDTRTestProcessVolumeMethodEntities.First().ReportResults;
                                }
                                if (property.IDTRTestProcessOtherMethodEntities.Any())
                                {
                                    worksheet.Cells["I" + currentRow + ":I" + currentRow].Value = property.IDTRTestProcessOtherMethodEntities.First().ReportResults;
                                }

                                worksheet.Cells["A" + currentRow + ":I" + currentRow].Style.WrapText = true;
                                worksheet.Cells["A" + currentRow + ":I" + currentRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                worksheet.Cells["A" + currentRow + ":I" + currentRow].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                worksheet.Cells["E" + currentRow + ":E" + currentRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                worksheet.Cells["A" + currentRow + ":I" + currentRow].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells["A" + currentRow + ":I" + currentRow].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells["A" + currentRow + ":I" + currentRow].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells["A" + currentRow + ":I" + currentRow].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                currentRow++;
                                rowIndex++;
                            }
                        }

                        excelPackage.SaveAs(newFile);

                        return excelPackage.Stream;
                    }
                }
            }
        }

        public Stream TestResultBySpeciment(IDTestRequirementModel data)
        {
            using (var file = new FileStream(@"wwwroot\resources\KQTN-PHOI.xlsx", FileMode.Open, FileAccess.Read))
            {
                using (var newFile = new MemoryStream())
                {
                    using (var excelPackage = new ExcelPackage(file))
                    {
                        var worksheet = excelPackage.Workbook.Worksheets.First();

                        worksheet.HeaderFooter.OddHeader.LeftAlignedText = "\r\n\r\n\r\n\r\n\r\n\r\n\r\nSố: " + data.InvoiceResultNo;
                        worksheet.HeaderFooter.OddHeader.RightAlignedText = "\r\n\r\n\r\n\r\n\r\n\r\n\r\n&I Trang: &P/&N";

                        worksheet.Cells["E5:H5"].Value = data.SpecimenName;
                        worksheet.Cells["E6:H6"].Value = data.SpecimenSymbol ?? "---";
                        worksheet.Cells["E7:H7"].Value = data.SYSRequirementInvoiceEntity.SYSCustomerEntity.Name;
                        worksheet.Cells["E8:H8"].Value = data.SYSRequirementInvoiceEntity.SYSCustomerEntity.Address ?? "---";
                        worksheet.Cells["E9:H9"].Value = data.SpecimenStatus ?? "---";
                        worksheet.Cells["E10:H10"].Value = Convert.ToDateTime(data.SYSRequirementInvoiceEntity.CreatedTime).ToString("dd/MM/yyyy");
                        worksheet.Cells["E11:H11"].Value = Convert.ToDateTime(data.IDTRTestPropertyEntities.ToList()[0].PlanFromTime).ToString("dd/MM/yyyy")
                                                            + " đến ngày "
                                                            + Convert.ToDateTime(data.IDTRTestPropertyEntities.ToList()[0].PlanToTime).ToString("dd/MM/yyyy");

                        worksheet.Cells["E5:H11"].Style.WrapText = true;

                        var currentRow = 14;
                        var rowIndex = 0;
                        foreach (var property in data.IDTRTestPropertyEntities)
                        {
                            currentRow++;

                            worksheet.Cells["B" + currentRow + ":B" + currentRow].Value = ++rowIndex;

                            var propertyNameData = Core.StringHelper.ChemicalSymbolsHelper.FormatToElementArray(property.CTGTestPropertyEntity.Name);
                            ExcelRichText supScript;
                            foreach (var (key, value) in propertyNameData)
                            {
                                switch (key)
                                {
                                    case "content":
                                        worksheet.Cells["C" + currentRow + ":C" + currentRow].RichText.Add(value);
                                        break;

                                    case "sub":
                                        var subScript = worksheet.Cells["C" + currentRow + ":C" + currentRow].RichText.Add(value);
                                        subScript.VerticalAlign = ExcelVerticalAlignmentFont.Subscript;
                                        break;

                                    case "sup":
                                        supScript = worksheet.Cells["C" + currentRow + ":C" + currentRow].RichText.Add(value);
                                        supScript.VerticalAlign = ExcelVerticalAlignmentFont.Superscript;
                                        break;
                                }
                            }

                            worksheet.Cells["D" + currentRow + ":F" + currentRow].Value = property.CTGTestPropertyEntity.Unit;
                            worksheet.Cells["D" + currentRow + ":F" + currentRow].Merge = true;

                            if (property.TestMethodId != null)
                            {
                                worksheet.Cells["G" + currentRow + ":G" + currentRow].RichText.Add(property.CTGTestMethodEntity.Name);
                                supScript = worksheet.Cells["G" + currentRow + ":G" + currentRow].RichText.Add(property.CTGTestMethodEntity.SymbolAttached);
                                supScript.VerticalAlign = ExcelVerticalAlignmentFont.Superscript;
                            }

                            if (property.IDTRTestProcessAASUCVISAESMethodEntities.Any())
                            {
                                worksheet.Cells["H" + currentRow + ":H" + currentRow].Value = property.IDTRTestProcessAASUCVISAESMethodEntities.First().ReportResults;
                            }
                            if (property.IDTRTestProcessWeightMethodEntities.Any())
                            {
                                worksheet.Cells["H" + currentRow + ":H" + currentRow].Value = property.IDTRTestProcessWeightMethodEntities.First().ReportResults;
                            }
                            if (property.IDTRTestProcessVolumeMethodEntities.Any())
                            {
                                worksheet.Cells["H" + currentRow + ":H" + currentRow].Value = property.IDTRTestProcessVolumeMethodEntities.First().ReportResults;
                            }
                            if (property.IDTRTestProcessOtherMethodEntities.Any())
                            {
                                worksheet.Cells["H" + currentRow + ":H" + currentRow].Value = property.IDTRTestProcessOtherMethodEntities.First().ReportResults;
                            }
                        }

                        worksheet.Cells["B15:H" + currentRow].Style.WrapText = true;
                        worksheet.Cells["B15:H" + currentRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["B15:H" + currentRow].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        worksheet.Cells["B15:H" + currentRow].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["B15:H" + currentRow].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["B15:H" + currentRow].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["B15:H" + currentRow].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                        worksheet.Cells["C15:C" + currentRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                        currentRow += 1;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Merge = true;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Value = "Ghi chú:";
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Style.Font.Bold = true;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Style.Font.UnderLine = true;

                        currentRow += 1;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Merge = true;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Style.WrapText = true;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Style.Font.Size = 10;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Value = @"- KPH: Không phát hiện/Not detectable";

                        currentRow += 1;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Merge = true;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Style.WrapText = true;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Style.Font.Size = 10;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Value = @"- LOD: Giới phát hiện của phương pháp/ Method detection limit";

                        currentRow += 1;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Merge = true;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Style.WrapText = true;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Style.Font.Size = 10;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Value = @"- (A): phép thử được BoA công nhận/ Test method is accreditated by BoA";

                        currentRow += 1;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Merge = true;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Style.WrapText = true;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Style.Font.Size = 10;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Value = @"- (S): phép thử sử dụng nhà thầu phụ/  Test method is used subcontractors";

                        currentRow += 1;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Merge = true;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Style.WrapText = true;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Style.Font.Size = 10;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Value = @"- Tên mẫu, ký hiệu mẫu và tên khách hàng được ghi theo yêu cầu của khách hàng/ The sample, sign, cilent are writen by Client’s requirement";

                        currentRow += 1;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Merge = true;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Style.WrapText = true;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Style.Font.Size = 10;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Value = @"- Kết quả thử nghiệm chỉ có giá trị trên mẫu thử/ The test results are valid only for the received sample";

                        currentRow += 1;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Merge = true;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Style.WrapText = true;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Style.Font.Size = 10;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Value = @"- Phiếu kết quả này không được trích sao nếu chưa có sự đồng ý của Trung tâm kỹ thuật TCĐLCL/ This test report not be reproduced except in pull without the written approval of Technical Center of Standards Metrology and Quality";

                        currentRow += 2;

                        worksheet.Cells["G" + currentRow + ":H" + currentRow].Value = "Nghệ An, ngày " + data.InvoiceResultDate.Value.Day + " tháng " + data.InvoiceResultDate.Value.Month + " năm " + data.InvoiceResultDate.Value.Year;
                        worksheet.Cells["G" + currentRow + ":H" + currentRow].Style.Font.Bold = false;
                        worksheet.Cells["G" + currentRow + ":H" + currentRow].Style.Font.Italic = true;
                        worksheet.Cells["G" + currentRow + ":H" + currentRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["G" + currentRow + ":H" + currentRow].Merge = true;

                        currentRow++;

                        worksheet.Cells["B" + currentRow + ":F" + currentRow].Value = "PT. BỘ PHẬN THỬ NGHIỆM";
                        worksheet.Cells["B" + currentRow + ":F" + currentRow].Style.Font.Bold = true;
                        worksheet.Cells["B" + currentRow + ":F" + currentRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["B" + currentRow + ":F" + currentRow].Merge = true;

                        worksheet.Cells["G" + currentRow + ":H" + currentRow].Value = "GIÁM ĐỐC";
                        worksheet.Cells["G" + currentRow + ":H" + currentRow].Style.Font.Bold = true;
                        worksheet.Cells["G" + currentRow + ":H" + currentRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["G" + currentRow + ":H" + currentRow].Merge = true;

                        currentRow += 5;

                        worksheet.Cells["B" + currentRow + ":F" + currentRow].Value = "";
                        worksheet.Cells["B" + currentRow + ":F" + currentRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["B" + currentRow + ":F" + currentRow].Merge = true;

                        worksheet.Cells["G" + currentRow + ":H" + currentRow].Value = "";
                        worksheet.Cells["B" + currentRow + ":F" + currentRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["G" + currentRow + ":H" + currentRow].Merge = true;

                        excelPackage.SaveAs(newFile);

                        return excelPackage.Stream;
                    }
                }
            }
        }
    }
}