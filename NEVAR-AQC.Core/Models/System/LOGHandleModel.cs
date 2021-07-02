using NEVAR_AQC.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace NEVAR_AQC.Core.Models.System
{
    public class LOGHandleModel : ExtensionEntity<long>
    {
        [Required]
        [StringLength(200)]
        public string Username { get; set; }

        [Required]
        [StringLength(200)]
        public string TargetTable { get; set; }

        [Required]
        [StringLength(20)]
        public string TargetHandle { get; set; }

        public string Content { get; set; }
    }
}