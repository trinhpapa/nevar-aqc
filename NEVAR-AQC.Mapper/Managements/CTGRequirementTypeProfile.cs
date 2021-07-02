#region	License
// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions</Copyright>
//     <Url> https://github.com/trinhpapa </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Mapper </Project>
//     <File>
//         <Name> CTGRequirementTypeProfile.cs </Name>
//         <Created> 18/4/2019 - 17:21 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//         CTGRequirementTypeProfile.cs
//     </Summary>
// <License>
#endregion License

using AutoMapper;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.Managements;

namespace NEVAR_AQC.Mapper.Managements
{
    public class CTGRequirementTypeProfile : Profile
    {
        public CTGRequirementTypeProfile()
        {
            CreateMap<CTGRequirementTypeEntity, CTGRequirementTypeViewModel>();
            CreateMap<CTGRequirementTypeEntity, CTGRequirementTypeModel>();
        }
    }
}