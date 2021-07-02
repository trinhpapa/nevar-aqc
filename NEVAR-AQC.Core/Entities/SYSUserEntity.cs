using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEVAR_AQC.Core.Entities
{
    public partial class SYSUserEntity : ExtensionEntity<long>
    {
        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(100)]
        public string PasswordEncrypted { get; set; }

        [StringLength(10)]
        public string PasswordSalt { get; set; }

        [StringLength(100)]
        public string DisplayName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int DepartmentId { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public int RoleId { get; set; }

        [StringLength(100)]
        public string SignalRId { get; set; }

        public bool ActiveStatus { get; set; }

        public CTGDepartmentEntity CTGDepartmentEntity { get; set; }

        public CTGRoleEntity CTGRoleEntity { get; set; }
    }
}