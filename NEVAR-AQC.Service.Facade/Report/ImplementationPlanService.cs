using NEVAR_AQC.Core.Models.ReceptionDepartment;
using NEVAR_AQC.Service.Report;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.IO;
using System.Linq;

namespace NEVAR_AQC.Service.Facade.Report
{
    public class ImplementationPlanService : IImplementationPlanService
    {
        public Stream TestPlanReport(SYSRequirementInvoiceModel data, int currentRow = 11)
        {
            using (var file = new FileStream(@"wwwroot\resources\KHTN.xlsx", FileMode.Open, FileAccess.Read))
            {
                using (var newFile = new MemoryStream())
                {
                    using (var excelPackage = new ExcelPackage(file))
                    {
                        var rowIndex = 1;
                        var worksheet = excelPackage.Workbook.Worksheets.First();

                        foreach (var specimen in data.IDTestRequirementEntities)
                        {
                            var propertiesList = specimen.IDTRTestPropertyEntities.OrderBy(w => w.OrderNumber);
                            foreach (var property in propertiesList)
                            {
                                worksheet.Cells["A" + currentRow + ":A" + currentRow].Value = rowIndex;

                                worksheet.Cells["B" + currentRow + ":C" + currentRow].Merge = true;

                                //Error: NEVAR_AQC.Core.Models.ReceptionDepartment.IDTestRequirementModel.CTGTestObjectEntity.get returned null
                                worksheet.Cells["B" + currentRow + ":C" + currentRow].Value = specimen.CTGTestObjectEntity.Name;

                                worksheet.Cells["D" + currentRow + ":D" + currentRow].Value = specimen.SpecimenCode;

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

                                worksheet.Cells["A" + currentRow + ":H" + currentRow].Style.WrapText = true;
                                worksheet.Cells["A" + currentRow + ":H" + currentRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                worksheet.Cells["A" + currentRow + ":H" + currentRow].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                worksheet.Cells["E" + currentRow + ":E" + currentRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                worksheet.Cells["A" + currentRow + ":H" + currentRow].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells["A" + currentRow + ":H" + currentRow].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells["A" + currentRow + ":H" + currentRow].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells["A" + currentRow + ":H" + currentRow].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

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
    }
}