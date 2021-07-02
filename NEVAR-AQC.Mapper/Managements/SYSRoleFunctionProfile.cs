using AutoMapper;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.Managements;

namespace NEVAR_AQC.Mapper.Managements
{
    public class SYSRoleFunctionProfile : Profile
    {
        public SYSRoleFunctionProfile()
        {
            CreateMap<SYSRoleFunctionCreateModel, SYSRoleFunctionEntity>();
            CreateMap<SYSRoleFunctionUpdateModel, SYSRoleFunctionEntity>();
            CreateMap<SYSRoleFunctionEntity, SYSRoleFunctionViewModel>();
            CreateMap<SYSRoleFunctionEntity, SYSRoleFunctionModel>();
            CreateMap<SYSRoleFunctionModel, SYSRoleFunctionEntity>();
        }
    }
}