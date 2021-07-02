using AutoMapper;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.Managements;

namespace NEVAR_AQC.Mapper.Managements
{
    public class CTGDepartmentProfile : Profile
    {
        public CTGDepartmentProfile()
        {
            CreateMap<CTGDepartmentEntity, CTGDepartmentModel>();
            CreateMap<CTGDepartmentEntity, CTGDepartmentViewModel>();
            CreateMap<CTGDepartmentCreateModel, CTGDepartmentEntity>();
            CreateMap<CTGDepartmentUpdateModel, CTGDepartmentEntity>();
        }
    }
}