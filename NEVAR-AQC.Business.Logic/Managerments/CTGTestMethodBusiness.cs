using AutoMapper;
using NEVAR_AQC.Business.Managerment;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Core.PagingHelper;
using NEVAR_AQC.Data.Managements;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.Logic.Managerments
{
    public class CTGTestMethodBusiness : ICTGTestMethodBusiness
    {
        private IMapper _mapper;
        private ICTGTestMethodRepository _cTGTestMethodRepository;

        public CTGTestMethodBusiness(IMapper mapper,
            ICTGTestMethodRepository cTGTestMethodRepository)
        {
            _mapper = mapper;
            _cTGTestMethodRepository = cTGTestMethodRepository;
        }

        public Task<CTGTestMethodModel> CreateAsync(CTGTestMethodModel model, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<CTGTestMethodEntity>(model);

            var createResult = _cTGTestMethodRepository.Create(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _cTGTestMethodRepository.SaveChange();

            var result = _mapper.Map<CTGTestMethodModel>(createResult);

            return Task.FromResult(result);
        }

        public Task<bool> CanBeDeleteAsync(long id)
        {
            return Task.FromResult(!_cTGTestMethodRepository.FindSingle(w => w.Id == id, w => w.IDTRTestPropertyEntities).IDTRTestPropertyEntities.Any());
        }

        public Task DeleteAsync(CTGTestMethodModel model, CancellationToken cancellationToken = default)
        {
            var entity = _cTGTestMethodRepository.FindSingle(x => x.Id == model.Id);
            entity.IsDeleted = true;
            entity.DeletedBy = model.DeletedBy;
            entity.DeletedTime = model.DeletedTime;

            _cTGTestMethodRepository.Update(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _cTGTestMethodRepository.SaveChange();

            return Task.CompletedTask;
        }

        public Task<IEnumerable<CTGTestMethodModel>> GetAllAsync()
        {
            var query = _cTGTestMethodRepository.Find(w => w.IsDeleted == false || w.IsDeleted == null);

            var result = _mapper.Map<IEnumerable<CTGTestMethodModel>>(query);

            return Task.FromResult(result);
        }

        public Task<CTGTestMethodModel> GetByIdAsync(long id)
        {
            var query = _cTGTestMethodRepository.FindSingle(w => w.Id == id && (w.IsDeleted == false || w.IsDeleted == null));

            var result = _mapper.Map<CTGTestMethodModel>(query);

            return Task.FromResult(result);
        }

        public Task<CTGTestMethodModel> GetByNameAsync(string name, long propertyId)
        {
            var query = _cTGTestMethodRepository.FindSingle(w => w.Name == name && w.TestPropertyId == propertyId && (w.IsDeleted == false || w.IsDeleted == null));

            var result = _mapper.Map<CTGTestMethodModel>(query);

            return Task.FromResult(result);
        }

        public Task<PagedResult<CTGTestMethodModel>> GetPagedAsync(int pageIndex, int pageSize, string searchString = null)
        {
            var query = _cTGTestMethodRepository.Find(w => w.IsDeleted == false || w.IsDeleted == null, w => w.CTGTestPropertyEntity, w => w.CTGTestPropertyEntity.CTGTestObjectEntity);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(w => w.Name.ToLower().Contains(searchString.Trim().ToLower()) || w.CTGTestPropertyEntity.Name.ToLower().Contains(searchString.Trim().ToLower()));
            }
            var totalRow = query.Count();

            query = query.OrderBy(w => w.Name).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            var result = _mapper.Map<IList<CTGTestMethodModel>>(query);

            return Task.FromResult(new PagedResult<CTGTestMethodModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                RowCount = totalRow,
                Results = result,
            });
        }

        public Task UpdateAsync(CTGTestMethodModel model, CancellationToken cancellationToken = default)
        {
            var entity = _cTGTestMethodRepository.FindSingle(x => x.Id == model.Id);
            entity.Name = model.Name;
            entity.SymbolAttached = model.SymbolAttached;
            entity.TestPropertyId = model.TestPropertyId;
            entity.Note = model.Note;
            entity.ModifiedBy = model.ModifiedBy;
            entity.ModifiedTime = model.ModifiedTime;

            _cTGTestMethodRepository.Update(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _cTGTestMethodRepository.SaveChange();

            return Task.CompletedTask;
        }
    }
}