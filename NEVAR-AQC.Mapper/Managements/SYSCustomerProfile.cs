#region	License
// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions</Copyright>
//     <Url> https://github.com/trinhpapa </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Mapper </Project>
//     <File>
//         <Name> CustomerProfile.cs </Name>
//         <Created> 27/2/2019 - 22:28 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//         CustomerProfile.cs
//     </Summary>
// <License>
#endregion License

using AutoMapper;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.Managements;

namespace NEVAR_AQC.Mapper.Managements
{
    public class SYSCustomerProfile : Profile
    {
        public SYSCustomerProfile()
        {
            CreateMap<SYSCustomerEntity, SYSCustomerModel>();
            CreateMap<SYSCustomerEntity, SYSCustomerViewModel>();
            CreateMap<SYSCustomerCreateModel, SYSCustomerEntity>();
            CreateMap<SYSCustomerCreateModel, SYSCustomerEntity>();
        }
    }
}