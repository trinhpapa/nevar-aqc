// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions </Copyright>
//     <Url> http://lehoangtrinh.com </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Service.Facade </Project>
//     <File>
//         <Name> SYSRequirementInvoiceService.cs </Name>
//         <Created> 19/4/2019 - 13:54 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//
//     </Summary>
// <License>

using NEVAR_AQC.Business.Managerment;
using NEVAR_AQC.Business.Notification;
using NEVAR_AQC.Business.ReceptionDepartment;
using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Core.Models.ReceptionDepartment;
using NEVAR_AQC.Core.Models.TestDepartment;
using NEVAR_AQC.Core.PagingHelper;
using NEVAR_AQC.Service.ReceptionDepartment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NEVAR_AQC.Core.StringHelper;

namespace NEVAR_AQC.Service.Facade.ReceptionDepartment
{
    public class SYSRequirementInvoiceService : ISYSRequirementInvoiceService
    {
        private readonly ISYSRequirementInvoiceBusiness _requirementInvoiceBusiness;
        private readonly ISYSCustomerBusiness _customerBusiness;
        private readonly IIDTestRequirementBusiness _iDTestRequirementBusiness;
        private readonly IRequirementInvoiceNotification _requirementInvoiceNotification;
        private readonly ICustomerNotification _customerNotification;
        private readonly ICTGRequirementTypeBusiness _ctgRequirementTypeBusiness;
        private readonly ICTGFieldBusiness _ctgFieldBusiness;

        public SYSRequirementInvoiceService(ISYSRequirementInvoiceBusiness requirementInvoiceBusiness,
            ISYSCustomerBusiness customerBusiness,
            IIDTestRequirementBusiness iDTestRequirementBusiness,
            IRequirementInvoiceNotification requirementInvoiceNotification,
            ICustomerNotification customerNotification, ICTGRequirementTypeBusiness ctgRequirementTypeBusiness, ICTGFieldBusiness ctgFieldBusiness)
        {
            _requirementInvoiceBusiness = requirementInvoiceBusiness;
            _customerBusiness = customerBusiness;
            _requirementInvoiceNotification = requirementInvoiceNotification;
            _customerNotification = customerNotification;
            _ctgRequirementTypeBusiness = ctgRequirementTypeBusiness;
            _ctgFieldBusiness = ctgFieldBusiness;
            _iDTestRequirementBusiness = iDTestRequirementBusiness;
        }

        public async Task<SYSRequirementInvoiceModel> CreateAsync(SYSRequirementInvoiceModel model, CancellationToken cancellationToken = default)
        {
            model.ProcessStatusId = 1;
            model.Edition = 0;
            model.CreatedTime = DateTime.Now;

            var customerCheck = await _customerBusiness.GetByNameAsync(model.SYSCustomerEntity.Name.Trim());
            if (customerCheck == null)
            {
                var customerModel = new SYSCustomerCreateModel
                {
                    CustomerTypeId = 1,
                    Name = model.SYSCustomerEntity.Name.Trim(),
                    Address = model.SYSCustomerEntity.Address.Trim(),
                    PhoneNumber = model.SYSCustomerEntity.PhoneNumber,
                    Fax = model.SYSCustomerEntity.Fax
                };

                var customerCreateResult = await _customerBusiness.CreateAsync(customerModel, cancellationToken);

                model.CustomerId = customerCreateResult.Id;

                _customerNotification.SendNotificaion("customerUpdate");
            }
            else
            {
                if (customerCheck.Address.Trim() == model.SYSCustomerEntity.Address.Trim())
                {
                    model.CustomerId = customerCheck.Id;
                }
                else
                {
                    var customerModel = new SYSCustomerCreateModel
                    {
                        CustomerTypeId = 1,
                        Name = model.SYSCustomerEntity.Name.Trim(),
                        Address = model.SYSCustomerEntity.Address.Trim(),
                        PhoneNumber = model.SYSCustomerEntity.PhoneNumber,
                        Fax = model.SYSCustomerEntity.Fax
                    };

                    var customerCreateResult = await _customerBusiness.CreateAsync(customerModel, cancellationToken);

                    model.CustomerId = customerCreateResult.Id;

                    _customerNotification.SendNotificaion("customerUpdate");
                }
            }

            var currentYear = DateTime.Now.Year;

            var currentSerial = await _requirementInvoiceBusiness.GetLastSerialAsync(currentYear);

            model.Serial = (int)currentSerial + 1;

            model.SerialYear = currentYear;

            var typeAlias = await _ctgRequirementTypeBusiness.GetByIdAsync(model.RequirementTypeId);

            model.InvoiceNo = $"{model.Serial}/{model.SerialYear}/{typeAlias.Alias}";

            var fieldSymbol = (await _ctgFieldBusiness.GetByIdAsync(model.FieldId)).Symbol;

            foreach (var tq in model.IDTestRequirementEntities)
            {
                tq.SpecimenCode = $"TN{fieldSymbol}/{model.Serial}/{tq.SpecimenOrder.ToNumberString()}";
            }

            var createdResult = await _requirementInvoiceBusiness.CreateAsync(model, cancellationToken);

            _requirementInvoiceNotification.SendNotificaion("InvoiceUpdate");

            return createdResult;
        }

