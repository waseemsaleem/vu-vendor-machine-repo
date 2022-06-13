using System.Collections.Generic;
using VendorMachine.Core.Models;
using VendorMachine.Core.ViewModels;

namespace VendorMachine.Core.Services.Interfaces
{
    public interface IUserRoleAdapter
    {
        UserRoleVM ToUserRoleVM(UserRoleModel userRole);
        UserRoleModel ToUserRoleModel(UserRoleVM userRole);
        List<UserRoleModel> ToUserRoleModel(List<UserRoleVM> userRoles);
        List<UserRoleVM> ToUserRoleVM(List<UserRoleModel> userRoles);
    }
}
