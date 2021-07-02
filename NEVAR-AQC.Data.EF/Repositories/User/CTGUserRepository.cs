#region	License
//------------------------------------------------------------------------------------------------
// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions</Copyright>
//     <Url> https://github.com/trinhpapa </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Data.EF </Project>
//     <File>
//         <Name> UserRepository.cs </Name>
//         <Created> 27/2/2019 - 22:28:23 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//         UserRepository.cs
//     </Summary>
// <License>
//------------------------------------------------------------------------------------------------
#endregion License

using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Data.User;

namespace NEVAR_AQC.Data.EF.Repositories.User
{
    public class CTGUserRepository : RepositoryBase<SYSUserEntity, NEVARDbContext>, ISYSUserRepository
    {
        public CTGUserRepository(NEVARDbContext context) : base(context)
        {
        }
    }
}