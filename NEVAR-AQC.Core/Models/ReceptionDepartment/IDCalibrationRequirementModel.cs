using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.User;
using System.ComponentModel.DataAnnotations;

namespace NEVAR_AQC.Core.Models.ReceptionDepartment
{
    public class IDCalibrationRequirementModel : ExtensionEntity<long>
    {
        public long RequirementInvoiceId { get; set; }

        [StringLength(200)]
        public string NameOfMeasuringDevice { get; set; }

        [StringLength(20)]
        public string SerialNumber { get; set; }

        [StringLength(200)]
        public string TechnicalCharacteristics { get; set; }

        public int? Amount { get; set; }

        [StringLength(50)]
        public string AmountUnit { get; set; }

        public bool Status { get; set; }

        public SYSUserModel CRESYSUserEntity { get; set; }
        public SYSUserModel MODSYSUserEntity { get; set; }
        public SYSUserModel DELSYSUserEntity { get; set; }

        public SYSRequirementInvoiceModel SYSRequirementInvoiceEntity { get; set; }
    }
}