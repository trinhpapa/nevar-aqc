using AutoMapper;
using NEVAR_AQC.Business.Managerment;
using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Data.Managements;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.Logic.Managerments
{
   public class CTGRequirementTypeBusiness : ICTGRequirementTypeBusiness
   {
      private IMapper _mapper;
      private ICTGRequirementTypeRepository _requirementTypeRepository;

      public CTGRequirementTypeBusiness(IMapper mapper,
          ICTGRequirementTypeRepository requirementTypeRepository)
      {
         _mapper = mapper;
         _requirementTypeRepository = requirementTypeRepository;
      }

      public Task<IEnumerable<CTGRequirementTypeViewModel>> GetAllAsync()
      {
         var requirementTypes = _requirementTypeRepository.Find(x => x.IsDeleted == false).OrderBy(x => x.Id);
         var result = _mapper.Map<IEnumerable<CTGRequirementTypeViewModel>>(requirementTypes);
         return Task.FromResult(result);
      }

      public Task<CTGRequirementTypeViewModel> GetByIdAsync(int id)
      {
         var requirementTypes = _requirementTypeRepository.FindSingle(x => x.Id == id && x.IsDeleted == false);
         var result = _mapper.Map<CTGRequirementTypeViewModel>(requirementTypes);
         return Task.FromResult(result);
      }
   }
}