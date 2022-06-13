#nullable disable

namespace VendorMachine.Core.Models
{
    public class UserRoleModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public UserModel User { get; set; }
        public string RoleId { get; set; }
        public RoleModel Role { get; set; }
    }
}
