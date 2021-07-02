using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.ReceptionDepartment;
using NEVAR_AQC.Core.Models.User;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NEVAR_AQC.Core.Models.Managements
{
    public class CTGRequirementStatusModel : ExtensionEntity<int>
    {
        [StringLength(100)]
        public string ProcessStatus { get; set; }

        [StringLength(6)]
        public string HtmlColour { get; set; }

        public bool? Status { get; set; } = true;

        public SYSUserModel SYSUserEntity { get; set; }

        public List<SYSRequirementInvoiceModel> SYSRequirementInvoiceEntities { get; set; }
    }
}