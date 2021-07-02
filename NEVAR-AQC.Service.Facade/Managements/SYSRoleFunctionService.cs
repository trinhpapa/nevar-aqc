// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions </Copyright>
//     <Url> http://lehoangtrinh.com </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Service.Facade </Project>
//     <File>
//         <Name> SYSRoleFunctionService.cs </Name>
//         <Created> 6/4/2019 - 20:27 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//
//     </Summary>
// <License>

using AutoMapper;
using NEVAR_AQC.Business.Managerment;
using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Service.Managements;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NEVAR_AQC.Service.Facade.Managements
{
    public class SYSRoleFunctionService : ISYSRoleFunctionService
    {
        private IMapper _mapper;
        private ISYSRoleFunctionBusiness _roleFunctionBusiness;

        public SYSRoleFunctionService(IMapper mapper,
            ISYSRoleFunctionBusiness roleFunctionBusiness)
        {
            _mapper = mapper;
            _roleFunctionBusiness = roleFunctionBusiness;
        }

        public async Task<IEnumerable<SYSRoleFunctionViewModel>> GetAllAsync()
        {
            return await _roleFunctionBusiness.GetAllAsync();
        }

        public async Task<IEnumerable<SYSRoleFunctionViewModel>> GetByRole(int roleId)
        {
            return await _roleFunctionBusiness.GetByRole(roleId);
        }
    }
}