using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.User;
using System;

namespace NEVAR_AQC.Core.Models.TestDepartment
{
    public class IDTRImplementerModel : ExtensionEntity<long>
    {
        public long SpecimenPropertyId { get; set; }

        public long? UserId { get; set; }

        public bool IsAccept { get; set; }

        public DateTime? TimeToStart { get; set; }

        public DateTime? TimeToReport { get; set; }

        public SYSUserModel SYSUserEntity { get; set; }
        public SYSUserModel CRESYSUserEntity { get; set; }
        public SYSUserModel MODSYSUserEntity { get; set; }
        public SYSUserModel DELSYSUserEntity { get; set; }
        public IDTRTestPropertyModel IDTRTestPropertyEntity { get; set; }
    }
}