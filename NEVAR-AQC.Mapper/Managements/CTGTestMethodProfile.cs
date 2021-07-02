using AutoMapper;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.Managements;

namespace NEVAR_AQC.Mapper.Managements
{
    public class CTGTestMethodProfile : Profile
    {
        public CTGTestMethodProfile()
        {
            CreateMap<CTGTestMethodModel, CTGTestMethodEntity>();
            CreateMap<CTGTestMethodEntity, CTGTestMethodModel>();
        }
    }
}