        public async Task<PagedResult<SYSRequirementInvoiceViewModel>> GetByDepartmentPagingAsync(int pageIndex,
            int pageSize,
            string createdTime,
            string resultDay,
            int status,
            string searchFilter,
            CancellationToken cancellationToken = default)
        {
            return await _requirementInvoiceBusiness.GetByDepartmentPagingAsync(pageIndex, pageSize, createdTime, resultDay, status, searchFilter, cancellationToken);
        }

        public async Task<SYSRequirementInvoiceViewModel> GetByIdAsync(long invoiceId, CancellationToken cancellationToken = default)
        {
            return await _requirementInvoiceBusiness.GetByIdAsync(invoiceId, cancellationToken);
        }

        public async Task<PagedResult<SYSRequirementInvoiceViewModel>> GetByUserPagingAsync(long userId,
            int pageIndex,
            int pageSize,
            int requirementType,
            string createdTime,
            string resultDay,
            int status,
            string searchFilter,
            CancellationToken cancellationToken = default)
        {
            return await _requirementInvoiceBusiness.GetByUserPagingAsync(userId, pageIndex, pageSize, requirementType, createdTime, resultDay, status, searchFilter, cancellationToken);
        }

        public async Task<PagedResult<SYSRequirementInvoiceViewModel>> GetAllPagingAsync(int pageIndex,
            int pageSize,
            int requirementType,
            string createdTime,
            string resultDay,
            int status,
            string searchFilter,
            CancellationToken cancellationToken = default)
        {
            return await _requirementInvoiceBusiness.GetAllPagingAsync(pageIndex, pageSize, requirementType, createdTime, resultDay, status, searchFilter, cancellationToken);
        }

        public async Task<IEnumerable<SYSRequirementInvoiceViewModel>> GetByUserAsync(long userId, CancellationToken cancellationToken = default)
        {
            return await _requirementInvoiceBusiness.GetByUserAsync(userId, cancellationToken);
        }

        public async Task<SYSRequirementInvoiceViewModel> GetByIdAndEditionAsync(long id, int edition, CancellationToken cancellationToken = default)
        {
            return await _requirementInvoiceBusiness.GetByIdAndEditionAsync(id, edition, cancellationToken);
        }

        public async Task UpdateStatusAsync(SYSRequirementInvoiceUpdateStatusModel model, CancellationToken cancellationToken = default)
        {
            model.ModifiedTime = DateTime.Now;

            await _requirementInvoiceBusiness.UpdateStatusAsync(model, cancellationToken);

            _requirementInvoiceNotification.SendNotificaion("InvoiceUpdate");
        }

