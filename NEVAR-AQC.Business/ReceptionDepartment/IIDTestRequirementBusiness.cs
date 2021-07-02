using NEVAR_AQC.Core.Models.ReceptionDepartment;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.ReceptionDepartment
{
    public interface IIDTestRequirementBusiness
    {
        Task<IEnumerable<IDTestRequirementModel>> GetByInvoiceAsync(long invoiceId, CancellationToken cancellationToken = default);

        Task<IDTestRequirementModel> GetByIdForReportAsync(long specimentId);

        Task<long> GetLastResultSerialAsync(int year);

        Task UpdateResultNo(long specimentId, long invoiceResultSerial, int invoiceResultYear, string invoiceResultNo, DateTime invoiceResultDate);
    }
}