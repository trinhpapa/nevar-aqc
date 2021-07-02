using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.Managements;
using NEVAR_AQC.Core.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NEVAR_AQC.Core.Models.ReceptionDepartment
{
    public class SYSRequirementInvoiceCreateModel : LogEntity<long>
    {
        public int RequirementTypeId { get; set; }

        public int Edition { get; set; } = 0;

        public long FieldId { get; set; }

        public long CustomerId { get; set; }

        [StringLength(100)]
        public string Representative { get; set; }

        [StringLength(20)]
        public string RepresentativePhone { get; set; }

        [StringLength(200)]
        public string SpecimenStatus { get; set; }

        [StringLength(200)]
        public string SpecimenAmount { get; set; }

        [StringLength(200)]
        public string OtherInformation { get; set; }

        [StringLength(200)]
        public string OtherRequirement { get; set; }

        public bool IsSaveSpecimen { get; set; }

        public string SaveSpecimenTime { get; set; }

        public bool IsUseSubcontractors { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ResultDay { get; set; }

        public int ReturnInvoiceResultTypeId { get; set; }

        public int ResultInvoiceAmount { get; set; }

        public int ProcessStatusId { get; set; }

        public SYSCustomerModel SYSCustomerEntity { get; set; }

        public CTGRequirementTypeModel CTGRequirementTypeEntity { get; set; }

        public List<IDCalibrationRequirementModel> IDCalibrationRequirementEntities { get; set; }

        public List<IDTestRequirementModel> IDTestRequirementEntities { get; set; }
    }

    public class SYSRequirementInvoiceUpdateModel : SYSRequirementInvoiceCreateModel
    {
        public long Id { get; set; }
    }

    public class SYSRequirementInvoiceUpdateStatusModel : ExtensionEntity<long>
    {
        public int ProcessStatusId { get; set; }
    }

    public class SYSRequirementInvoiceViewModel : SYSRequirementInvoiceUpdateModel
    {
        public int? Serial { get; set; }

        public int? SerialYear { get; set; }

        [StringLength(50)]
        public string InvoiceNo { get; set; }

        public CTGReturnInvoiceResultTypeModel CTGReturnInvoiceResultTypeEntity { get; set; }

        public CTGFieldModel CTGFieldEntity { get; set; }

        public CTGRequirementStatusModel CTGRequirementStatusEntity { get; set; }

        public SYSUserModel CRESYSUserEntity { get; set; }

        public SYSUserModel MODSYSUserEntity { get; set; }

        public SYSUserModel DELSYSUserEntity { get; set; }
    }

    public class SYSRequirementInvoiceModel : ExtensionEntity<long>
    {
        public int RequirementTypeId { get; set; }

        public int? Serial { get; set; }

        public int? SerialYear { get; set; }

        [StringLength(50)]
        public string InvoiceNo { get; set; }

        public int Edition { get; set; }

        public long FieldId { get; set; }

        public long CustomerId { get; set; }

        [StringLength(100)]
        public string Representative { get; set; }

        [StringLength(20)]
        public string RepresentativePhone { get; set; }

        [StringLength(200)]
        public string OtherInformation { get; set; }

        [StringLength(200)]
        public string OtherRequirement { get; set; }

        public bool IsSaveSpecimen { get; set; }

        public string SaveSpecimenTime { get; set; }

        public bool IsUseSubcontractors { get; set; }

        public DateTime? ResultDay { get; set; }

        public int ReturnInvoiceResultTypeId { get; set; }

        public int ResultInvoiceAmount { get; set; }

        public int ProcessStatusId { get; set; }
        public CTGReturnInvoiceResultTypeModel CTGReturnInvoiceResultTypeEntity { get; set; }
        public CTGFieldModel CTGFieldEntity { get; set; }
        public CTGRequirementTypeModel CTGRequirementTypeEntity { get; set; }
        public SYSCustomerModel SYSCustomerEntity { get; set; }
        public CTGRequirementStatusModel CTGRequirementStatusEntity { get; set; }
        public SYSUserModel CRESYSUserEntity { get; set; }
        public SYSUserModel MODSYSUserEntity { get; set; }
        public SYSUserModel DELSYSUserEntity { get; set; }
        public List<IDCalibrationRequirementModel> IDCalibrationRequirementEntities { get; set; }
        public List<IDTestRequirementModel> IDTestRequirementEntities { get; set; }
    }
}