#region	License
// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions</Copyright>
//     <Url> https://github.com/trinhpapa </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Mapper </Project>
//     <File>
//         <Name> RequirementInvoiceProfile.cs </Name>
//         <Created> 27/2/2019 - 22:28 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//         RequirementInvoiceProfile.cs
//     </Summary>
// <License>
#endregion License

using AutoMapper;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.ReceptionDepartment;

namespace NEVAR_AQC.Mapper.ReceptionDepartment
{
    public class SYSRequirementInvoiceProfile : Profile
    {
        public SYSRequirementInvoiceProfile()
        {
            CreateMap<SYSRequirementInvoiceEntity, SYSRequirementInvoiceViewModel>();
            CreateMap<SYSRequirementInvoiceEntity, SYSRequirementInvoiceModel>();
            CreateMap<SYSRequirementInvoiceModel, SYSRequirementInvoiceEntity>();
            CreateMap<SYSRequirementInvoiceCreateModel, SYSRequirementInvoiceEntity>();
            CreateMap<SYSRequirementInvoiceUpdateModel, SYSRequirementInvoiceEntity>();
        }
    }
}