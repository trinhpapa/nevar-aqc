using AutoMapper;
using NEVAR_AQC.Business.Managerment;
using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Data.Managements;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NEVAR_AQC.Business.Logic.Managerments
{
    public class CTGReturnInvoiceResultTypeBusiness : ICTGReturnInvoiceResultTypeBusiness
    {
        private readonly IMapper _mapper;
        private readonly ICTGReturnInvoiceResultTypeRepository _cTGReturnInvoiceResultTypeRepository;

        public CTGReturnInvoiceResultTypeBusiness(IMapper mapper,
            ICTGReturnInvoiceResultTypeRepository cTGReturnInvoiceResultTypeRepository)
        {
            _mapper = mapper;
            _cTGReturnInvoiceResultTypeRepository = cTGReturnInvoiceResultTypeRepository;
        }

        public Task<IEnumerable<CTGReturnInvoiceResultTypeModel>> GetStatusOnAsync()
        {
            var query = _cTGReturnInvoiceResultTypeRepository.Find(w => w.Status == true && (w.IsDeleted == null || w.IsDeleted == false));
            var result = _mapper.Map<IEnumerable<CTGReturnInvoiceResultTypeModel>>(query);
            return Task.FromResult(result);
        }
    }
}