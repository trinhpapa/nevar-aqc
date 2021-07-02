using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Data.SystemLog;

namespace NEVAR_AQC.Data.EF.Repositories.SystemLog
{
    public class LOGLoginRepository : RepositoryBase<LOGLoginEntity, NEVARLogDbContext>, ILOGLoginRepository
    {
        public LOGLoginRepository(NEVARLogDbContext context) : base(context)
        {
        }
    }
}