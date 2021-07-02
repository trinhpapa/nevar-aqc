using System.ComponentModel.DataAnnotations;

namespace NEVAR_AQC.Core.Models.User
{
    public class UserLoginModel
    {
        [StringLength(50)]
        [MinLength(1)]
        public string Username { get; set; }

        [StringLength(100)]
        [MinLength(1)]
        public string PasswordOrigin { get; set; }

        [StringLength(10)]
        public string PasswordSalt { get; set; }
    }
}