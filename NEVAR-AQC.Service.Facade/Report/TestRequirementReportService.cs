using NEVAR_AQC.Core.Models.ReceptionDepartment;
using NEVAR_AQC.Service.Report;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.IO;
using System.Linq;

namespace NEVAR_AQC.Service.Facade.Report
{
    public class TestRequirementReportService : ITestRequirementReportService
    {
        public Stream RequirementInvoiceReport(SYSRequirementInvoiceViewModel data)
        {
            using (var file = new FileStream(@"wwwroot\resources\YCTN-PHOI.xlsx", FileMode.Open, FileAccess.Read))
            {
                using (new MemoryStream())
                {
                    using (var excelPackage = new ExcelPackage(file))
                    {
                        var worksheet = excelPackage.Workbook.Worksheets.First();

                        if (data.Edition != 0)
                        {
                            worksheet.Cells["D4:H4"].Value = "PHIẾU YÊU CẦU THỬ NGHIỆM CHỈNH SỬA LẦN " + data.Edition;
                            worksheet.Cells["D5:H5"].Value = "TEST REQUIREMENT EDITION " + data.Edition;
                        }

                        worksheet.Cells["B7:E7"].Value = "Số: " + data.InvoiceNo;

                        if (data.CreatedTime != null)
                            worksheet.Cells["F7:H7"].Value =
                                "Nghệ An, ngày " + ((DateTime)data.CreatedTime).Day + " tháng " +
                                ((DateTime)data.CreatedTime).Month + " năm " + ((DateTime)data.CreatedTime).Year;

                        ExcelRichText title;
                        ExcelRichText content;
                        ExcelRichText supScript;
                        ExcelRichText subScript;

                        //title = worksheet.Cells["B10:H10"].RichText.Add("Tên khách hàng: ");
                        //title.Bold = true;
                        worksheet.Cells["D10:H10"].Value = data.SYSCustomerEntity.Name;
                        worksheet.Cells["D10:H10"].Style.Font.Bold = false;

                        //title = worksheet.Cells["B11:H11"].RichText.Add("Người đại diện: ");
                        //title.Bold = true;
                        worksheet.Cells["D11:H11"].Value = data.Representative ?? "---";
                        worksheet.Cells["D11:H11"].Style.Font.Bold = false;
                        if (data.Representative == null)
                        {
                            worksheet.Cells["D11:H11"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }

                        //title = worksheet.Cells["B12:H12"].RichText.Add("Địa chỉ: ");
                        //title.Bold = true;
                        worksheet.Cells["D12:H12"].Value = data.SYSCustomerEntity.Address ?? "---";
                        worksheet.Cells["D12:H12"].Style.Font.Bold = false;
                        if (data.SYSCustomerEntity.Address == null)
                        {
                            worksheet.Cells["D12:H12"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }

                        //title = worksheet.Cells["B13:H13"].RichText.Add("Thông tin khác: ");
                        //title.Bold = true;
                        worksheet.Cells["D13:H13"].Value = data.OtherInformation ?? "---";
                        worksheet.Cells["D13:H13"].Style.Font.Bold = false;
                        if (data.OtherInformation == null)
                        {
                            worksheet.Cells["D13:H13"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }

                        int currentRow = 16;
                        var specimenIndex = 0;
                        if (data.IDTestRequirementEntities != null)
                        {
                            foreach (var specimen in data.IDTestRequirementEntities)
                            {
                                var currentSpecimenRow = currentRow + 1;
                                var propertiesList = specimen.IDTRTestPropertyEntities.OrderBy(w => w.OrderNumber);
                                foreach (var property in propertiesList)
                                {
                                    currentRow++;

                                    //Property
                                    var propertyNameData = Core.StringHelper.ChemicalSymbolsHelper.FormatToElementArray(property.CTGTestPropertyEntity.Name);
                                    foreach (var (key, value) in propertyNameData)
                                    {
                                        switch (key)
                                        {
                                            case "content":
                                                worksheet.Cells["G" + currentRow + ":G" + currentRow].RichText.Add(value);
                                                break;

                                            case "sub":
                                                subScript = worksheet.Cells["G" + currentRow + ":G" + currentRow].RichText.Add(value);
                                                subScript.VerticalAlign = ExcelVerticalAlignmentFont.Subscript;
                                                break;

                                            case "sup":
                                                supScript = worksheet.Cells["G" + currentRow + ":G" + currentRow].RichText.Add(value);
                                                supScript.VerticalAlign = ExcelVerticalAlignmentFont.Superscript;
                                                break;
                                        }
                                    }

                                    //Method
                                    if (property.TestMethodId != null)
                                    {
                                        worksheet.Cells["H" + currentRow].RichText.Add(property.CTGTestMethodEntity.Name);
                                        supScript = worksheet.Cells["H" + currentRow].RichText.Add(property.CTGTestMethodEntity.SymbolAttached);
                                        supScript.VerticalAlign = ExcelVerticalAlignmentFont.Superscript;
                                    }
                                }
                                worksheet.Cells["B" + currentSpecimenRow + ":B" + currentRow].Merge = true;
                                worksheet.Cells["B" + currentSpecimenRow + ":B" + currentRow].Value = ++specimenIndex;
                                worksheet.Cells["C" + currentSpecimenRow + ":C" + currentRow].Merge = true;
                                worksheet.Cells["C" + currentSpecimenRow + ":C" + currentRow].Value = specimen.SpecimenName + (specimen.SpecimenSymbol != null ? " (" + specimen.SpecimenSymbol + ")" : "");
                                worksheet.Cells["D" + currentSpecimenRow + ":D" + currentRow].Merge = true;
                                worksheet.Cells["D" + currentSpecimenRow + ":D" + currentRow].Value = specimen.SpecimenAmount;
                                worksheet.Cells["E" + currentSpecimenRow + ":E" + currentRow].Merge = true;
                                worksheet.Cells["E" + currentSpecimenRow + ":E" + currentRow].Value = specimen.SpecimenQuantum;
                                worksheet.Cells["F" + currentSpecimenRow + ":F" + currentRow].Merge = true;
                                worksheet.Cells["F" + currentSpecimenRow + ":F" + currentRow].Value = specimen.SpecimenStatus;
                            }
                        }

                        worksheet.Cells["B17:H" + currentRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["B17:H" + currentRow].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        worksheet.Cells["B17:H" + currentRow].Style.WrapText = true;

                        worksheet.Cells["G17:G" + currentRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                        worksheet.Cells["B17:H" + currentRow].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["B17:H" + currentRow].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["B17:H" + currentRow].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["B17:H" + currentRow].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                        currentRow++;

                        worksheet.Cells["B" + currentRow + ":E" + currentRow].Merge = true;
                        worksheet.Cells["B" + currentRow + ":E" + currentRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells["B" + currentRow + ":E" + currentRow].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        title = worksheet.Cells["B" + currentRow + ":E" + currentRow].RichText.Add("Sử dụng nhà thầu phụ: ");
                        title.Bold = true;
                        content = worksheet.Cells["B" + currentRow + ":E" + currentRow].RichText.Add(data.IsUseSubcontractors ? "Có                    " : "Không                    ");
                        content.Bold = false;

                        worksheet.Cells["F" + currentRow + ":H" + currentRow].Merge = true;
                        worksheet.Cells["F" + currentRow + ":H" + currentRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells["F" + currentRow + ":H" + currentRow].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        title = worksheet.Cells["F" + currentRow + ":H" + currentRow].RichText.Add("Ngày dự kiến có kết quả: ");
                        title.Bold = true;
                        content = worksheet.Cells["F" + currentRow + ":H" + currentRow].RichText.Add(Convert.ToDateTime(data.ResultDay).ToString("dd/MM/yyyy"));
                        content.Bold = false;

                        currentRow++;

                        worksheet.Cells["B" + currentRow + ":E" + currentRow].Merge = true;
                        worksheet.Cells["B" + currentRow + ":E" + currentRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells["B" + currentRow + ":E" + currentRow].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        title = worksheet.Cells["B" + currentRow + ":E" + currentRow].RichText.Add("Lưu mẫu: ");
                        title.Bold = true;
                        content = worksheet.Cells["B" + currentRow + ":E" + currentRow].RichText.Add(data.IsSaveSpecimen ? "Có                    " : "Không                    ");
                        content.Bold = false;

                        worksheet.Cells["F" + currentRow + ":H" + currentRow].Merge = true;
                        worksheet.Cells["F" + currentRow + ":H" + currentRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells["F" + currentRow + ":H" + currentRow].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        title = worksheet.Cells["F" + currentRow + ":H" + currentRow].RichText.Add("Thời gian lưu mẫu (nếu có): ");
                        title.Bold = true;
                        content = worksheet.Cells["F" + currentRow + ":H" + currentRow].RichText.Add(data.SaveSpecimenTime ?? "          ---");
                        content.Bold = false;

                        currentRow++;

                        worksheet.Cells["B" + currentRow + ":E" + currentRow].Merge = true;
                        worksheet.Cells["B" + currentRow + ":E" + currentRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells["B" + currentRow + ":E" + currentRow].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        title = worksheet.Cells["B" + currentRow + ":E" + currentRow].RichText.Add("Trả kết quả: ");
                        title.Bold = true;
                        content = worksheet.Cells["B" + currentRow + ":E" + currentRow].RichText.Add(data.CTGReturnInvoiceResultTypeEntity.Name + "                    ");
                        content.Bold = false;

                        worksheet.Cells["F" + currentRow + ":H" + currentRow].Merge = true;
                        worksheet.Cells["F" + currentRow + ":H" + currentRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells["F" + currentRow + ":H" + currentRow].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        title = worksheet.Cells["F" + currentRow + ":H" + currentRow].RichText.Add("Số lượng PKQTN: ");
                        title.Bold = true;
                        content = worksheet.Cells["F" + currentRow + ":H" + currentRow].RichText.Add(data.ResultInvoiceAmount.ToString());
                        content.Bold = false;

                        currentRow += 2;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["B" + currentRow + ":H" + currentRow].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        worksheet.Cells["B" + currentRow + ":E" + currentRow].Merge = true;
                        worksheet.Cells["B" + currentRow + ":E" + currentRow].Value = "NGƯỜI GỬI MẪU";

                        worksheet.Cells["F" + currentRow + ":H" + currentRow].Merge = true;
                        worksheet.Cells["F" + currentRow + ":H" + currentRow].Value = "NGƯỜI NHẬN MẪU";

                        currentRow += 4;

                        worksheet.Cells["B" + currentRow + ":E" + currentRow].Merge = true;
                        worksheet.Cells["B" + currentRow + ":E" + currentRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["B" + currentRow + ":E" + currentRow].Style.Font.Bold = true;
                        worksheet.Cells["B" + currentRow + ":E" + currentRow].Value = data.Representative;

                        worksheet.Cells["F" + currentRow + ":H" + currentRow].Merge = true;
                        worksheet.Cells["F" + currentRow + ":H" + currentRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["F" + currentRow + ":H" + currentRow].Style.Font.Bold = true;
                        worksheet.Cells["F" + currentRow + ":H" + currentRow].Value = data.CRESYSUserEntity.DisplayName;

                        excelPackage.Save();

                        return excelPackage.Stream;
                    }
                }
            }
        }
    }
}