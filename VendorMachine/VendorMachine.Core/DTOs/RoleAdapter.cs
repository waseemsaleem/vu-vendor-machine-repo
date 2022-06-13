#nullable disable

using System.Collections.Generic;
using VendorMachine.Core.Models;
using VendorMachine.Core.Services.Interfaces;
using VendorMachine.Core.ViewModels;

namespace VendorMachine.Core.DTOs
{
    public class RoleAdapter : IRoleAdapter
    {
        public RoleModel ToRoleModel(RoleVM role)
        {
            return new RoleModel
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName,
            };
        }

        public List<RoleModel> ToRoleModel(List<RoleVM> roles)
        {
            List<RoleModel> result = new List<RoleModel>();
            foreach (RoleVM role in roles)
            {
                result.Add(ToRoleModel(role));
            }
            return result;
        }

        public RoleVM ToRoleVM(RoleModel role)
        {
            return new RoleVM
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName,
            };
        }

        public List<RoleVM> ToRoleVM(List<RoleModel> roles)
        {
            List<RoleVM> result = new List<RoleVM>();
            foreach (RoleModel role in roles)
            {
                result.Add(ToRoleVM(role));
            }
            return result;
        }
    }
}
