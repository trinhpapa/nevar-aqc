#region	License
//------------------------------------------------------------------------------------------------
// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions</Copyright>
//     <Url> https://github.com/trinhpapa </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Data.EF </Project>
//     <File>
//         <Name> DepartmentRepository.cs </Name>
//         <Created> 27/2/2019 - 22:28:23 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//         DepartmentRepository.cs
//     </Summary>
// <License>
//------------------------------------------------------------------------------------------------
#endregion License

using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Data.Managements;

namespace NEVAR_AQC.Data.EF.Repositories.Managements
{
    public class CTGDepartmentRepository : RepositoryBase<CTGDepartmentEntity, NEVARDbContext>, ICTGDepartmentRepository
    {
        public CTGDepartmentRepository(NEVARDbContext context) : base(context)
        {
        }
    }
}