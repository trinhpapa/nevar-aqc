// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions </Copyright>
//     <Url> http://lehoangtrinh.com </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC </Project>
//     <File>
//         <Name> HomeController.cs </Name>
//         <Created> 25/6/2019 - 07:31 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//
//     </Summary>
// <License>

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NEVAR_AQC.Data.EF;
using NEVAR_AQC.Filters;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NEVAR_AQC.Controllers
{
    [SessionFilter]
    public class HomeController : Controller
    {
        private NEVARDbContext _ctx;

        public HomeController(NEVARDbContext ctx)
        {
            _ctx = ctx;
        }

        public IActionResult Index() => View();

        public IActionResult AccessDenied() => View();

        public async Task<IActionResult> UpdateInvoice(IFormFile file)
        {
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = null;

                    if (Path.GetExtension(file.FileName) == ".csv")
                    {
                        char csvDelimiter = ',';
                        worksheet = package.Workbook.Worksheets.Add("Sheet1");
                        ExcelTextFormat format = new ExcelTextFormat()
                        {
                            Delimiter = csvDelimiter
                        };
                        var stringData = System.Text.Encoding.UTF8.GetString(stream.ToArray()); ;
                        worksheet.Cells[1, 1].LoadFromText(stringData, format);
                    }
                    else
                    {
                        package.Load(stream);
                        worksheet = package.Workbook.Worksheets.First();
                    }

                    var start = worksheet.Dimension.Start;
                    var end = worksheet.Dimension.End;

                    var result = new List<ReadResult>();
                    var fail = new List<ReadResult>();

                    for (int row = start.Row + 1; row <= end.Row; row++)
                    {
                        var item = new ReadResult();
                        try
                        {
                            for (int col = start.Column; col <= end.Column; col++)
                            {
                                var title = worksheet.Cells[1, col].Value as String;

                                if (!string.IsNullOrEmpty(title))
                                {

                                    title = title.Trim().ToLower();

                                    if (title == "mã mẫu")
                                    {
                                        item.MaSoMau = worksheet.Cells[row, col].Value.ToString()?.Replace(" ", "");
                                    }
                                    if (title == "năm")
                                    {
                                        item.Nam = Convert.ToInt32(worksheet.Cells[row, col].Value);
                                    }
                                    if (title == "mã phiếu kq")
                                    {
                                        item.SoPhieu = worksheet.Cells[row, col].Value.ToString()?.Replace(" ", "");
                                    }
                                    if (title == "ngày ra phiếu kết quả")
                                    {
                                        var value = worksheet.Cells[row, col].Value;
                                        item.NgayIn = Convert.ToDateTime(value);
                                    }
                                }
                                else
                                {
                                    continue;
                                }

                            }

                            result.Add(item);
                        }
                        catch
                        {
                            fail.Add(item);
                            continue;
                        }
                    }

                    foreach (var item in result)
                    {
                        var update = await _ctx.IDTestRequirement.FirstOrDefaultAsync(i => i.SpecimenCode == item.MaSoMau && i.SYSRequirementInvoiceEntity.SerialYear == item.Nam);
                        if (update != null)
                        {
                            update.InvoiceResultNo = item.SoPhieu;
                            update.InvoiceResultDate = item.NgayIn;
                            update.InvoiceResultYear = Convert.ToInt32(item.SoPhieu.Split("/")[1]) + 2000;
                            update.InvoiceResultSerial = Convert.ToInt64(item.SoPhieu.Split("/")[0]);
                        }
                    }

                    await _ctx.SaveChangesAsync();
                    return new JsonResult(new
                    {
                        result,
                        fail
                    });
                }
            }
        }

        public async Task<IActionResult> GetTest()
        {

            var data = await _ctx.IDTestRequirement.Where(item => (item.InvoiceResultSerial == 19 || item.InvoiceResultSerial == 20 || item.InvoiceResultSerial == 21) && item.InvoiceResultYear == 2020).ToListAsync();
            foreach (var item in data)
            {
                item.InvoiceResultDate = null;
                item.InvoiceResultNo = null;
                item.InvoiceResultSerial = null;
                item.InvoiceResultYear = null;
            }

            await _ctx.SaveChangesAsync();

            return Ok();
        }
    }

    public class ReadResult
    {
        public string MaSoMau { get; set; }
        public int Nam { get; set; }
        public string SoPhieu { get; set; }
        public DateTime NgayIn { get; set; }
    }
}