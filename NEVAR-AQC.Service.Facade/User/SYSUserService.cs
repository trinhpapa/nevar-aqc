// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions </Copyright>
//     <Url> http://lehoangtrinh.com </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Service.Facade </Project>
//     <File>
//         <Name> SYSUserService.cs </Name>
//         <Created> 9/5/2019 - 13:58 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//
//     </Summary>
// <License>

using AutoMapper;
using NEVAR_AQC.Business.Managerment;
using NEVAR_AQC.Business.User;
using NEVAR_AQC.Core.Models.User;
using NEVAR_AQC.Core.PagingHelper;
using NEVAR_AQC.Core.StringHelper;
using NEVAR_AQC.Service.User;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NEVAR_AQC.Service.Facade.User
{
    public class SYSUserService : ISYSUserService
    {
        private readonly IMapper _mapper;
        private readonly ISYSUserBusiness _userBusiness;
        private readonly ISYSRoleFunctionBusiness _roleFunctionBusiness;
        private readonly ICTGSystemFunctionBusiness _cTgSystemFunctionBusiness;

        public SYSUserService(IMapper mapper,
            ISYSUserBusiness userBusiness,
            ISYSRoleFunctionBusiness roleFunctionBusiness,
            ICTGSystemFunctionBusiness cTgSystemFunctionBusiness)
        {
            _mapper = mapper;
            _userBusiness = userBusiness;
            _roleFunctionBusiness = roleFunctionBusiness;
            _cTgSystemFunctionBusiness = cTgSystemFunctionBusiness;
        }

        public async Task<SYSUserViewModel> CreateAsync(SYSUserCreateModel model, CancellationToken cancellationToken = default)
        {
            var checkUsername = await _userBusiness.GetByUsernameAsync(model.Username);

            if (checkUsername != null)
            {
                throw new Exception("Tên đăng nhập " + model.Username + " đã tồn tại");
            }

            model.CreatedTime = DateTime.Now;

            model.PasswordSalt = PasswordEncryption.GeneratePasswordKey();

            model.PasswordEncrypted = PasswordEncryption.EncryptionPasswordWithKey(model.PasswordOrigin, model.PasswordSalt);

            return await _userBusiness.CreateAsync(model, cancellationToken);
        }

        public async Task<IEnumerable<SYSUserModel>> GetAllAsync(string username, string searchString)
        {
            return await _userBusiness.GetAllAsync(username, searchString);
        }

        public async Task<SYSUserViewModel> GetByIdAsync(long id)
        {
            return await _userBusiness.GetByIdAsync(id);
        }

        public async Task<PagedResult<SYSUserModel>> GetPagingAsync(int pageIndex, int pageSize, string searchString)
        {
            return await _userBusiness.GetPagingAsync(pageIndex, pageSize, searchString);
        }

        public async Task<UserSessionModel> LoginByCredential(UserLoginModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.PasswordOrigin))
            {
                return null;
            }

            var getUserResult = await _userBusiness.GetByUsernameAsync(model.Username);

            if (getUserResult == null)
            {
                return null;
            }

            if (getUserResult.ActiveStatus == false)
            {
                throw new Exception("Tài khoản " + model.Username + " đã bị khóa!");
            }

            var passwordEncripted = PasswordEncryption.EncryptionPasswordWithKey(model.PasswordOrigin, getUserResult.PasswordSalt);

            if (getUserResult.PasswordEncrypted == passwordEncripted)
            {
                var functionIds = await _roleFunctionBusiness.GetFunctionIdByRole(getUserResult.RoleId);
                var functionKeys = await _cTgSystemFunctionBusiness.GetKeyById(functionIds);
                getUserResult.FunctionKeys = functionKeys;
                return _mapper.Map<UserSessionModel>(getUserResult);
            }
            else
            {
                return null;
            }
        }

        public async Task UpdateAsync(SYSUserUpdateModel model, CancellationToken cancellationToken = default)
        {
            var checkUsername = await _userBusiness.GetByUsernameAsync(model.Username);

            if (checkUsername != null && checkUsername.Id != model.Id)
            {
                throw new Exception("Tên đăng nhập " + model.Username + " đã tồn tại");
            }

            model.ModifiedTime = DateTime.Now;

            await _userBusiness.UpdateAsync(model, cancellationToken);
        }

        public async Task UpdatePasswordAsync(SYSUserUpdateModel model, CancellationToken cancellationToken = default)
        {
            if (model.PasswordOld != null)
            {
                var user = await _userBusiness.GetByUsernameAsync(model.Username);
                if (user == null)
                {
                    throw new Exception("Không tồn tại tài khoản " + model.Username);
                }

                var passwordOld = PasswordEncryption.EncryptionPasswordWithKey(model.PasswordOld.Trim(), user.PasswordSalt);

                model.Id = user.Id;

                if (user.PasswordEncrypted != passwordOld)
                {
                    throw new Exception("Mật khẩu cũ không đúng!");
                }
            }

            model.PasswordSalt = PasswordEncryption.GeneratePasswordKey();

            model.PasswordEncrypted = PasswordEncryption.EncryptionPasswordWithKey(model.PasswordOrigin, model.PasswordSalt);

            await _userBusiness.UpdatePasswordAsync(model, cancellationToken);
        }
    }
}