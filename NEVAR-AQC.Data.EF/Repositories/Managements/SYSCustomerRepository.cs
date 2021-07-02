#region	License
// <License>
//     <Copyright> 2019 © Le Hoang Trinh </Copyright>
//     <Url> https://github.com/trinhpapa </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Data.EF </Project>
//     <File>
//         <Name> CustomerRepository.cs </Name>
//         <Created> 14/2/2019 - 13:00:59 </Created>
//     </File>
//     <Summary>
//
//     </Summary>
// </License>
#endregion License

using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Data.Managements;

namespace NEVAR_AQC.Data.EF.Repositories.Managements
{
    public class SYSCustomerRepository : RepositoryBase<SYSCustomerEntity, NEVARDbContext>, ISYSCustomerRepository
    {
        public SYSCustomerRepository(NEVARDbContext context) : base(context)
        {
        }
    }
}