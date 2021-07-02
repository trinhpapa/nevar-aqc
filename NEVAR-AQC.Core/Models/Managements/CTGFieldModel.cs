using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.ReceptionDepartment;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NEVAR_AQC.Core.Models.Managements
{
    public class CTGFieldModel : ExtensionEntity<long>
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        public string Symbol { get; set; }

        public List<SYSRequirementInvoiceModel> RequirementInvoiceEntities { get; set; }

        public List<CTGTestObjectModel> CTGTestObjectEntities { get; set; }
    }
}