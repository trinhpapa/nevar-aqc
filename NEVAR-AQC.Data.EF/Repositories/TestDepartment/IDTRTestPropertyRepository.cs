using Microsoft.EntityFrameworkCore;
using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Data.TestDepartment;
using System.Linq;
using System.Threading.Tasks;

namespace NEVAR_AQC.Data.EF.Repositories.TestDepartment
{
    public class IDTRTestPropertyRepository : RepositoryBase<IDTRTestPropertyEntity, NEVARDbContext>, IIDTRTestPropertyRepository
    {
        private NEVARDbContext _context;

        public IDTRTestPropertyRepository(NEVARDbContext context) : base(context)
        {
            _context = context;
        }

        public void DeleteSummaryOfResultItem(long propertyId)
        {
            var entity = _context.IDTRTestProperty.Where(w => w.Id == propertyId)
                .Include(w => w.IDTRTestProcessWeightMethodEntities)
                .Include(w => w.IDTRTestProcessVolumeMethodEntities)
                .Include(w => w.IDTRTestProcessOtherMethodEntities)
                .Include(w => w.IDTRTestProcessAASUCVISAESMethodEntities)
                .SingleOrDefault();
            _context.IDTRTestProcessWeightMethod.RemoveRange(entity.IDTRTestProcessWeightMethodEntities);
            _context.IDTRTestProcessVolumeMethod.RemoveRange(entity.IDTRTestProcessVolumeMethodEntities);
            _context.IDTRTestProcessOtherMethod.RemoveRange(entity.IDTRTestProcessOtherMethodEntities);
            _context.IDTRTestProcessAASUCVISAESMethod.RemoveRange(entity.IDTRTestProcessAASUCVISAESMethodEntities);
            _context.SaveChanges();
        }

        public IQueryable<IDTRTestPropertyEntity> GetTestPropertyForReport(long propertyId)
        {
            return _context.IDTRTestProperty
                .Include("IDTestRequirementEntity")
                .Include("IDTestRequirementEntity.CTGTestObjectEntity")
                .Include("CTGTestPropertyEntity")
                .Include("CTGTestMethodEntity")
                .Include("IDTRImplementerEntities.SYSUserEntity")
                .Include("IDTRTestProcessWeightMethodEntities")
                .Include("IDTRTestProcessVolumeMethodEntities")
                .Include("IDTRTestProcessOtherMethodEntities")
                .Include("IDTRTestProcessAASUCVISAESMethodEntities")
                .Where(w => w.Id == propertyId);
        }

        public IQueryable<IDTRTestPropertyEntity> GetTestPropertyForResult(long propertyId)
        {
            return _context.IDTRTestProperty
                .Include("IDTestRequirementEntity")
                .Include("IDTestRequirementEntity.CTGTestObjectEntity")
                .Include("CTGTestPropertyEntity")
                .Include("IDTRImplementerEntities.SYSUserEntity")
                .Include("IDTRTestResultEntities")
                .Where(w => w.Id == propertyId);
        }

        public IQueryable<IDTRTestPropertyEntity> GetInvoiceByTestProperty(long propertyId)
        {
            return _context.IDTRTestProperty
                .Include("IDTestRequirementEntity.SYSRequirementInvoiceEntity")
                .Where(w => w.Id == propertyId);
        }
    }
}