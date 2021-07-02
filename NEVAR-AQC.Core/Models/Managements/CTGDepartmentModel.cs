using NEVAR_AQC.Core.Entities;
using NEVAR_AQC.Core.Models.User;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NEVAR_AQC.Core.Models.Managements
{
    public class CTGDepartmentCreateModel : LogEntity<long>
    {
        [StringLength(200)]
        public string Name { get; set; }

        public bool Status { get; set; } = true;
    }

    public class CTGDepartmentUpdateModel : CTGDepartmentCreateModel
    {
        public int Id { get; set; }
    }

    public class CTGDepartmentViewModel : CTGDepartmentUpdateModel
    {
    }

    public class CTGDepartmentModel : ExtensionEntity<int>
    {
        [StringLength(200)]
        public string Name { get; set; }

        public bool Status { get; set; } = true;

        public List<CTGRequirementTypeModel> CTGRequirementTypeEntities { get; set; }
        public List<SYSUserModel> SYSUserEntities { get; set; }
    }
}