using System.Collections.Generic;

#nullable disable

namespace VendorMachine.Core.Models
{
    public class RoleModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }

        public IEnumerable<UserRoleModel> UserRoles { get; set; }
    }
}
