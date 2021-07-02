#region	License
// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions</Copyright>
//     <Url> https://github.com/trinhpapa </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Mapper </Project>
//     <File>
//         <Name> UserProfile.cs </Name>
//         <Created> 27/2/2019 - 22:28 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//         UserProfile.cs
//     </Summary>
// <License>
#endregion License

using AutoMapper;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.User;

namespace NEVAR_AQC.Mapper.User
{
    public class SYSUserProfile : Profile
    {
        public SYSUserProfile()
        {
            CreateMap<SYSUserEntity, SYSUserModel>();
            CreateMap<SYSUserEntity, UserSessionModel>();
            CreateMap<SYSUserEntity, SYSUserViewModel>();
            CreateMap<SYSUserCreateModel, SYSUserEntity>();
            CreateMap<SYSUserUpdateModel, SYSUserEntity>();
            CreateMap<SYSUserEntity, UserLoginModel>()
                .ForMember(dest => dest.Username, source => source.MapFrom(src => src.Username))
                .ForMember(dest => dest.PasswordSalt, source => source.MapFrom(src => src.PasswordSalt));
        }
    }
}