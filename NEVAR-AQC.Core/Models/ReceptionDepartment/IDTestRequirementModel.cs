using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Core.Models.TestDepartment;
using NEVAR_AQC.Core.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NEVAR_AQC.Core.Models.ReceptionDepartment
{
    public class IDTestRequirementCreateModel : LogEntity<long>
    {
        public long RequirementInvoiceId { get; set; }

        public long? InvoiceResultSerial { get; set; }

        public int? InvoiceResultYear { get; set; }

        public string InvoiceResultNo { get; set; }

        [StringLength(200)]
        public string SpecimenName { get; set; }

        [StringLength(200)]
        public string SpecimenSymbol { get; set; }

        public int SpecimenOrder { get; set; }

        [StringLength(100)]
        public string SpecimenCode { get; set; }

        public long? ObjectId { get; set; }

        [StringLength(1000)]
        public string ImageLink { get; set; }

        public int? SpecimenAmount { get; set; }

        public string SpecimenStatus { get; set; }

        public string SpecimenQuantum { get; set; }

        public List<IDTRTestPropertyModel> IDTRTestPropertyEntities { get; set; }
    }

    public class IDTestRequirementUpdateModel : IDTestRequirementCreateModel
    {
        public long Id { get; set; }
    }

    public class IDTestRequirementViewModel : IDTestRequirementUpdateModel
    {
        public SYSRequirementInvoiceModel SYSRequirementInvoiceEntity { get; set; }

        public CTGTestObjectModel CTGTestObjectEntity { get; set; }
    }

    public class IDTestRequirementModel : ExtensionEntity<long>
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

        public SYSUserModel CRESYSUserEntity { get; set; }
        public SYSUserModel MODSYSUserEntity { get; set; }
        public SYSUserModel DELSYSUserEntity { get; set; }
        public SYSRequirementInvoiceModel SYSRequirementInvoiceEntity { get; set; }
        public CTGTestObjectModel CTGTestObjectEntity { get; set; }

        public List<IDTRTestPropertyModel> IDTRTestPropertyEntities { get; set; }

    }
}