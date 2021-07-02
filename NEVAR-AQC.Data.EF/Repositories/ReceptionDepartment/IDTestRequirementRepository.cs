using Microsoft.EntityFrameworkCore;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Data.ReceptionDepartment;
using System.Collections.Generic;
using System.Linq;

namespace NEVAR_AQC.Data.EF.Repositories.ReceptionDepartment
{
    public class IDTestRequirementRepository : RepositoryBase<IDTestRequirementEntity, NEVARDbContext>, IIDTestRequirementRepository
    {
        private NEVARDbContext _context;

        public IDTestRequirementRepository(NEVARDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<IDTestRequirementEntity> TestRequirementReport(long specimentNo)
        {
            return _context.IDTestRequirement
                .Include("SYSRequirementInvoiceEntity")
                .Include("SYSRequirementInvoiceEntity.SYSCustomerEntity")
                .Include("IDTRTestPropertyEntities.CTGTestPropertyEntity")
                .Include("IDTRTestPropertyEntities.CTGTestMethodEntity")
                .Include("IDTRTestPropertyEntities.IDTRTestProcessAASUCVISAESMethodEntities")
                .Include("IDTRTestPropertyEntities.IDTRTestProcessWeightMethodEntities")
                .Include("IDTRTestPropertyEntities.IDTRTestProcessVolumeMethodEntities")
                .Include("IDTRTestPropertyEntities.IDTRTestProcessOtherMethodEntities")
                .Where(w => w.Id == specimentNo);
        }
    }
}