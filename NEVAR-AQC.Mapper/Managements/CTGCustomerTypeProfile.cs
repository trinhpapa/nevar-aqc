using AutoMapper;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.Managements;

namespace NEVAR_AQC.Mapper.Managements
{
    public class CTGCustomerTypeProfile : Profile
    {
        public CTGCustomerTypeProfile()
        {
            CreateMap<CTGCustomerTypeEntity, CTGCustomerTypeModel>();
            CreateMap<CTGCustomerTypeModel, CTGCustomerTypeEntity>();
        }
    }
}