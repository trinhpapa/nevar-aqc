using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEVAR_AQC.Core.Entities
{
    public partial class IDTestRequirementEntity : ExtensionEntity<long>
    {
        public long RequirementInvoiceId { get; set; }

        public long? ObjectId { get; set; }

        public long? InvoiceResultSerial { get; set; }

        public int? InvoiceResultYear { get; set; }

        public string InvoiceResultNo { get; set; }

        public DateTime? InvoiceResultDate { get; set; }

        [StringLength(200)]
        public string SpecimenName { get; set; }

        [StringLength(200)]
        public string SpecimenSymbol { get; set; }

        public int SpecimenOrder { get; set; }

        [StringLength(100)]
        public string SpecimenCode { get; set; }

        [StringLength(1000)]
        public string ImageLink { get; set; }

        public int? SpecimenAmount { get; set; }

        public string SpecimenStatus { get; set; }

        public string SpecimenQuantum { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual SYSUserEntity CRESYSUserEntity { get; set; }

        [ForeignKey("ModifiedBy")]
        public virtual SYSUserEntity MODSYSUserEntity { get; set; }

        [ForeignKey("DeletedBy")]
        public virtual SYSUserEntity DELSYSUserEntity { get; set; }

        public virtual SYSRequirementInvoiceEntity SYSRequirementInvoiceEntity { get; set; }
        public virtual CTGTestObjectEntity CTGTestObjectEntity { get; set; }

        [ForeignKey("SpecimenId")]
        public virtual ICollection<IDTRTestPropertyEntity> IDTRTestPropertyEntities { get; set; }
    }
}