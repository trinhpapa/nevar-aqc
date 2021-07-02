// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions </Copyright>
//     <Url> http://lehoangtrinh.com </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Business.Logic </Project>
//     <File>
//         <Name> SYSRequirementInvoiceBusiness.cs </Name>
//         <Created> 19/4/2019 - 13:54 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//
//     </Summary>
// <License>

using AutoMapper;
using NEVAR_AQC.Business.ReceptionDepartment;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.ReceptionDepartment;
using NEVAR_AQC.Core.Models.TestDepartment;
using NEVAR_AQC.Core.PagingHelper;
using NEVAR_AQC.Data.ReceptionDepartment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NEVAR_AQC.Business.Logic.ReceptionDepartment
{
    public class SYSRequirementInvoiceBusiness : ISYSRequirementInvoiceBusiness
    {
        private IMapper _mapper;
        private readonly ISYSRequirementInvoiceRepository _requirementInvoiceRepository;

        public SYSRequirementInvoiceBusiness(IMapper mapper,
            ISYSRequirementInvoiceRepository requirementInvoiceRepository)
        {
            _mapper = mapper;
            _requirementInvoiceRepository = requirementInvoiceRepository;
        }

        public Task<SYSRequirementInvoiceModel> CreateAsync(SYSRequirementInvoiceModel model, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<SYSRequirementInvoiceEntity>(model);

            var createdResult = _requirementInvoiceRepository.Create(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _requirementInvoiceRepository.SaveChange();

            var result = _mapper.Map<SYSRequirementInvoiceModel>(createdResult);

            return Task.FromResult(result);
        }

        public Task<PagedResult<SYSRequirementInvoiceViewModel>> GetByDepartmentPagingAsync(int pageIndex, int pageSize, string createdTime, string resultDay, int status, string searchFilter, CancellationToken cancellationToken = default)
        {
            var query = _requirementInvoiceRepository
                .Find(w => w.Edition == 0 && (w.IsDeleted == false || w.IsDeleted == null), x => x.CRESYSUserEntity, x => x.CTGRequirementTypeEntity, x => x.CTGRequirementStatusEntity, x => x.IDTestRequirementEntities, x => x.IDCalibrationRequirementEntities);

            if (!string.IsNullOrEmpty(createdTime))
            {
                var createdTimeSearch = Convert.ToDateTime(createdTime);
                query = query.Where(w => w.CreatedTime.Value.Date == createdTimeSearch.Date && w.CreatedTime.Value.Month == createdTimeSearch.Month && w.CreatedTime.Value.Year == createdTimeSearch.Year);
            }
            if (!string.IsNullOrEmpty(resultDay))
            {
                var resultTimeSearch = Convert.ToDateTime(resultDay);
                query = query.Where(w => w.ResultDay == resultTimeSearch);
            }
            if (status != 0)
            {
                query = query.Where(w => w.ProcessStatusId == status);
            }
            if (!string.IsNullOrEmpty(searchFilter))
            {
                query = query.Where(w => w.InvoiceNo.Contains(searchFilter));
            }

            var totalRow = query.Count();

            query = query.OrderByDescending(w => w.CreatedTime).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            var data = _mapper.Map<IList<SYSRequirementInvoiceViewModel>>(query);

            return Task.FromResult(new PagedResult<SYSRequirementInvoiceViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            });
        }

        public Task<SYSRequirementInvoiceViewModel> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var model = _requirementInvoiceRepository.FindSingle(w => w.Id == id && (w.IsDeleted == false || w.IsDeleted == null), w => w.CTGRequirementTypeEntity);
            var result = _mapper.Map<SYSRequirementInvoiceViewModel>(model);
            return Task.FromResult(result);
        }

        public Task<SYSRequirementInvoiceViewModel> GetByInvoiceNoAsync(string invoiceNo, CancellationToken cancellationToken = default)
        {
            var model = _requirementInvoiceRepository.Find(w => w.InvoiceNo == invoiceNo && (w.IsDeleted == false || w.IsDeleted == null), w => w.CTGRequirementTypeEntity).FirstOrDefault();
            var result = _mapper.Map<SYSRequirementInvoiceViewModel>(model);
            return Task.FromResult(result);
        }

        public Task<IEnumerable<SYSRequirementInvoiceViewModel>> GetByUserAsync(long userId, CancellationToken cancellationToken = default)
        {
            var model = _requirementInvoiceRepository
                .Find(w => w.CreatedBy == userId && w.Edition == 0 && (w.IsDeleted == false || w.IsDeleted == null), x => x.CTGRequirementTypeEntity, x => x.CTGRequirementStatusEntity, x => x.SYSCustomerEntity)
                .OrderByDescending(w => w.CreatedTime)
                .ToList();
            var result = _mapper.Map<IEnumerable<SYSRequirementInvoiceViewModel>>(model);
            return Task.FromResult(result);
        }

        public Task<PagedResult<SYSRequirementInvoiceViewModel>> GetByUserPagingAsync(long userId,
            int pageIndex,
            int pageSize,
            int requirementType,
            string createdTime,
            string resultDay,
            int status,
            string searchFilter,
            CancellationToken cancellationToken = default)
        {
            var query = _requirementInvoiceRepository
                .Find(w => w.CreatedBy == userId && w.Edition == 0 && (w.IsDeleted == false || w.IsDeleted == null), x => x.CTGRequirementTypeEntity, x => x.CTGRequirementStatusEntity, x => x.SYSCustomerEntity, w => w.IDTestRequirementEntities);
            if (requirementType != 0)
            {
                query = query.Where(w => w.RequirementTypeId == requirementType);
            }
            if (!string.IsNullOrEmpty(createdTime))
            {
                var createdTimeSearch = Convert.ToDateTime(createdTime);
                query = query.Where(w => w.CreatedTime.Value.Date == createdTimeSearch.Date && w.CreatedTime.Value.Month == createdTimeSearch.Month && w.CreatedTime.Value.Year == createdTimeSearch.Year);
            }
            if (!string.IsNullOrEmpty(resultDay))
            {
                var resultTimeSearch = Convert.ToDateTime(resultDay);
                query = query.Where(w => w.ResultDay == resultTimeSearch);
            }
            if (status != 0)
            {
                query = query.Where(w => w.ProcessStatusId == status);
            }
            if (!string.IsNullOrEmpty(searchFilter))
            {
                query = query.Where(w => w.SYSCustomerEntity.Name.ToLower().Contains(searchFilter.ToLower()) || w.Representative.ToLower().Contains(searchFilter.ToLower()) || w.InvoiceNo.ToLower().Contains(searchFilter.ToLower()));
            }
            var totalRow = query.Count();

            query = query.OrderByDescending(w => w.CreatedTime).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            var data = _mapper.Map<IList<SYSRequirementInvoiceViewModel>>(query);

            return Task.FromResult(new PagedResult<SYSRequirementInvoiceViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            });
        }

        public Task<PagedResult<SYSRequirementInvoiceViewModel>> GetAllPagingAsync(int pageIndex,
            int pageSize,
            int requirementType,
            string createdTime,
            string resultDay,
            int status,
            string searchFilter,
            CancellationToken cancellationToken = default)
        {
            var query = _requirementInvoiceRepository
                .Find(w => w.Edition == 0 && (w.IsDeleted == false || w.IsDeleted == null),
                   x => x.CTGRequirementTypeEntity, x => x.CTGRequirementStatusEntity, x => x.SYSCustomerEntity, w => w.IDTestRequirementEntities, x => x.CRESYSUserEntity);
            if (requirementType != 0)
            {
                query = query.Where(w => w.RequirementTypeId == requirementType);
            }
            if (!string.IsNullOrEmpty(createdTime))
            {
                var createdTimeSearch = Convert.ToDateTime(createdTime);
                query = query.Where(w => w.CreatedTime.Value.Date == createdTimeSearch.Date && w.CreatedTime.Value.Month == createdTimeSearch.Month && w.CreatedTime.Value.Year == createdTimeSearch.Year);
            }
            if (!string.IsNullOrEmpty(resultDay))
            {
                var resultTimeSearch = Convert.ToDateTime(resultDay);
                query = query.Where(w => w.ResultDay == resultTimeSearch);
            }
            if (status != 0)
            {
                query = query.Where(w => w.ProcessStatusId == status);
            }
            if (!string.IsNullOrEmpty(searchFilter))
            {
                query = query.Where(w => w.SYSCustomerEntity.Name.ToLower().Contains(searchFilter.ToLower()) || w.Representative.ToLower().Contains(searchFilter.ToLower()) || w.InvoiceNo.ToLower().Contains(searchFilter.ToLower()));
            }
            var totalRow = query.Count();

            query = query.OrderByDescending(w => w.CreatedTime).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            var data = _mapper.Map<IList<SYSRequirementInvoiceViewModel>>(query);

            return Task.FromResult(new PagedResult<SYSRequirementInvoiceViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            });
        }

        public Task<SYSRequirementInvoiceViewModel> GetByIdAndEditionAsync(long id, int edition, CancellationToken cancellationToken = default)
        {
            var query = _requirementInvoiceRepository.FindSingle(w => w.Id == id && w.Edition == edition && (w.IsDeleted == false || w.IsDeleted == null), x => x.CTGRequirementTypeEntity, x => x.CTGRequirementStatusEntity, x => x.SYSCustomerEntity);
            var result = _mapper.Map<SYSRequirementInvoiceViewModel>(query);
            return Task.FromResult(result);
        }

        public Task<SYSRequirementInvoiceModel> GetByIdForUpdateAsync(long invoiceId)
        {
            var query = _requirementInvoiceRepository.FindSingle(w => w.Id == invoiceId, w => w.SYSCustomerEntity, w => w.IDTestRequirementEntities);
            var result = _mapper.Map<SYSRequirementInvoiceModel>(query);
            return Task.FromResult(result);
        }

        public Task RemoveAsync(long id, long userId, CancellationToken cancellationToken = default)
        {
            var entity = _requirementInvoiceRepository.FindSingle(w => w.Id == id && w.CreatedBy == userId && w.ProcessStatusId == 1);
            entity.IsDeleted = true;

            _requirementInvoiceRepository.Update(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _requirementInvoiceRepository.SaveChange();

            return Task.CompletedTask;
        }

        public Task UpdateAsync(SYSRequirementInvoiceUpdateModel model, CancellationToken cancellationToken = default)
        {
            var updateEntity = _mapper.Map<SYSRequirementInvoiceEntity>(model);

            var entity = _requirementInvoiceRepository.FindSingle(x => x.Id == model.Id);

            entity = updateEntity;

            _requirementInvoiceRepository.Update(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _requirementInvoiceRepository.SaveChange();

            return Task.CompletedTask;
        }

        public Task UpdateStatusAsync(SYSRequirementInvoiceUpdateStatusModel model, CancellationToken cancellationToken = default)
        {
            var entity = _requirementInvoiceRepository.FindSingle(x => x.Id == model.Id);

            entity.ProcessStatusId = model.ProcessStatusId;

            _requirementInvoiceRepository.Update(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _requirementInvoiceRepository.SaveChange();

            return Task.CompletedTask;
        }

        public Task<SYSRequirementInvoiceViewModel> TestRequirementReportAsync(string invoiceNo, int edition)
        {
            //FIXME: Stackoverflow mapping
            var query = _requirementInvoiceRepository.TestRequirementReport(invoiceNo, edition).FirstOrDefault();
            var result = _mapper.Map<SYSRequirementInvoiceViewModel>(query);
            return Task.FromResult(result);
        }

        public Task<SYSRequirementInvoiceModel> GetDetailTestRequirementByIdAsync(long id)
        {
            var query = _requirementInvoiceRepository.GetDetailTestRequirementById(id).FirstOrDefault();
            var result = _mapper.Map<SYSRequirementInvoiceModel>(query);
            return Task.FromResult(result);
        }

        public Task<PagedResult<IDTRImplementerModel>> GetDetailTestRequirementByImplementerAsync(long userId,
            int pageIndex,
            int pageSize,
            DateTime? fromTime,
            DateTime? toTime,
            bool? acceptStatus,
            string searchFilter)
        {
            var query = _requirementInvoiceRepository.GetDetailTestRequirementByImplementer(userId);

            if (fromTime != null)
            {
                query = query.Where(w => w.IDTRTestPropertyEntity.PlanFromTime.Value.Date == fromTime.Value.Date && w.IDTRTestPropertyEntity.PlanFromTime.Value.Month == fromTime.Value.Month && w.IDTRTestPropertyEntity.PlanFromTime.Value.Year == fromTime.Value.Year);
            }

            if (toTime != null)
            {
                query = query.Where(w => w.IDTRTestPropertyEntity.PlanToTime.Value.Date == toTime.Value.Date && w.IDTRTestPropertyEntity.PlanToTime.Value.Month == toTime.Value.Month && w.IDTRTestPropertyEntity.PlanToTime.Value.Year == toTime.Value.Year);
            }

            if (acceptStatus != null)
            {
                query = query.Where(w => w.IsAccept == acceptStatus);
            }

            if (searchFilter != null)
            {
                query = query
                    .Where(w => w.IDTRTestPropertyEntity.IDTestRequirementEntity.SpecimenCode.Contains(searchFilter) || Regex.Replace(w.IDTRTestPropertyEntity.CTGTestPropertyEntity.Name, @"(<.?su[bp]>)", "").Contains(searchFilter));
            }

            var totalRow = query.Count();

            query = query.OrderBy(w => w.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            var data = _mapper.Map<IList<IDTRImplementerModel>>(query);

            return Task.FromResult(new PagedResult<IDTRImplementerModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            });
        }

        public Task<SYSRequirementInvoiceModel> GetByIdForSummaryAsync(long invoiceId)
        {
            var entity = _requirementInvoiceRepository.GetByIdForSummary(invoiceId).FirstOrDefault();

            //Stackover flow here
            var result = _mapper.Map<SYSRequirementInvoiceModel>(entity);

            return Task.FromResult(result);
        }

        public Task<SYSRequirementInvoiceModel> GetFullByIdForUpdateAsync(long invoiceId)
        {
            var entity = _requirementInvoiceRepository.GetFullByIdForUpdateTestRequirement(invoiceId).FirstOrDefault();

            var result = _mapper.Map<SYSRequirementInvoiceModel>(entity);

            return Task.FromResult(result);
        }

        public Task<int> GetCurrentEditionInvoiceByInvoiceNo(string invoiceNo)
        {
            var currentEdition = _requirementInvoiceRepository
                .Find(w => w.InvoiceNo == invoiceNo)
                .OrderByDescending(w => w.Edition)
                .FirstOrDefault()
                .Edition;
            return Task.FromResult(currentEdition);
        }

        public Task<SYSRequirementInvoiceModel> GetByNoAndEditionAsync(string invoiceNo, int edition)
        {
            var query = _requirementInvoiceRepository.FindSingle(w => w.InvoiceNo == invoiceNo && w.Edition == edition, w => w.SYSCustomerEntity, w => w.IDTestRequirementEntities);
            var result = _mapper.Map<SYSRequirementInvoiceModel>(query);
            return Task.FromResult(result);
        }

        public async Task<int?> GetLastSerialAsync(int year)
        {
            var query = _requirementInvoiceRepository.Find(w => w.SerialYear == year && w.IsDeleted == false);
            var result = await query.OrderByDescending(w => w.Serial).FirstOrDefaultAsync();
            return result?.Serial ?? 0;
        }

    }
}