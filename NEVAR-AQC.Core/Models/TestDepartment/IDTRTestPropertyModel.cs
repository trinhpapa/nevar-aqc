using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Core.Models.ReceptionDepartment;
using NEVAR_AQC.Core.Models.User;
using System;
using System.Collections.Generic;

namespace NEVAR_AQC.Core.Models.TestDepartment
{
    public class IDTRTestPropertyModel : ExtensionEntity<long>
    {
        public long SpecimenId { get; set; }

        public long? TestPropertyId { get; set; }

        public long? TestMethodId { get; set; }

        public int? OrderNumber { get; set; }

        public DateTime? PlanFromTime { get; set; }

        public DateTime? PlanToTime { get; set; }
        public ICollection<CTGTestMethodEntity> CTGTestMethodEntities { get; set; }
        public IDTestRequirementModel IDTestRequirementEntity { get; set; }
        public CTGTestPropertyModel CTGTestPropertyEntity { get; set; }
        public CTGTestMethodModel CTGTestMethodEntity { get; set; }
        public SYSUserModel CRESYSUserEntity { get; set; }
        public SYSUserModel MODSYSUserEntity { get; set; }
        public SYSUserModel DELSYSUserEntity { get; set; }

        public List<IDTRImplementerModel> IDTRImplementerEntities { get; set; }
        public List<IDTRTestProcessWeightMethodModel> IDTRTestProcessWeightMethodEntities { get; set; }
        public List<IDTRTestProcessVolumeMethodModel> IDTRTestProcessVolumeMethodEntities { get; set; }
        public List<IDTRTestProcessOtherMethodModel> IDTRTestProcessOtherMethodEntities { get; set; }
        public List<IDTRTestProcessAASUCVISAESMethodModel> IDTRTestProcessAASUCVISAESMethodEntities { get; set; }
    }
}