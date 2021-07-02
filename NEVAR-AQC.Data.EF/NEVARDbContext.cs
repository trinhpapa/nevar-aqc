#region	License
//------------------------------------------------------------------------------------------------
// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions</Copyright>
//     <Url> https://github.com/trinhpapa </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Data.EF </Project>
//     <File>
//         <Name> NEVARDbContext.cs </Name>
//         <Created> 27/2/2019 - 22:28:23 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//         NEVARDbContext.cs
//     </Summary>
// <License>
//------------------------------------------------------------------------------------------------
#endregion License

using Microsoft.EntityFrameworkCore;
using NEVAR_AQC.Core.Entities;

namespace NEVAR_AQC.Data.EF
{
    public partial class NEVARDbContext : DbContext
    {
        public NEVARDbContext()
        {
        }

        public NEVARDbContext(DbContextOptions<NEVARDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CTGCustomerTypeEntity> CTGCustomerType { get; set; }
        public virtual DbSet<CTGDepartmentEntity> CTGDepartment { get; set; }
        public virtual DbSet<CTGRequirementStatusEntity> CTGRequirementStatus { get; set; }
        public virtual DbSet<CTGRequirementTypeEntity> CTGRequirementType { get; set; }
        public virtual DbSet<CTGRoleEntity> CTGRole { get; set; }
        public virtual DbSet<CTGTestMethodEntity> CTGTestMethod { get; set; }
        public virtual DbSet<CTGTestObjectEntity> CTGTestObject { get; set; }
        public virtual DbSet<CTGTestPropertyEntity> CTGTestProperty { get; set; }
        public virtual DbSet<CTGSystemFunctionEntity> CTGSystemFunction { get; set; }
        public virtual DbSet<CTGFieldEntity> CTGField { get; set; }
        public virtual DbSet<CTGReturnInvoiceResultTypeEntity> CTGReturnInvoiceResultType { get; set; }
        public virtual DbSet<SYSCustomerEntity> SYSCustomer { get; set; }
        public virtual DbSet<IDCalibrationRequirementEntity> IDCalibrationRequirement { get; set; }
        public virtual DbSet<IDTestRequirementEntity> IDTestRequirement { get; set; }
        public virtual DbSet<IDTRTestPropertyEntity> IDTRTestProperty { get; set; }
        public virtual DbSet<IDTRImplementerEntity> IDTRImplementer { get; set; }
        public virtual DbSet<IDTRTestProcessWeightMethodEntity> IDTRTestProcessWeightMethod { get; set; }
        public virtual DbSet<IDTRTestProcessVolumeMethodEntity> IDTRTestProcessVolumeMethod { get; set; }
        public virtual DbSet<IDTRTestProcessOtherMethodEntity> IDTRTestProcessOtherMethod { get; set; }
        public virtual DbSet<IDTRTestProcessAASUCVISAESMethodEntity> IDTRTestProcessAASUCVISAESMethod { get; set; }
        public virtual DbSet<SYSRequirementInvoiceEntity> SYSRequirementInvoice { get; set; }
        public virtual DbSet<SYSRoleFunctionEntity> SYSRoleFunction { get; set; }
        public virtual DbSet<SYSUserEntity> SYSUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<CTGCustomerTypeEntity>(entity =>
            {
                entity.ToTable("CTGCustomerType");
            });

            modelBuilder.Entity<CTGRequirementStatusEntity>(entity =>
            {
                entity.ToTable("CTGRequirementStatus");
            });

            modelBuilder.Entity<CTGRequirementTypeEntity>(entity =>
            {
                entity.ToTable("CTGRequirementType");
            });

            modelBuilder.Entity<CTGTestMethodEntity>(entity =>
            {
                entity.ToTable("CTGTestMethod");
            });

            modelBuilder.Entity<CTGTestObjectEntity>(entity =>
            {
                entity.ToTable("CTGTestObject");
            });

            modelBuilder.Entity<CTGTestPropertyEntity>(entity =>
            {
                entity.ToTable("CTGTestProperty");
            });

            modelBuilder.Entity<CTGSystemFunctionEntity>(entity =>
            {
                entity.ToTable("CTGSystemFunction");
            });

            modelBuilder.Entity<CTGDepartmentEntity>(entity =>
            {
                entity.ToTable("CTGDepartment");
            });

            modelBuilder.Entity<CTGRoleEntity>(entity =>
            {
                entity.ToTable("CTGRole");
            });

            modelBuilder.Entity<CTGFieldEntity>(entity =>
            {
                entity.ToTable("CTGField");
            });

            modelBuilder.Entity<CTGReturnInvoiceResultTypeEntity>(entity =>
            {
                entity.ToTable("CTGReturnInvoiceResultType");
            });

            modelBuilder.Entity<SYSCustomerEntity>(entity =>
            {
                entity.ToTable("SYSCustomer");
            });

            modelBuilder.Entity<IDCalibrationRequirementEntity>(entity =>
            {
                entity.ToTable("IDCalibrationRequirement");
            });

            modelBuilder.Entity<IDTestRequirementEntity>(entity =>
            {
                entity.ToTable("IDTestRequirement");
            });

            modelBuilder.Entity<IDTRTestPropertyEntity>(entity =>
            {
                entity.ToTable("IDTRTestProperty");
            });

            modelBuilder.Entity<IDTRImplementerEntity>(entity =>
            {
                entity.ToTable("IDTRImplementer");
            });

            modelBuilder.Entity<IDTRTestProcessWeightMethodEntity>(entity =>
            {
                entity.ToTable("IDTRTestProcessWeightMethod");
            });

            modelBuilder.Entity<IDTRTestProcessVolumeMethodEntity>(entity =>
            {
                entity.ToTable("IDTRTestProcessVolumeMethod");
            });

            modelBuilder.Entity<IDTRTestProcessOtherMethodEntity>(entity =>
            {
                entity.ToTable("IDTRTestProcessOtherMethod");
            });
            modelBuilder.Entity<IDTRTestProcessAASUCVISAESMethodEntity>(entity =>
            {
                entity.ToTable("IDTRTestProcessAASUCVISAESMethod");
            });

            modelBuilder.Entity<SYSRequirementInvoiceEntity>(entity =>
            {
                entity.ToTable("SYSRequirementInvoice");
            });

            modelBuilder.Entity<SYSRoleFunctionEntity>(entity =>
            {
                entity.ToTable("SYSRoleFunction");
            });

            modelBuilder.Entity<SYSUserEntity>(entity =>
            {
                entity.ToTable("SYSUser");
            });
        }
    }
}