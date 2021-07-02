using NEVAR_AQC.Core.Models.ReceptionDepartment;
using NEVAR_AQC.Core.Models.TestDepartment;
using NEVAR_AQC.Core.PagingHelper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.ReceptionDepartment
{
   public interface ISYSRequirementInvoiceBusiness
   {
      #region RequirementInvoice - Get

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

      Task<SYSRequirementInvoiceViewModel> GetByIdAsync(long id, CancellationToken cancellationToken = default);

      Task<SYSRequirementInvoiceViewModel> GetByInvoiceNoAsync(string invoiceNo, CancellationToken cancellationToken = default);

      Task<PagedResult<SYSRequirementInvoiceViewModel>> GetByDepartmentPagingAsync(int pageIndex,
          int pageSize,
          string createdTime,
          string resultDay,
          int status,
          string searchFilter,
          CancellationToken cancellationToken = default);

      Task<SYSRequirementInvoiceViewModel> GetByIdAndEditionAsync(long id, int edition, CancellationToken cancellationToken = default);

      Task<SYSRequirementInvoiceModel> GetDetailTestRequirementByIdAsync(long id);

      Task<PagedResult<IDTRImplementerModel>> GetDetailTestRequirementByImplementerAsync(long userId,
          int pageIndex,
          int pageSize,
          DateTime? fromTime,
          DateTime? toTime,
          bool? acceptStatus,
          string searchFilter);

      Task<SYSRequirementInvoiceModel> GetByIdForSummaryAsync(long invoiceId);

      Task<SYSRequirementInvoiceModel> GetByIdForUpdateAsync(long invoiceId);

      Task<SYSRequirementInvoiceModel> GetFullByIdForUpdateAsync(long invoiceId);

      Task<int> GetCurrentEditionInvoiceByInvoiceNo(string invoiceNo);

      Task<SYSRequirementInvoiceModel> GetByNoAndEditionAsync(string invoiceNo, int edition);

      Task<int?> GetLastSerialAsync(int year);

      #endregion RequirementInvoice - Get

      #region RequirementInvoice - Create

      Task<SYSRequirementInvoiceModel> CreateAsync(SYSRequirementInvoiceModel model, CancellationToken cancellationToken = default);

      #endregion RequirementInvoice - Create

      #region RequirementInvoice - Update

      Task UpdateStatusAsync(SYSRequirementInvoiceUpdateStatusModel model, CancellationToken cancellationToken = default);

      Task UpdateAsync(SYSRequirementInvoiceUpdateModel model, CancellationToken cancellationToken = default);

      #endregion RequirementInvoice - Update

      #region RequirementInvoice - Remove

      Task RemoveAsync(long id, long userId, CancellationToken cancellationToken = default);

      #endregion RequirementInvoice - Remove

      #region RequirementInvoice - Report

      Task<SYSRequirementInvoiceViewModel> TestRequirementReportAsync(string invoiceNo, int edition);

      #endregion RequirementInvoice - Report
   }
}