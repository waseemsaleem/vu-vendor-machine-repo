using System.Collections.Generic;

#nullable disable


namespace VendorMachine.Core.Models
{
    public class UserModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Deposit { get; set; }
        public bool IsLoggedIn { get; set; }

        public IEnumerable<ProductModel> Products { get; set; }

        public IEnumerable<UserRoleModel> UserRoles { get; set; }
    }
}
