#nullable disable
using System.Collections.Generic;
using System.Linq;
using VendorMachine.Core.ViewModels;

namespace VendorMachine.ApiTests.Helpers
{
    public class RoleMockData
    {
        public static RoleVM _role = new RoleVM()
        {
            RoleId = "efdb929d-2998-443a-b15c-e27b9715b25f",
            RoleName = "Test"
        };
        static List<RoleVM> _roles = new List<RoleVM>()
            {
                new RoleVM{
                    RoleId = "2b368c67-51f1-4580-b0de-d8ba71a9768d",
                    RoleName = "Buyer"
                },
                new RoleVM{
                    RoleId = "efdb929d-2998-443a-b15c-e27b9715b09f",
                    RoleName = "Seller"
                }
            };

        public static List<RoleVM> GetRoles()
        {
            return _roles;
        }

        public static RoleVM GetRole(string id)
        {
            RoleVM role = GetRoles().FirstOrDefault(x => x.RoleId.Equals(id));
            return role == null ? role = new RoleVM() : role;
        }

        public static RoleVM AddRole(RoleVM role)
        {
            _roles.Add(role);
            return GetRole(role.RoleId);
        }

        public static RoleVM UpdateRole(string id, RoleVM role)
        {
            RoleVM existingRole = GetRole(id);
            existingRole = role;
            return GetRole(id);
        }

        public static RoleVM DeleteRole(string id)
        {
            RoleVM existingRole = GetRole(id);
            GetRoles().Remove(existingRole);
            return existingRole;
        }

    }
}
