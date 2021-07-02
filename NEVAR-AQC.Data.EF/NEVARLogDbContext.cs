using Microsoft.EntityFrameworkCore;
using NEVAR_AQC.Core.Entities;

namespace NEVAR_AQC.Data.EF
{
    public class NEVARLogDbContext : DbContext
    {
        public NEVARLogDbContext()
        {
        }

        public NEVARLogDbContext(DbContextOptions<NEVARLogDbContext> options) : base(options)
        {
        }

        public virtual DbSet<LOGLoginEntity> LOGLogin { get; set; }
        public virtual DbSet<LOGHandleEntity> LOGHandle { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LOGLoginEntity>(entity =>
            {
                entity.ToTable("LOGLogin");
            });

            modelBuilder.Entity<LOGHandleEntity>(entity =>
            {
                entity.ToTable("LOGHandle");
            });
        }
    }
}