using AutoMapper;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.TestDepartment;
using System;
using System.Collections.Generic;
using System.Text;

namespace NEVAR_AQC.Mapper.ReceptionDepartment
{
    public class IDTRTestProcessOtherMethodProfile : Profile
    {
        public IDTRTestProcessOtherMethodProfile()
        {
            CreateMap<IDTRTestProcessOtherMethodModel, IDTRTestProcessOtherMethodEntity>();
            CreateMap<IDTRTestProcessOtherMethodEntity, IDTRTestProcessOtherMethodModel>();
        }
    }
}
