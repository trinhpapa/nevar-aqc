using AutoMapper;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.ReceptionDepartment;

namespace NEVAR_AQC.Mapper.ReceptionDepartment
{
    public class IDTestRequirementProfile : Profile
    {
        public IDTestRequirementProfile()
        {
            CreateMap<IDTestRequirementCreateModel, IDTestRequirementEntity>();
            CreateMap<IDTestRequirementUpdateModel, IDTestRequirementEntity>();
            CreateMap<IDTestRequirementEntity, IDTestRequirementViewModel>();
            CreateMap<IDTestRequirementEntity, IDTestRequirementModel>();
        }
    }
}