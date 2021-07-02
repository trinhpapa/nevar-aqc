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
    public class CTGFieldBusiness : ICTGFieldBusiness
    {
        private IMapper _mapper;
        private ICTGFieldRepository _cTGFieldRepository;

        public CTGFieldBusiness(IMapper mapper,
            ICTGFieldRepository cTGFieldRepository)
        {
            _mapper = mapper;
            _cTGFieldRepository = cTGFieldRepository;
        }

        public Task<CTGFieldModel> CreateAsync(CTGFieldModel model, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<CTGFieldEntity>(model);

            var createResult = _cTGFieldRepository.Create(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _cTGFieldRepository.SaveChange();

            var result = _mapper.Map<CTGFieldModel>(createResult);

            return Task.FromResult(result);
        }

        public Task<bool> CanBeDeleteAsync(long id)
        {
            return Task.FromResult(!_cTGFieldRepository.FindSingle(w => w.Id == id, w => w.RequirementInvoiceEntities)
                .RequirementInvoiceEntities.Any());
        }

        public Task DeleteAsync(CTGFieldModel model, CancellationToken cancellationToken = default)
        {
            var entity = _cTGFieldRepository.FindSingle(x => x.Id == model.Id);
            entity.IsDeleted = true;
            entity.DeletedBy = model.DeletedBy;
            entity.DeletedTime = model.DeletedTime;

            _cTGFieldRepository.Update(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _cTGFieldRepository.SaveChange();

            return Task.CompletedTask;
        }

        public Task<IEnumerable<CTGFieldModel>> GetAllAsync()
        {
            var query = _cTGFieldRepository.Find(w => w.IsDeleted == false || w.IsDeleted == null);

            var result = _mapper.Map<IEnumerable<CTGFieldModel>>(query);

            return Task.FromResult(result);
        }

        public Task<CTGFieldModel> GetByIdAsync(long id)
        {
            var query = _cTGFieldRepository.FindSingle(w => w.Id == id && (w.IsDeleted == false || w.IsDeleted == null));

            var result = _mapper.Map<CTGFieldModel>(query);

            return Task.FromResult(result);
        }

        public Task<CTGFieldModel> GetBySymbolAsync(string symbol)
        {
            var query = _cTGFieldRepository.FindSingle(w => w.Symbol == symbol && (w.IsDeleted == false || w.IsDeleted == null));

            var result = _mapper.Map<CTGFieldModel>(query);

            return Task.FromResult(result);
        }

        public Task<PagedResult<CTGFieldModel>> GetPagedAsync(int pageIndex, int pageSize, string searchString = null)
        {
            var query = _cTGFieldRepository.Find(w => w.IsDeleted == false || w.IsDeleted == null);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(w => w.Name.Contains(searchString));
            }
            var totalRow = query.Count();

            query = query.OrderBy(w => w.Name).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            var result = _mapper.Map<IList<CTGFieldModel>>(query);

            return Task.FromResult(new PagedResult<CTGFieldModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                RowCount = totalRow,
                Results = result,
            });
        }

        public Task UpdateAsync(CTGFieldModel model, CancellationToken cancellationToken = default)
        {
            var entity = _cTGFieldRepository.FindSingle(x => x.Id == model.Id);
            entity.Name = model.Name;
            entity.Symbol = model.Symbol;
            entity.Note = model.Note;
            entity.ModifiedBy = model.ModifiedBy;
            entity.ModifiedTime = model.ModifiedTime;

            _cTGFieldRepository.Update(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _cTGFieldRepository.SaveChange();

            return Task.CompletedTask;
        }
    }
}