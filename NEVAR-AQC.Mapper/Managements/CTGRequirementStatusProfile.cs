using AutoMapper;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.Managements;
using System;
using System.Collections.Generic;
using System.Text;

namespace NEVAR_AQC.Mapper.Managements
{
    public class CTGRequirementStatusProfile : Profile
    {
        public CTGRequirementStatusProfile()
        {
            CreateMap<CTGRequirementStatusModel, CTGRequirementStatusEntity>();
            CreateMap<CTGRequirementStatusEntity, CTGRequirementStatusModel>();
        }
    }
}
