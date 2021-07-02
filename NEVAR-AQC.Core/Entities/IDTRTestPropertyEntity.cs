using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEVAR_AQC.Core.Entities
{
    public class IDTRTestPropertyEntity : ExtensionEntity<long>
    {
        public long SpecimenId { get; set; }

        public long? TestPropertyId { get; set; }

        public long? TestMethodId { get; set; }

        public int? OrderNumber { get; set; }

        public DateTime? PlanFromTime { get; set; }

        public DateTime? PlanToTime { get; set; }

        [NotMapped]
        public ICollection<CTGTestMethodEntity> CTGTestMethodEntities { get; set; }

        public virtual IDTestRequirementEntity IDTestRequirementEntity { get; set; }
        public virtual CTGTestPropertyEntity CTGTestPropertyEntity { get; set; }
        public virtual CTGTestMethodEntity CTGTestMethodEntity { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual SYSUserEntity CRESYSUserEntity { get; set; }

        [ForeignKey("ModifiedBy")]
        public virtual SYSUserEntity MODSYSUserEntity { get; set; }

        [ForeignKey("DeletedBy")]
        public virtual SYSUserEntity DELSYSUserEntity { get; set; }

      [ForeignKey("SpecimenPropertyId")]
        public virtual ICollection<IDTRImplementerEntity> IDTRImplementerEntities { get; set; }

        [ForeignKey("SpecimenPropertyId")]
        public virtual ICollection<IDTRTestProcessWeightMethodEntity> IDTRTestProcessWeightMethodEntities { get; set; }

        [ForeignKey("SpecimenPropertyId")]
        public virtual ICollection<IDTRTestProcessVolumeMethodEntity> IDTRTestProcessVolumeMethodEntities { get; set; }

        [ForeignKey("SpecimenPropertyId")]
        public virtual ICollection<IDTRTestProcessOtherMethodEntity> IDTRTestProcessOtherMethodEntities { get; set; }

        [ForeignKey("SpecimenPropertyId")]
        public virtual ICollection<IDTRTestProcessAASUCVISAESMethodEntity> IDTRTestProcessAASUCVISAESMethodEntities { get; set; }
    }
}