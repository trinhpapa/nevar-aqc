using NEVAR_AQC.Business.Notification;
using NEVAR_AQC.Business.ReceptionDepartment;
using NEVAR_AQC.Business.TestDepartment;
using NEVAR_AQC.Core.Models.ReceptionDepartment;
using NEVAR_AQC.Core.Models.TestDepartment;
using NEVAR_AQC.Service.TestDepartment;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Service.Facade.TestDepartment
{
    public class TestPlanService : ITestPlanService
    {
        private ISYSRequirementInvoiceBusiness _sYSRequirementInvoiceBusiness;
        private IIDTRImplementerBusiness _iDTRImplementerBusiness;
        private IIDTRTestPropertyBusiness _iDTRTestPropertyBusiness;
        private IImplementerNotification _implementerNotification;
        private IRequirementInvoiceNotification _requirementInvoiceNotification;

        public TestPlanService(ISYSRequirementInvoiceBusiness sYSRequirementInvoiceBusiness,
            IIDTRImplementerBusiness iDTRImplementerBusiness,
            IIDTRTestPropertyBusiness iDTRTestPropertyBusiness,
            IImplementerNotification implementerNotification,
            IRequirementInvoiceNotification requirementInvoiceNotification)
        {
            _sYSRequirementInvoiceBusiness = sYSRequirementInvoiceBusiness;
            _iDTRImplementerBusiness = iDTRImplementerBusiness;
            _iDTRTestPropertyBusiness = iDTRTestPropertyBusiness;
            _implementerNotification = implementerNotification;
            _requirementInvoiceNotification = requirementInvoiceNotification;
        }

        public async Task CreateAsync(IEnumerable<IDTRTestPropertyModel> model, CancellationToken cancellationToken = default)
        {
            foreach (var property in model)
            {
                await _iDTRTestPropertyBusiness.UpdatePlanAsync(property, cancellationToken);

                _requirementInvoiceNotification.SendNotificaion("InvoiceUpdate");

                _implementerNotification.SendNotificaion("implementerUpdate");
            }
        }

        public async Task DeleteSummaryOfResultItemAsync(IDTRTestPropertyModel model, CancellationToken cancellationToken = default)
        {
            var invoice = await _iDTRTestPropertyBusiness.GetInvoiceByTestPropertyAsync(model.Id);
            if (invoice.IDTestRequirementEntity.SYSRequirementInvoiceEntity.ProcessStatusId == 4)
            {
                await _iDTRTestPropertyBusiness.DeleteSummaryOfResultItemAsync(model);

                _implementerNotification.SendNotificaion("implementerUpdate");

                _requirementInvoiceNotification.SendNotificaion("InvoiceUpdate");
            }
            else
            {
                throw new Exception("Không thể xóa kết quả này!");
            }
        }

        public async Task<IDTRTestPropertyModel> GetPropertyForReportAsync(long propertyId)
        {
            return await _iDTRTestPropertyBusiness.GetPropertyForReportAsync(propertyId);
        }

        public async Task<IDTRTestPropertyModel> GetPropertyForResultAsync(long propertyId)
        {
            return await _iDTRTestPropertyBusiness.GetPropertyForResultAsync(propertyId);
        }

        public async Task ImplementerAcceptAsync(long implementerId, long invoiceId, CancellationToken cancellationToken = default)
        {
            var implementerHasAccept = _iDTRImplementerBusiness.CheckImplementerHasAccept(implementerId);
            if (!implementerHasAccept)
            {
                var invoiceModel = new SYSRequirementInvoiceUpdateStatusModel();
                invoiceModel.Id = invoiceId;
                invoiceModel.ProcessStatusId = 4;

                await _sYSRequirementInvoiceBusiness.UpdateStatusAsync(invoiceModel, cancellationToken);

                await _iDTRImplementerBusiness.UpdateTimeToStartAsync(implementerId);

                _requirementInvoiceNotification.SendNotificaion("InvoiceUpdate");

                await _iDTRImplementerBusiness.UpdateAcceptAsync(implementerId);

                _implementerNotification.SendNotificaion("implementerUpdate");
            }
            else
            {
                throw new Exception("Đã có người xử lý chỉ tiêu này!");
            }
        }

        public async Task UpdateTestProcess(IDTRTestPropertyModel model, CancellationToken cancellationToken = default)
        {
            var checkHasProcess = _iDTRTestPropertyBusiness.CheckHasProcess(model.Id);
            if (!checkHasProcess)
            {
                await _iDTRTestPropertyBusiness.DeleteSummaryOfResultItemAsync(model);
                await _iDTRTestPropertyBusiness.UpdateTestProcessAsync(model);
                _requirementInvoiceNotification.SendNotificaion("InvoiceUpdate");
                _implementerNotification.SendNotificaion("implementerUpdate");
            }
            else
            {
                throw new Exception("Báo cáo của chỉ tiêu này đã chốt!");
            }
        }
    }
}