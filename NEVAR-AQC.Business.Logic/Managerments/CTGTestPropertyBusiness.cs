using AutoMapper;
using NEVAR_AQC.Business.Managerment;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Core.PagingHelper;
using NEVAR_AQC.Data.Managements;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.Logic.Managerments
{
    public class CTGTestPropertyBusiness : ICTGTestPropertyBusiness
    {
        private IMapper _mapper;
        private ICTGTestPropertyRepository _cTGTestPropertyRepository;

        public CTGTestPropertyBusiness(IMapper mapper,
            ICTGTestPropertyRepository cTGTestPropertyRepository)
        {
            _mapper = mapper;
            _cTGTestPropertyRepository = cTGTestPropertyRepository;
        }

        public Task<CTGTestPropertyModel> CreateAsync(CTGTestPropertyModel model, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<CTGTestPropertyEntity>(model);

            var createResult = _cTGTestPropertyRepository.Create(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _cTGTestPropertyRepository.SaveChange();

            var result = _mapper.Map<CTGTestPropertyModel>(createResult);

            return Task.FromResult(result);
        }

        public Task<bool> CanBeDeleteAsync(long id)
        {
            return Task.FromResult(!_cTGTestPropertyRepository.FindSingle(w => w.Id == id, w => w.IDTRTestPropertyEntities).IDTRTestPropertyEntities.Any());
        }

        public Task DeleteAsync(CTGTestPropertyModel model, CancellationToken cancellationToken = default)
        {
            var entity = _cTGTestPropertyRepository.FindSingle(x => x.Id == model.Id);
            entity.IsDeleted = true;
            entity.DeletedBy = model.DeletedBy;
            entity.DeletedTime = model.DeletedTime;

            _cTGTestPropertyRepository.Update(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _cTGTestPropertyRepository.SaveChange();

            return Task.CompletedTask;
        }

        public Task<IEnumerable<CTGTestPropertyModel>> GetAllAsync()
        {
            var query = _cTGTestPropertyRepository.Find(w => w.IsDeleted == false || w.IsDeleted == null);

            var result = _mapper.Map<IEnumerable<CTGTestPropertyModel>>(query);

            return Task.FromResult(result);
        }

        public Task<CTGTestPropertyModel> GetByIdAsync(long id)
        {
            var query = _cTGTestPropertyRepository.FindSingle(w => w.Id == id && (w.IsDeleted == false || w.IsDeleted == null));

            var result = _mapper.Map<CTGTestPropertyModel>(query);

            return Task.FromResult(result);
        }

        public Task<CTGTestPropertyModel> GetByNameAsync(string name, long objectId)
        {
            var query = _cTGTestPropertyRepository.FindSingle(w => w.Name == name && w.ObjectId == objectId && (w.IsDeleted == false || w.IsDeleted == null));

            var result = _mapper.Map<CTGTestPropertyModel>(query);

            return Task.FromResult(result);
        }

        public Task<PagedResult<CTGTestPropertyModel>> GetPagedAsync(int pageIndex, int pageSize, string searchString = null)
        {
            var query = _cTGTestPropertyRepository.Find(w => w.IsDeleted == false || w.IsDeleted == null, w => w.CTGTestObjectEntity);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(w => Regex.Replace(w.Name, @"(<.?su[bp]>)", "").ToLower().Contains(searchString.Trim().ToLower()) || w.CTGTestObjectEntity.Name.ToLower().Contains(searchString.Trim().ToLower()));
            }
            var totalRow = query.Count();

            query = query.OrderBy(w => w.Name).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            var result = _mapper.Map<IList<CTGTestPropertyModel>>(query);

            return Task.FromResult(new PagedResult<CTGTestPropertyModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                RowCount = totalRow,
                Results = result,
            });
        }

        public Task UpdateAsync(CTGTestPropertyModel model, CancellationToken cancellationToken = default)
        {
            var entity = _cTGTestPropertyRepository.FindSingle(x => x.Id == model.Id);
            entity.Name = model.Name;
            entity.Unit = model.Unit;
            entity.Note = model.Note;
            entity.ModifiedBy = model.ModifiedBy;
            entity.ModifiedTime = model.ModifiedTime;

            _cTGTestPropertyRepository.Update(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _cTGTestPropertyRepository.SaveChange();

            return Task.CompletedTask;
        }

        public Task<IEnumerable<CTGTestPropertyModel>> GetByObjectIdAsync(long objectId)
        {
            var query = _cTGTestPropertyRepository.Find(w => w.ObjectId == objectId && (w.IsDeleted == false || w.IsDeleted == null));
            var result = _mapper.Map<IEnumerable<CTGTestPropertyModel>>(query);
            return Task.FromResult(result);
        }
    }
}