using AutoMapper;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.Managements;

namespace NEVAR_AQC.Mapper.Managements
{
    public class CTGReturnInvoiceResultTypeProfile : Profile
    {
        public CTGReturnInvoiceResultTypeProfile()
        {
            CreateMap<CTGReturnInvoiceResultTypeEntity, CTGReturnInvoiceResultTypeModel>();
            CreateMap<CTGReturnInvoiceResultTypeModel, CTGReturnInvoiceResultTypeEntity>();
        }
    }
}