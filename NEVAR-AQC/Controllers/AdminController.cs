using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NEVAR_AQC.Data.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NEVAR_AQC.Controllers
{
    public class AdminController : Controller
    {
        private NEVARDbContext _ctx;

        public AdminController(NEVARDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<IActionResult> RemovePlan(long invoiceId)
        {
            var invoice = await _ctx.SYSRequirementInvoice.FirstOrDefaultAsync(item => item.Serial == 74 && item.SerialYear == 2020 && item.IsDeleted == false);
            var testRequirement = await _ctx.IDTestRequirement.Where(item => item.RequirementInvoiceId == invoice.Id).Include(item => item.IDTRTestPropertyEntities).ThenInclude(item => item.IDTRImplementerEntities).ToListAsync();
            foreach (var item in testRequirement)
            {
                foreach(var an in item.IDTRTestPropertyEntities)
                {
                    an.PlanFromTime = null;
                    an.PlanToTime = null;
                    _ctx.IDTRImplementer.RemoveRange(an.IDTRImplementerEntities);
                }
            }

            await _ctx.SaveChangesAsync();
            return Ok();
        }

        public IActionResult Test()
        {
            var data = System.IO.File.ReadAllText("Data.txt");
            var data1 = data.Split("&");
            return new JsonResult(data);
        }
    }
}
