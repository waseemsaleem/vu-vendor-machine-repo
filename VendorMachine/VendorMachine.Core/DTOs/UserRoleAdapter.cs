#nullable disable

using System.Collections.Generic;
using VendorMachine.Core.Models;
using VendorMachine.Core.Services.Interfaces;
using VendorMachine.Core.ViewModels;

namespace VendorMachine.Core.DTOs
{
    public class UserRoleAdapter : IUserRoleAdapter
    {
        public UserRoleModel ToUserRoleModel(UserRoleVM userRole)
        {
            return new UserRoleModel
            {
                RoleId = userRole.RoleId,
                UserId = userRole.UserId,
            };
        }

        public List<UserRoleModel> ToUserRoleModel(List<UserRoleVM> userRoles)
        {
            List<UserRoleModel> result = new List<UserRoleModel>();
            foreach (UserRoleVM userRole in userRoles)
            {
                result.Add(ToUserRoleModel(userRole));
            }
            return result;
        }

        public UserRoleVM ToUserRoleVM(UserRoleModel userRole)
        {
            return new UserRoleVM
            {
                RoleId = userRole.RoleId,
                UserId = userRole.UserId,
            };
        }

        public List<UserRoleVM> ToUserRoleVM(List<UserRoleModel> userRoles)
        {
            List<UserRoleVM> result = new List<UserRoleVM>();
            foreach (UserRoleModel userRole in userRoles)
            {
                result.Add(ToUserRoleVM(userRole));
            }
            return result;
        }
    }
}
