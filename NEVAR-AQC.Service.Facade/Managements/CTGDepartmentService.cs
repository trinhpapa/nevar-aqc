using AutoMapper;
using NEVAR_AQC.Business.Managerment;
using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Service.Managements;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NEVAR_AQC.Service.Facade.Managements
{
    public class CTGDepartmentService : ICTGDepartmentService
    {
        private IMapper _mapper;
        private ICTGDepartmentBusiness _departmentBusiness;

        public CTGDepartmentService(IMapper mapper, ICTGDepartmentBusiness departmentBusiness)
        {
            _mapper = mapper;
            _departmentBusiness = departmentBusiness;
        }

        public async Task<IEnumerable<CTGDepartmentViewModel>> GetAllAsync()
        {
            var data = await _departmentBusiness.GetAllAsync();
            var result = _mapper.Map<IEnumerable<CTGDepartmentViewModel>>(data);
            return await Task.FromResult(result);
        }
    }
}