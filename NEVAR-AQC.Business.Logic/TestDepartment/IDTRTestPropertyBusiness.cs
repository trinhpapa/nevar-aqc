using AutoMapper;
using NEVAR_AQC.Business.TestDepartment;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.TestDepartment;
using NEVAR_AQC.Data.TestDepartment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.Logic.TestDepartment
{
    public class IDTRTestPropertyBusiness : IIDTRTestPropertyBusiness
    {
        private IMapper _mapper;
        private IIDTRTestPropertyRepository _iDTRTestPropertyRepository;

        public IDTRTestPropertyBusiness(IMapper mapper,
            IIDTRTestPropertyRepository iDTRTestPropertyRepository)
        {
            _mapper = mapper;
            _iDTRTestPropertyRepository = iDTRTestPropertyRepository;
        }

        public Task UpdatePlanAsync(IDTRTestPropertyModel model, CancellationToken cancellationToken = default)
        {
            var entity = _iDTRTestPropertyRepository.FindSingle(w => w.Id == model.Id, w => w.IDTestRequirementEntity, w => w.IDTestRequirementEntity.SYSRequirementInvoiceEntity);
            entity.PlanFromTime = model.PlanFromTime;
            entity.PlanToTime = model.PlanToTime;
            entity.IDTRImplementerEntities = _mapper.Map<ICollection<IDTRImplementerEntity>>(model.IDTRImplementerEntities);
            entity.IDTestRequirementEntity.SYSRequirementInvoiceEntity.ProcessStatusId = 3;

            _iDTRTestPropertyRepository.Update(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _iDTRTestPropertyRepository.SaveChange();

            return Task.CompletedTask;
        }

        public Task UpdateTestMethod(IDTRTestPropertyModel model, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public bool CheckHasProcess(long propertyId)
        {
            var entity = _iDTRTestPropertyRepository.FindSingle(w => w.Id == propertyId, w => w.IDTRTestProcessWeightMethodEntities,
                w => w.IDTRTestProcessVolumeMethodEntities, w => w.IDTRTestProcessOtherMethodEntities, w => w.IDTRTestProcessAASUCVISAESMethodEntities);
            if (entity.IDTRTestProcessWeightMethodEntities.Where(w => w.IsSubmitReport == true).Any() || entity.IDTRTestProcessVolumeMethodEntities.Where(w => w.IsSubmitReport == true).Any()
                || entity.IDTRTestProcessOtherMethodEntities.Where(w => w.IsSubmitReport == true).Any() || entity.IDTRTestProcessAASUCVISAESMethodEntities.Where(w => w.IsSubmitReport == true).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task UpdateTestProcessAsync(IDTRTestPropertyModel model, CancellationToken cancellationToken = default)
        {
            var entity = _iDTRTestPropertyRepository.FindSingle(w => w.Id == model.Id);
            if (model.IDTRTestProcessWeightMethodEntities != null)
            {
                entity.IDTRTestProcessWeightMethodEntities = _mapper.Map<ICollection<IDTRTestProcessWeightMethodEntity>>(model.IDTRTestProcessWeightMethodEntities);
            }
            if (model.IDTRTestProcessVolumeMethodEntities != null)
            {
                entity.IDTRTestProcessVolumeMethodEntities = _mapper.Map<ICollection<IDTRTestProcessVolumeMethodEntity>>(model.IDTRTestProcessVolumeMethodEntities);
            }
            if (model.IDTRTestProcessOtherMethodEntities != null)
            {
                entity.IDTRTestProcessOtherMethodEntities = _mapper.Map<ICollection<IDTRTestProcessOtherMethodEntity>>(model.IDTRTestProcessOtherMethodEntities);
            }
            if (model.IDTRTestProcessAASUCVISAESMethodEntities != null)
            {
                entity.IDTRTestProcessAASUCVISAESMethodEntities = _mapper.Map<ICollection<IDTRTestProcessAASUCVISAESMethodEntity>>(model.IDTRTestProcessAASUCVISAESMethodEntities);
            }

            _iDTRTestPropertyRepository.Update(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _iDTRTestPropertyRepository.SaveChange();

            return Task.CompletedTask;
        }

        public Task DeleteSummaryOfResultItemAsync(IDTRTestPropertyModel model, CancellationToken cancellationToken = default)
        {
            _iDTRTestPropertyRepository.DeleteSummaryOfResultItem(model.Id);
            return Task.CompletedTask;
        }

        public Task<IDTRTestPropertyModel> GetPropertyForReportAsync(long propertyId)
        {
            var query = _iDTRTestPropertyRepository.GetTestPropertyForReport(propertyId).FirstOrDefault();
            var result = _mapper.Map<IDTRTestPropertyModel>(query);
            return Task.FromResult(result);
        }

        public Task<IDTRTestPropertyModel> GetPropertyForResultAsync(long propertyId)
        {
            var query = _iDTRTestPropertyRepository.GetTestPropertyForResult(propertyId).FirstOrDefault();
            var result = _mapper.Map<IDTRTestPropertyModel>(query);
            return Task.FromResult(result);
        }

        public Task<IDTRTestPropertyModel> GetInvoiceByTestPropertyAsync(long propertyId)
        {
            var query = _iDTRTestPropertyRepository.GetInvoiceByTestProperty(propertyId).FirstOrDefault();
            var result = _mapper.Map<IDTRTestPropertyModel>(query);
            return Task.FromResult(result);
        }
    }
}