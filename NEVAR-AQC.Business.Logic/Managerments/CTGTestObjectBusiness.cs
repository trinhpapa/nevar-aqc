#region	License
// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions</Copyright>
//     <Url> https://github.com/trinhpapa </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Business.Logic </Project>
//     <File>
//         <Name> CTGTestObjectBusiness.cs </Name>
//         <Created> 14/5/2019 - 08:34 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//         CTGTestObjectBusiness.cs
//     </Summary>
// <License>
#endregion License

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
    public class CTGTestObjectBusiness : ICTGTestObjectBusiness
    {
        private IMapper _mapper;
        private ICTGTestObjectRepository _cTGTestObjectRepository;

        public CTGTestObjectBusiness(IMapper mapper,
            ICTGTestObjectRepository cTGTestObjectRepository)
        {
            _mapper = mapper;
            _cTGTestObjectRepository = cTGTestObjectRepository;
        }

        public Task<bool> CanBeDeleteAsync(long id)
        {
            return Task.FromResult(!_cTGTestObjectRepository.FindSingle(w => w.Id == id, w => w.IDTestRequirementEntities).IDTestRequirementEntities.Any());
        }

        public Task<CTGTestObjectModel> CreateAsync(CTGTestObjectModel model, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<CTGTestObjectEntity>(model);

            var createResult = _cTGTestObjectRepository.Create(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _cTGTestObjectRepository.SaveChange();

            var result = _mapper.Map<CTGTestObjectModel>(createResult);

            return Task.FromResult(result);
        }

        public Task DeleteAsync(CTGTestObjectModel model, CancellationToken cancellationToken = default)
        {
            var entity = _cTGTestObjectRepository.FindSingle(x => x.Id == model.Id);
            entity.IsDeleted = true;
            entity.DeletedBy = model.DeletedBy;
            entity.DeletedTime = model.DeletedTime;

            _cTGTestObjectRepository.Update(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _cTGTestObjectRepository.SaveChange();

            return Task.CompletedTask;
        }

        public Task<IEnumerable<CTGTestObjectModel>> GetAllAsync()
        {
            var query = _cTGTestObjectRepository.Find(w => w.IsDeleted == false || w.IsDeleted == null);

            var result = _mapper.Map<IEnumerable<CTGTestObjectModel>>(query);

            return Task.FromResult(result);
        }

        public Task<CTGTestObjectModel> GetByIdAsync(long id)
        {
            var query = _cTGTestObjectRepository.FindSingle(w => w.Id == id && (w.IsDeleted == false || w.IsDeleted == null));

            var result = _mapper.Map<CTGTestObjectModel>(query);

            return Task.FromResult(result);
        }

        public Task<CTGTestObjectModel> GetByNameAsync(string name)
        {
            var query = _cTGTestObjectRepository.FindSingle(w => w.Name == name && (w.IsDeleted == false || w.IsDeleted == null));

            var result = _mapper.Map<CTGTestObjectModel>(query);

            return Task.FromResult(result);
        }

        public Task<PagedResult<CTGTestObjectModel>> GetPagedAsync(int pageIndex, int pageSize, string searchString = null)
        {
            var query = _cTGTestObjectRepository.Find(w => w.IsDeleted == false || w.IsDeleted == null, w => w.CTGFieldEntity);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(w => w.Name.ToLower().Contains(searchString.Trim().ToLower()) || w.CTGFieldEntity.Name.ToLower().Contains(searchString.Trim().ToLower()));
            }
            var totalRow = query.Count();

            query = query.OrderBy(w => w.Name).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            var result = _mapper.Map<IList<CTGTestObjectModel>>(query);

            return Task.FromResult(new PagedResult<CTGTestObjectModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                RowCount = totalRow,
                Results = result,
            });
        }

        public Task UpdateAsync(CTGTestObjectModel model, CancellationToken cancellationToken = default)
        {
            var entity = _cTGTestObjectRepository.FindSingle(x => x.Id == model.Id);
            entity.Name = model.Name;
            entity.Note = model.Note;
            entity.ModifiedBy = model.ModifiedBy;
            entity.ModifiedTime = model.ModifiedTime;

            _cTGTestObjectRepository.Update(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _cTGTestObjectRepository.SaveChange();

            return Task.CompletedTask;
        }
    }
}