using AutoMapper;
using NEVAR_AQC.Business.Managerment;
using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Service.Managements;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NEVAR_AQC.Service.Facade.Managements
{
    public class CTGRequirementTypeService : ICTGRequirementTypeService
    {
        private IMapper _mapper;
        private ICTGRequirementTypeBusiness _requirementTypeBusiness;

        public CTGRequirementTypeService(IMapper mapper,
             ICTGRequirementTypeBusiness requirementTypeBusiness)
        {
            _mapper = mapper;
            _requirementTypeBusiness = requirementTypeBusiness;
        }

        public async Task<IEnumerable<CTGRequirementTypeViewModel>> GetAllAsync()
        {
            return await _requirementTypeBusiness.GetAllAsync();
        }
    }
}