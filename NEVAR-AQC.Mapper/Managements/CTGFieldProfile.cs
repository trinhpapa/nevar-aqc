using AutoMapper;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.Managements;

namespace NEVAR_AQC.Mapper.Managements
{
    public class CTGFieldProfile : Profile
    {
        public CTGFieldProfile()
        {
            CreateMap<CTGFieldEntity, CTGFieldModel>();
            CreateMap<CTGFieldModel, CTGFieldEntity>();
        }
    }
}