using System;
using Dapper;
using Microsoft.Extensions.Configuration;
using NEVAR_AQC.Core.Models.Statistic;
using NEVAR_AQC.Data.Statistic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NEVAR_AQC.Data.EF.Repositories.Statistic
{
    public class HomeStatisticRepository : IHomeStatisticRepository
    {
        private string _connectionString;
        private readonly NEVARDbContext _ctx;

        public HomeStatisticRepository(IConfiguration configuration, NEVARDbContext ctx)
        {
            _ctx = ctx;
            _connectionString = configuration.GetConnectionString("NEVARConnection");
        }

        public Task<IQueryable<LineStatistic>> GetLineStatistic()
        {
            var currentYear = DateTime.Now.Year;
            var data = _ctx.SYSRequirementInvoice.AsNoTracking()
               .Where(item => item.IsDeleted == false && item.CreatedTime.Value.Year == currentYear)
               .GroupBy(item => new { item.CreatedTime.Value.Month, item.RequirementTypeId })
               .Select(item => new LineStatistic
               {
                   Month = item.Key.Month,
                   Year = currentYear,
                   TestRequirement = item.Count(i => i.RequirementTypeId == 1),
                   CalibrationRequirement = item.Count(i => i.RequirementTypeId == 2),
               })
               .OrderBy(item => item.Month)
               .AsQueryable();

            return Task.FromResult(data);
        }

        public HomeStatisticModel GetHomeStatistic()
        {
            var sql = @"SELECT
                            (SELECT COUNT(Invoice.Id) FROM SYSRequirementInvoice As Invoice
                            WHERE Invoice.IsDeleted = 0) As InvoiceCount,
                            (SELECT COUNT(Customer.Id) FROM SYSCustomer As Customer
                            WHERE Customer.IsDeleted = 0) As CustomerCount,
                            (SELECT COUNT(Field.Id) FROM CTGField As Field
                            WHERE Field.IsDeleted = 0) As FieldCount,
                            (SELECT COUNT(Department.Id) FROM CTGDepartment As Department
                            WHERE Department.IsDeleted = 0) As DepartmentCount";
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                return conn.Query<HomeStatisticModel>(sql).FirstOrDefault();
            }
        }
    }
}