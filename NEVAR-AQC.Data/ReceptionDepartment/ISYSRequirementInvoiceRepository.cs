#region	License
//------------------------------------------------------------------------------------------------
// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions</Copyright>
//     <Url> https://github.com/trinhpapa </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Data </Project>
//     <File>
//         <Name> IRequirementInvoiceRepository.cs </Name>
//         <Created> 27/2/2019 - 22:28:23 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//         IRequirementInvoiceRepository.cs
//     </Summary>
// <License>
//------------------------------------------------------------------------------------------------
#endregion License

using NEVAR_AQC.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace NEVAR_AQC.Data.ReceptionDepartment
{
    public interface ISYSRequirementInvoiceRepository : IRepositoryBase<SYSRequirementInvoiceEntity>
    {
        IEnumerable<SYSRequirementInvoiceEntity> GetPagedByUser(long userId, int pageIndex, int pageSize);

        IEnumerable<SYSRequirementInvoiceEntity> GetByUser(long userId);

        IQueryable<SYSRequirementInvoiceEntity> TestRequirementReport(string invoiceNo, int edition);

        IQueryable<SYSRequirementInvoiceEntity> GetDetailTestRequirementById(long id);

        IQueryable<IDTRImplementerEntity> GetDetailTestRequirementByImplementer(long userId);

        IQueryable<SYSRequirementInvoiceEntity> GetByIdForSummary(long invoiceId);

        IQueryable<SYSRequirementInvoiceEntity> GetFullByIdForUpdateTestRequirement(long invoiceId);
    }
}