        public async Task SubmitSummaryOfResults(SYSRequirementInvoiceUpdateStatusModel model, CancellationToken cancellationToken = default)
        {
            var invoiceData = await _requirementInvoiceBusiness.GetByIdForSummaryAsync(model.Id);

            if (invoiceData.ProcessStatusId == 5)
            {
                throw new Exception("Phiếu này đã có kết quả, không thể xác nhận");
            }

            foreach (var specimen in invoiceData.IDTestRequirementEntities)
            {
                foreach (var property in specimen.IDTRTestPropertyEntities)
                {
                    if (!property.IDTRTestProcessWeightMethodEntities.Any() && !property.IDTRTestProcessVolumeMethodEntities.Any()
                        && !property.IDTRTestProcessOtherMethodEntities.Any() && !property.IDTRTestProcessAASUCVISAESMethodEntities.Any())
                    {
                        throw new Exception("Chỉ tiêu (" + property.CTGTestPropertyEntity.Name + ") chưa có báo cáo!");
                    }
                }
            }

            model.ModifiedTime = DateTime.Now;

            await _requirementInvoiceBusiness.UpdateStatusAsync(model, cancellationToken);

            _requirementInvoiceNotification.SendNotificaion("InvoiceUpdate");
        }

        public async Task<SYSRequirementInvoiceViewModel> RequirementInvoiceReportAsync(string invoiceNo, int edition)
        {
            var invoice = await _requirementInvoiceBusiness.GetByInvoiceNoAsync(invoiceNo);
            if (invoice.CTGRequirementTypeEntity.Alias == "YCTN")
            {
                return await _requirementInvoiceBusiness.TestRequirementReportAsync(invoiceNo, edition);
            }
            throw new FieldAccessException();
        }

        public async Task<SYSRequirementInvoiceModel> GetDetailTestRequirementByIdAsync(long id)
        {
            var data = await _requirementInvoiceBusiness.GetDetailTestRequirementByIdAsync(id);
            data.CRESYSUserEntity.PasswordEncrypted = null;
            data.CRESYSUserEntity.PasswordSalt = null;
            data.CRESYSUserEntity.DateOfBirth = null;
            data.CRESYSUserEntity.ActiveStatus = false;
            data.CRESYSUserEntity.Email = null;
            data.CRESYSUserEntity.PhoneNumber = null;
            return data;
        }

        public async Task<PagedResult<IDTRImplementerModel>> GetDetailTestRequirementByImplementerAsync(long userId,
            int pageIndex,
            int pageSize,
            DateTime? fromTime,
            DateTime? toTime,
            bool? acceptStatus,
            string searchFilter)
        {
            var data = await _requirementInvoiceBusiness.GetDetailTestRequirementByImplementerAsync(userId, pageIndex, pageSize, fromTime, toTime, acceptStatus, searchFilter);
            return data;
        }

        public async Task RemoveAsync(long invoiceId, long userId, CancellationToken cancellationToken = default)
        {
            await _requirementInvoiceBusiness.RemoveAsync(invoiceId, userId, cancellationToken);

            _requirementInvoiceNotification.SendNotificaion("InvoiceUpdate");
        }

        public async Task<SYSRequirementInvoiceModel> GetByIdForSummaryAsync(long invoiceId)
        {
            return await _requirementInvoiceBusiness.GetByIdForSummaryAsync(invoiceId);
        }

        public async Task<IDTestRequirementModel> GetByIdForReportAsync(long specimentId)
        {
            return await _iDTestRequirementBusiness.GetByIdForReportAsync(specimentId);
        }

        public async Task<SYSRequirementInvoiceModel> GetByIdForUpdateAsync(long invoiceId)
        {
            return await _requirementInvoiceBusiness.GetByIdForUpdateAsync(invoiceId);
        }

