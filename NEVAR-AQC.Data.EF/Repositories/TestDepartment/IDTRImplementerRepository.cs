using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Data.TestDepartment;

namespace NEVAR_AQC.Data.EF.Repositories.TestDepartment
{
    public class IDTRImplementerRepository : RepositoryBase<IDTRImplementerEntity, NEVARDbContext>, IIDTRImplementerRepository
    {
        public IDTRImplementerRepository(NEVARDbContext context) : base(context)
        {
        }
    }
}