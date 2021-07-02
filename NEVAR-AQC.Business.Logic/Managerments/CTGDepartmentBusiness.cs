#region	License
// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions</Copyright>
//     <Url> https://github.com/trinhpapa </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Business.Logic </Project>
//     <File>
//         <Name> DepartmentBusiness.cs </Name>
//         <Created> 7/4/2019 - 20:27:29 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//         DepartmentBusiness.cs
//     </Summary>
// <License>
#endregion License

using AutoMapper;
using NEVAR_AQC.Business.Managerment;
using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Data.Managements;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.Logic.Managerments
{
    public class CTGDepartmentBusiness : ICTGDepartmentBusiness
    {
        private IMapper _mapper;
        private ICTGDepartmentRepository _departmentRepository;

        public CTGDepartmentBusiness(IMapper mapper,
            ICTGDepartmentRepository departmentRepository)
        {
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }

        public Task<IEnumerable<CTGDepartmentViewModel>> GetAllAsync()
        {
            var data = _departmentRepository.Find(x => x.IsDeleted == false);
            var result = _mapper.Map<IEnumerable<CTGDepartmentViewModel>>(data);
            return Task.FromResult(result);
        }
    }
}