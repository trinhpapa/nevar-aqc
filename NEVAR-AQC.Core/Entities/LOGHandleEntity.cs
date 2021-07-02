using System.ComponentModel.DataAnnotations;

namespace NEVAR_AQC.Core.Entities
{
    public class LOGHandleEntity : ExtensionEntity<long>
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