using AutoMapper;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.Managements;

namespace NEVAR_AQC.Mapper.Managements
{
    public class CTGTestPropertyProfile : Profile
    {
        public CTGTestPropertyProfile()
        {
            CreateMap<CTGTestPropertyModel, CTGTestPropertyEntity>();
            CreateMap<CTGTestPropertyEntity, CTGTestPropertyModel>();
        }
    }
}