using AutoMapper;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.System;

namespace NEVAR_AQC.Mapper.System
{
    public class LOGHandleProfile : Profile
    {
        public LOGHandleProfile()
        {
            CreateMap<LOGHandleEntity, LOGHandleModel>();
            CreateMap<LOGHandleModel, LOGHandleEntity>();
        }
    }
}