        /// <summary>
        /// Sửa phiếu yêu cầu, bao gồm sửa thông tin khách hàng và tên các mẫu thử.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateInvoiceAsync(SYSRequirementInvoiceModel model)
        {
            var oldInvoice = await _requirementInvoiceBusiness.GetFullByIdForUpdateAsync(model.Id);

            var currentEdition = await _requirementInvoiceBusiness.GetCurrentEditionInvoiceByInvoiceNo(oldInvoice.InvoiceNo);

            oldInvoice.Id = 0;

            oldInvoice.Edition = ++currentEdition;

            oldInvoice.Representative = model.Representative;

            #region Child Handle

            foreach (var oldSpecimen in oldInvoice.IDTestRequirementEntities)
            {
                oldSpecimen.Id = 0;
                oldSpecimen.RequirementInvoiceId = 0;

                var newSpecimen = model.IDTestRequirementEntities?.Where(w => w.SpecimenCode == oldSpecimen.SpecimenCode).First();
                if (newSpecimen?.SpecimenName != null)
                {
                    oldSpecimen.SpecimenName = newSpecimen.SpecimenName;
                }
                if (newSpecimen?.SpecimenSymbol != null)
                {
                    oldSpecimen.SpecimenSymbol = newSpecimen.SpecimenSymbol;
                }

                oldSpecimen.IDTRTestPropertyEntities = null;
            }

            #endregion Child Handle

            var customerCheck = await _customerBusiness.GetByNameAsync(model.SYSCustomerEntity.Name.Trim());
            if (customerCheck == null)
            {
                var customerModel = new SYSCustomerCreateModel
                {
                    CustomerTypeId = 1,
                    Name = model.SYSCustomerEntity.Name.Trim(),
                    Address = model.SYSCustomerEntity.Address.Trim(),
                    PhoneNumber = model.SYSCustomerEntity.PhoneNumber,
                    Fax = model.SYSCustomerEntity.Fax
                };

                var customerCreateResult = await _customerBusiness.CreateAsync(customerModel);

                model.CustomerId = customerCreateResult.Id;

                _customerNotification.SendNotificaion("customerUpdate");
            }
            else
            {
                if (customerCheck.Address.Trim() == model.SYSCustomerEntity.Address.Trim())
                {
                    model.CustomerId = customerCheck.Id;
                }
                else
                {
                    var customerModel = new SYSCustomerCreateModel
                    {
                        CustomerTypeId = 1,
                        Name = model.SYSCustomerEntity.Name.Trim(),
                        Address = model.SYSCustomerEntity.Address.Trim(),
                        PhoneNumber = model.SYSCustomerEntity.PhoneNumber,
                        Fax = model.SYSCustomerEntity.Fax
                    };

                    var customerCreateResult = await _customerBusiness.CreateAsync(customerModel);

                    model.CustomerId = customerCreateResult.Id;

                    _customerNotification.SendNotificaion("customerUpdate");
                }
            }

            await _requirementInvoiceBusiness.CreateAsync(oldInvoice);

            _requirementInvoiceNotification.SendNotificaion("InvoiceUpdate");
        }

        public async Task<int> GetCurrentEditionByInvoiceNo(string invoiceNo)
        {
            return await _requirementInvoiceBusiness.GetCurrentEditionInvoiceByInvoiceNo(invoiceNo);
        }

        public async Task<SYSRequirementInvoiceModel> GetByNoAndEditionAsync(string invoiceNo, int edition)
        {
            return await _requirementInvoiceBusiness.GetByNoAndEditionAsync(invoiceNo, edition);
        }

        public async Task<long> GetCurrentResultSerial(int year)
        {
            return await _iDTestRequirementBusiness.GetLastResultSerialAsync(year);
        }

        public async Task UpdateInvoiceResultNo(long specimentId, long invoiceResultSerial, int invoiceResultYear, string invoiceResultNo, DateTime invoiceResultDate)
        {
             await _iDTestRequirementBusiness.UpdateResultNo(specimentId, invoiceResultSerial, invoiceResultYear, invoiceResultNo, invoiceResultDate);
        }
    }
}