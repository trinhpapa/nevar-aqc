using AutoMapper;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.TestDepartment;

namespace NEVAR_AQC.Mapper.TestDepartment
{
    public class IDTRTestPropertyProfile : Profile
    {
        public IDTRTestPropertyProfile()
        {
            CreateMap<IDTRTestPropertyEntity, IDTRTestPropertyModel>();
            CreateMap<IDTRTestPropertyModel, IDTRTestPropertyEntity>();
        }
    }
}