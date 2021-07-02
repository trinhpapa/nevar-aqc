using NEVAR_AQC.Core.Models.ReceptionDepartment;
using NEVAR_AQC.Core.Models.TestDepartment;
using NEVAR_AQC.Core.PagingHelper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Service.ReceptionDepartment
{
    public interface ISYSRequirementInvoiceService
    {
        Task<SYSRequirementInvoiceModel> CreateAsync(SYSRequirementInvoiceModel model, CancellationToken cancellationToken = default);

        Task<SYSRequirementInvoiceViewModel> GetByIdAsync(long invoiceId, CancellationToken cancellationToken = default);

        Task<SYSRequirementInvoiceModel> GetDetailTestRequirementByIdAsync(long id);

        Task<IEnumerable<SYSRequirementInvoiceViewModel>> GetByUserAsync(long userId, CancellationToken cancellationToken = default);

        Task<PagedResult<SYSRequirementInvoiceViewModel>> GetByUserPagingAsync(long userId,
            int pageIndex,
            int pageSize,
            int requirementType,
            string createdTime,
            string resultDay,
            int status,
            string searchFilter,
            CancellationToken cancellationToken = default);

        Task<PagedResult<SYSRequirementInvoiceViewModel>> GetAllPagingAsync(int pageIndex,
            int pageSize,
            int requirementType,
            string createdTime,
            string resultDay,
            int status,
            string searchFilter,
            CancellationToken cancellationToken = default);

        Task<PagedResult<SYSRequirementInvoiceViewModel>> GetByDepartmentPagingAsync(int pageIndex,
            int pageSize,
            string createdTime,
            string resultDay,
            int status,
            string searchFilter,
            CancellationToken cancellationToken = default);

        Task UpdateStatusAsync(SYSRequirementInvoiceUpdateStatusModel model, CancellationToken cancellationToken = default);

        Task<SYSRequirementInvoiceViewModel> RequirementInvoiceReportAsync(string invoiceNo, int edition);

        Task<PagedResult<IDTRImplementerModel>> GetDetailTestRequirementByImplementerAsync(long userId,
            int pageIndex,
            int pageSize,
            DateTime? fromTime,
            DateTime? toTime,
            bool? acceptStatus,
            string searchFilter);

        Task RemoveAsync(long invoiceId, long userId, CancellationToken cancellationToken = default);

        Task<SYSRequirementInvoiceModel> GetByIdForSummaryAsync(long invoiceId);

        Task<SYSRequirementInvoiceModel> GetByIdForUpdateAsync(long invoiceId);

        Task SubmitSummaryOfResults(SYSRequirementInvoiceUpdateStatusModel model, CancellationToken cancellationToken = default);

        Task<IDTestRequirementModel> GetByIdForReportAsync(long specimentId);

        Task<long> GetCurrentResultSerial(int year);

        Task UpdateInvoiceResultNo(long specimentId, long invoiceResultSerial, int invoiceResultYear, string invoiceResultNo, DateTime invoiceResultDate);

        Task UpdateInvoiceAsync(SYSRequirementInvoiceModel model);

        Task<int> GetCurrentEditionByInvoiceNo(string invoiceNo);

        Task<SYSRequirementInvoiceModel> GetByNoAndEditionAsync(string invoiceNo, int edition);
    }
}