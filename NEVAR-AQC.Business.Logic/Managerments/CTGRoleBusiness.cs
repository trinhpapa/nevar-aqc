// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions </Copyright>
//     <Url> http://lehoangtrinh.com </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Business.Logic </Project>
//     <File>
//         <Name> CTGRoleBusiness.cs </Name>
//         <Created> 8/4/2019 - 08:35 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//
//     </Summary>
// <License>

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
    public class CTGRoleBusiness : ICTGRoleBusiness
    {
        private IMapper _mapper;
        private ICTGRoleRepository _roleRepository;

        public CTGRoleBusiness(IMapper mapper,
            ICTGRoleRepository roleRepository)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        public Task<IEnumerable<Core.Models.Managements.CTGRoleModel>> GetAllAsync()
        {
            var query = _roleRepository.Find(null);
            var result = _mapper.Map<IEnumerable<Core.Models.Managements.CTGRoleModel>>(query);
            return Task.FromResult(result);
        }

        public Task<Core.Models.Managements.CTGRoleModel> CreateAsync(Core.Models.Managements.CTGRoleModel model, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<Core.Entities.CTGRoleEntity>(model);

            var createResult = _roleRepository.Create(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _roleRepository.SaveChange();

            var result = _mapper.Map<Core.Models.Managements.CTGRoleModel>(createResult);

            return Task.FromResult(result);
        }

        public Task<bool> CanBeDeleteAsync(int id)
        {
            return Task.FromResult(!_roleRepository.FindSingle(w => w.Id == id, w => w.SYSUserEntities)
                .SYSUserEntities.Any());
        }

        public Task DeleteAsync(Core.Models.Managements.CTGRoleModel model, CancellationToken cancellationToken = default)
        {
            var entity = _roleRepository.FindSingle(x => x.Id == model.Id);
            entity.IsDeleted = true;
            entity.DeletedBy = model.DeletedBy;
            entity.DeletedTime = model.DeletedTime;

            _roleRepository.Update(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _roleRepository.SaveChange();

            return Task.CompletedTask;
        }

        public Task<Core.Models.Managements.CTGRoleModel> GetByIdAsync(int id)
        {
            var query = _roleRepository.FindSingle(w => w.Id == id && (w.IsDeleted == false || w.IsDeleted == null), w => w.SYSRoleFunctionEntities);

            var result = _mapper.Map<Core.Models.Managements.CTGRoleModel>(query);

            return Task.FromResult(result);
        }

        public Task<PagedResult<Core.Models.Managements.CTGRoleModel>> GetPagedAsync(int pageIndex, int pageSize, string searchString = null)
        {
            var query = _roleRepository.Find(w => w.IsDeleted == false || w.IsDeleted == null);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(w => w.Name.ToLower().Contains(searchString.ToLower()));
            }
            var totalRow = query.Count();

            query = query.OrderBy(w => w.Name).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            var result = _mapper.Map<IList<Core.Models.Managements.CTGRoleModel>>(query);

            return Task.FromResult(new PagedResult<Core.Models.Managements.CTGRoleModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                RowCount = totalRow,
                Results = result,
            });
        }

        public Task UpdateAsync(CTGRoleModel model, CancellationToken cancellationToken = default)
        {
            var entity = _roleRepository.FindSingle(x => x.Id == model.Id);
            entity.Name = model.Name;
            entity.SYSRoleFunctionEntities = _mapper.Map<ICollection<SYSRoleFunctionEntity>>(model.SYSRoleFunctionEntities);
            entity.Note = model.Note;
            entity.ModifiedBy = model.ModifiedBy;
            entity.ModifiedTime = model.ModifiedTime;

            _roleRepository.Update(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _roleRepository.SaveChange();

            return Task.CompletedTask;
        }
    }
}