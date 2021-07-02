using AutoMapper;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.TestDepartment;
using System;
using System.Collections.Generic;
using System.Text;

namespace NEVAR_AQC.Mapper.ReceptionDepartment
{
    public class IDTRTestProcessVolumeMethodProfile : Profile
    {
        public IDTRTestProcessVolumeMethodProfile()
        {
            CreateMap<IDTRTestProcessVolumeMethodModel, IDTRTestProcessVolumeMethodEntity>();
            CreateMap<IDTRTestProcessVolumeMethodEntity, IDTRTestProcessVolumeMethodModel>();
        }
    }
}
