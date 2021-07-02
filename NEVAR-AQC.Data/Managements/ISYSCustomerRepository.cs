#region	License
// <License>
//     <Copyright> 2019 © Le Hoang Trinh </Copyright>
//     <Url> https://github.com/trinhpapa </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Data </Project>
//     <File>
//         <Name> ICustomerRepository.cs </Name>
//         <Created> 14/2/2019 - 12:59:56 </Created>
//     </File>
//     <Summary>
//
//     </Summary>
// </License>
#endregion License

using NEVAR_AQC.Core.Entities;

namespace NEVAR_AQC.Data.Managements
{
    public interface ISYSCustomerRepository : IRepositoryBase<SYSCustomerEntity>
    {
    }
}