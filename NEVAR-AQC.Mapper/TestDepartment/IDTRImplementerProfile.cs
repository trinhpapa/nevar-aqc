using AutoMapper;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.TestDepartment;

namespace NEVAR_AQC.Mapper.TestDepartment
{
    public class IDTRImplementerProfile : Profile
    {
        public IDTRImplementerProfile()
        {
            CreateMap<IDTRImplementerEntity, IDTRImplementerModel>();
            CreateMap<IDTRImplementerModel, IDTRImplementerEntity>();
        }
    }
}