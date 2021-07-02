#region	License
// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions</Copyright>
//     <Url> https://github.com/trinhpapa </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Mapper </Project>
//     <File>
//         <Name> AutoMapperConfiguration.cs </Name>
//         <Created> 27/2/2019 - 22:28 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//         AutoMapperConfiguration.cs
//     </Summary>
// <License>
#endregion License

using AutoMapper;
using NEVAR_AQC.Mapper.Managements;
using NEVAR_AQC.Mapper.ReceptionDepartment;
using NEVAR_AQC.Mapper.System;
using NEVAR_AQC.Mapper.TestDepartment;
using NEVAR_AQC.Mapper.User;

namespace NEVAR_AQC.Mapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(config =>
           {
               config.AddProfile(new CTGFieldProfile());
               config.AddProfile(new CTGRequirementTypeProfile());
               config.AddProfile(new CTGDepartmentProfile());
               config.AddProfile(new CTGReturnInvoiceResultTypeProfile());
               config.AddProfile(new CTGTestMethodProfile());
               config.AddProfile(new CTGTestPropertyProfile());
               config.AddProfile(new CTGTestObjectProfile());
               config.AddProfile(new CTGRoleProfile());
               config.AddProfile(new CTGSystemFunctionProfile());
               config.AddProfile(new CTGCustomerTypeProfile());
               config.AddProfile(new IDTestRequirementProfile());
               config.AddProfile(new IDTRTestProcessAASUCVISAESMethodProfile());
               config.AddProfile(new IDTRTestProcessOtherMethodProfile());
               config.AddProfile(new IDTRTestProcessVolumeMethodProfile());
               config.AddProfile(new IDTRTestProcessWeightMethodProfile());
               config.AddProfile(new SYSUserProfile());
               config.AddProfile(new SYSCustomerProfile());
               config.AddProfile(new SYSRoleFunctionProfile());
               config.AddProfile(new SYSRequirementInvoiceProfile());
               config.AddProfile(new IDTRImplementerProfile());
               config.AddProfile(new LOGLoginProfile());
               config.AddProfile(new LOGHandleProfile());
           });
        }
    }
}