using AutoMapper;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.Managements;

namespace NEVAR_AQC.Mapper.Managements
{
    public class CTGTestObjectProfile : Profile
    {
        public CTGTestObjectProfile()
        {
            CreateMap<CTGTestObjectModel, CTGTestObjectEntity>();
            CreateMap<CTGTestObjectEntity, CTGTestObjectModel>();
        }
    }
}