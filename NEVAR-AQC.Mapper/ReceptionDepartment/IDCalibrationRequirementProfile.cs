using AutoMapper;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.ReceptionDepartment;

namespace NEVAR_AQC.Mapper.ReceptionDepartment
{
    public class IDCalibrationRequirementProfile : Profile
    {
        public IDCalibrationRequirementProfile()
        {
            CreateMap<IDCalibrationRequirementModel, IDCalibrationRequirementEntity>();
            CreateMap<IDCalibrationRequirementEntity, IDCalibrationRequirementModel>();
        }
    }
}