using NEVAR_AQC.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace NEVAR_AQC.Core.Models.Managements
{
    public class CTGRequirementTypeViewModel : CTGRequirementTypeModel
    {
    }

    public class CTGRequirementTypeModel : ExtensionEntity<int>
    {
        [StringLength(200)]
        public string Vietnamese { get; set; }

        [StringLength(200)]
        public string English { get; set; }

        [StringLength(10)]
        public string Alias { get; set; }

        public int DepartmentId { get; set; }

        public CTGDepartmentModel CTGDepartmentEntity { get; set; }
    }
}