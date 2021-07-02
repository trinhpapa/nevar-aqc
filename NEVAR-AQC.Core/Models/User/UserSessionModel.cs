using System.Collections.Generic;

namespace NEVAR_AQC.Core.Models.User
{
    public class UserSessionModel
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public int? RoleId { get; set; }
        public IEnumerable<int> FunctionKeys { get; set; }
    }
}