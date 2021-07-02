using AutoMapper;
using NEVAR_AQC.Business.User;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.User;
using NEVAR_AQC.Core.PagingHelper;
using NEVAR_AQC.Data.User;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.Logic.User
{
    public class SYSUserBusiness : ISYSUserBusiness
    {
        private IMapper _mapper;
        private ISYSUserRepository _userRepository;

        public SYSUserBusiness(IMapper mapper,
            ISYSUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public Task<PagedResult<SYSUserModel>> GetPagingAsync(int pageIndex, int pageSize, string searchString)
        {
            var query = _userRepository.Find(null, x => x.CTGRoleEntity, x => x.CTGDepartmentEntity);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(w => w.Username.Contains(searchString) || w.DisplayName.Contains(searchString));
            }

            var totalRow = query.Count();

            query = query.OrderBy(w => w.Username).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            var data = _mapper.Map<IList<SYSUserModel>>(query);

            return Task.FromResult(new PagedResult<SYSUserModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            });
        }

        public Task<IEnumerable<SYSUserModel>> GetAllAsync(string username, string searchName)
        {
            var query = _userRepository.Find(x => x.ActiveStatus == true && (x.IsDeleted == false || x.IsDeleted == null));

            if (!string.IsNullOrEmpty(username))
            {
                query = query.Where(w => w.Username.Contains(username));
            }

            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(w => w.DisplayName.Contains(searchName));
            }

            query = query.OrderBy(w => w.Username);

            return Task.FromResult(_mapper.Map<IEnumerable<SYSUserModel>>(query));
        }

        public Task<SYSUserModel> GetByUsernameAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return null;
            }

            var query = _userRepository.FindSingle(x => x.Username == username && (x.IsDeleted == false || x.IsDeleted == null));

            var result = _mapper.Map<SYSUserModel>(query);

            return Task.FromResult(result);
        }

        public Task<SYSUserViewModel> GetByIdAsync(long id)
        {
            var query = _userRepository.FindSingle(x => x.Id == id);

            var result = _mapper.Map<SYSUserViewModel>(query);

            return Task.FromResult(result);
        }

        public Task<SYSUserViewModel> CreateAsync(SYSUserCreateModel model, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<SYSUserEntity>(model);

            var createdResult = _userRepository.Create(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _userRepository.SaveChange();

            var result = _mapper.Map<SYSUserViewModel>(createdResult);

            return Task.FromResult(result);
        }

        public Task UpdateAsync(SYSUserUpdateModel model, CancellationToken cancellationToken = default)
        {
            var entity = _userRepository.FindSingle(x => x.Id == model.Id);
            entity.Username = model.Username;
            entity.DisplayName = model.DisplayName;
            entity.ActiveStatus = model.ActiveStatus;
            entity.ActiveStatus = model.ActiveStatus;
            entity.DepartmentId = model.DepartmentId;
            entity.RoleId = model.RoleId;
            entity.Note = model.Note;

            _userRepository.Update(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _userRepository.SaveChange();

            return Task.CompletedTask;
        }

        public Task UpdatePasswordAsync(SYSUserUpdateModel model, CancellationToken cancellationToken = default)
        {
            var entity = _userRepository.FindSingle(x => x.Id == model.Id);
            entity.PasswordSalt = model.PasswordSalt;
            entity.PasswordEncrypted = model.PasswordEncrypted;

            _userRepository.Update(entity);

            cancellationToken.ThrowIfCancellationRequested();

            _userRepository.SaveChange();

            return Task.CompletedTask;
        }
    }
}