#region	License
// <License>
//     <Copyright> 2019 © Le Hoang Trinh </Copyright>
//     <Url> https://github.com/trinhpapa </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Data </Project>
//     <File>
//         <Name> IUserRepository.cs </Name>
//         <Created> 13/2/2019 - 19:35:21 </Created>
//     </File>
//     <Summary>
//
//     </Summary>
// </License>
#endregion License

using NEVAR_AQC.Core.Entities;

namespace NEVAR_AQC.Data.User
{
    public interface ISYSUserRepository : IRepositoryBase<SYSUserEntity>
    {
    }
}