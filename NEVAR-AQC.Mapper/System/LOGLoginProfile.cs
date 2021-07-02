using AutoMapper;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.System;

namespace NEVAR_AQC.Mapper.System
{
    public class LOGLoginProfile : Profile
    {
        public LOGLoginProfile()
        {
            CreateMap<LOGLoginEntity, LOGLoginModel>();
            CreateMap<LOGLoginModel, LOGLoginEntity>();
        }
    }
}