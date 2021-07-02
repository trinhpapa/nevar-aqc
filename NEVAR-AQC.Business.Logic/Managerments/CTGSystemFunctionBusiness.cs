using AutoMapper;
using NEVAR_AQC.Business.Managerment;
using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Data.Managements;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.Logic.Managerments
{
    public class CTGSystemFunctionBusiness : ICTGSystemFunctionBusiness
    {
        private IMapper _mapper;
        private ICTGSystemFunctionRepository _cTGSystemFunctionRepository;

        public CTGSystemFunctionBusiness(IMapper mapper,
            ICTGSystemFunctionRepository cTGSystemFunctionRepository)
        {
            _mapper = mapper;
            _cTGSystemFunctionRepository = cTGSystemFunctionRepository;
        }

        public Task<IEnumerable<CTGSystemFunctionModel>> GetAllAsync()
        {
            var data = _cTGSystemFunctionRepository.Find(w => w.IsDeleted == null || w.IsDeleted == false).OrderBy(w => w.Key);
            var result = _mapper.Map<IEnumerable<CTGSystemFunctionModel>>(data);
            return Task.FromResult(result);
        }

        public Task<List<int>> GetKeyById(List<long> functionId)
        {
            var data = _cTGSystemFunctionRepository
                .Find(x => functionId.Contains(x.Id))
                .Select(w => w.Key)
                .ToList();
            return Task.FromResult(data);
        }
    }
}