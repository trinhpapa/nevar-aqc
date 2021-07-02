// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions </Copyright>
//     <Url> http://lehoangtrinh.com </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Business.Logic </Project>
//     <File>
//         <Name> SYSRoleFunctionBusiness.cs </Name>
//         <Created> 7/4/2019 - 20:46 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//
//     </Summary>
// <License>

using AutoMapper;
using NEVAR_AQC.Business.Managerment;
using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Data.Managements;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.Logic.Managerments
{
    public class SYSRoleFunctionBusiness : ISYSRoleFunctionBusiness
    {
        private IMapper _mapper;
        private ISYSRoleFunctionrepository _roleFunctionRepository;

        public SYSRoleFunctionBusiness(IMapper mapper, ISYSRoleFunctionrepository roleFunctionRepository)
        {
            _mapper = mapper;
            _roleFunctionRepository = roleFunctionRepository;
        }

        public Task<IEnumerable<SYSRoleFunctionViewModel>> GetAllAsync()
        {
            var data = _roleFunctionRepository.Find(w => w.IsDeleted == null || w.IsDeleted == false);
            var result = _mapper.Map<IEnumerable<SYSRoleFunctionViewModel>>(data);
            return Task.FromResult(result);
        }

        public Task<IEnumerable<SYSRoleFunctionViewModel>> GetByRole(int roleId)
        {
            var data = _roleFunctionRepository.Find(w => w.RoleId == roleId);
            var result = _mapper.Map<IEnumerable<SYSRoleFunctionViewModel>>(data);
            return Task.FromResult(result);
        }

        public Task<List<long>> GetFunctionIdByRole(int roleId)
        {
            var data = _roleFunctionRepository
                .Find(w => w.RoleId == roleId)
                .Select(x => x.FunctionId)
                .ToList();
            return Task.FromResult(data);
        }

        public void DeleteByRoleAsync(int roleId)
        {
            var entities = _roleFunctionRepository.Find(w => w.RoleId == roleId).ToList();

            foreach (var itemEntity in entities)
            {
                _roleFunctionRepository.Delete(itemEntity);
            }

            _roleFunctionRepository.SaveChange();
        }
    }
}