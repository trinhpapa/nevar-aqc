using NEVAR_AQC.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace NEVAR_AQC.Core.Models.System
{
    public class LOGLoginModel : ExtensionEntity<long>
    {
        [Required]
        [StringLength(200)]
        public string Username { get; set; }

        [Required]
        public DateTime LoginTime { get; set; }

        [StringLength(200)]
        public string Browser { get; set; }
    }
}