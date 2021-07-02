using AutoMapper;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.Managements;

namespace NEVAR_AQC.Mapper.Managements
{
    public class CTGSystemFunctionProfile : Profile
    {
        public CTGSystemFunctionProfile()
        {
            CreateMap<CTGSystemFunctionEntity, CTGSystemFunctionModel>();
            CreateMap<CTGSystemFunctionModel, CTGSystemFunctionEntity>();
        }
    }
}