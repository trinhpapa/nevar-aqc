using AutoMapper;
using NEVAR_AQC.Business.TestDepartment;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.TestDepartment;
using NEVAR_AQC.Data.TestDepartment;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.Logic.TestDepartment
{
    public class IDTRImplementerBusiness : IIDTRImplementerBusiness
    {
        private IMapper _mapper;
        private IIDTRImplementerRepository _iDTRImplementerRepository;

        public IDTRImplementerBusiness(IMapper mapper,
            IIDTRImplementerRepository iDTRImplementerRepository)
        {
            _mapper = mapper;
            _iDTRImplementerRepository = iDTRImplementerRepository;
        }

        public Task CreateAsync(IDTRImplementerModel model, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<IDTRImplementerEntity>(model);

            _iDTRImplementerRepository.Create(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _iDTRImplementerRepository.SaveChange();

            return Task.CompletedTask;
        }

        public Task DeleteAsync(IDTRImplementerModel model, CancellationToken cancellationToken = default)
        {
            var entity = _iDTRImplementerRepository.FindSingle(w => w.Id == model.Id);

            _iDTRImplementerRepository.Delete(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _iDTRImplementerRepository.SaveChange();

            return Task.CompletedTask;
        }

        public Task UpdateAcceptAsync(long implementerId, CancellationToken cancellationToken = default)
        {
            var entity = _iDTRImplementerRepository.FindSingle(w => w.Id == implementerId);
            entity.IsAccept = true;

            _iDTRImplementerRepository.Update(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _iDTRImplementerRepository.SaveChange();

            return Task.CompletedTask;
        }

        public bool CheckImplementerHasAccept(long implementerId)
        {
            var specimenId = _iDTRImplementerRepository.FindSingle(w => w.Id == implementerId).SpecimenPropertyId;
            return _iDTRImplementerRepository.Find(w => w.SpecimenPropertyId == specimenId && w.IsAccept == true).Any(); ;
        }

        public bool CheckIsImplementer(long userId, long propertyId)
        {
            return _iDTRImplementerRepository.FindSingle(w => w.UserId == userId && w.SpecimenPropertyId == propertyId).IsAccept;
        }

        public bool CheckImplementerByProperty(long userId, long propertyId)
        {
            return _iDTRImplementerRepository.Find(w => w.SpecimenPropertyId == propertyId && w.UserId == userId).Any();
        }

        public Task UpdateTimeToStartAsync(long implementerId)
        {
            var entity = _iDTRImplementerRepository.FindSingle(w => w.Id == implementerId);
            entity.TimeToStart = DateTime.Now;

            _iDTRImplementerRepository.Update(entity);

            _iDTRImplementerRepository.SaveChange();

            return Task.CompletedTask;
        }

        public Task UpdateTimeToReportAsync(long implementerId)
        {
            var entity = _iDTRImplementerRepository.FindSingle(w => w.Id == implementerId);
            entity.TimeToReport = DateTime.Now;

            _iDTRImplementerRepository.Update(entity);

            _iDTRImplementerRepository.SaveChange();

            return Task.CompletedTask;
        }
        
    }
}