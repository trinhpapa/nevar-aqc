using NEVAR_AQC.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NEVAR_AQC.Core.Models.Managements
{
    public class CTGCustomerTypeModel : ExtensionEntity<int>
    {
        [StringLength(200)]
        public string Name { get; set; }

        public bool Status { get; set; } = true;

        public List<SYSCustomerModel> SYSCustomerEntities { get; set; }
    }
}