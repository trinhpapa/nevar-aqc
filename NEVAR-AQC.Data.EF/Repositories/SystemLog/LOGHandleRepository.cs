using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Data.SystemLog;

namespace NEVAR_AQC.Data.EF.Repositories.SystemLog
{
    public class LOGHandleRepository : RepositoryBase<LOGHandleEntity, NEVARLogDbContext>, ILOGHandleRepository
    {
        public LOGHandleRepository(NEVARLogDbContext context) : base(context)
        {
        }
    }
}