#nullable disable

using System.Collections.Generic;
using VendorMachine.Core.Models;
using VendorMachine.Core.ViewModels;

namespace VendorMachine.Core.Services.Interfaces
{
    public interface IRoleAdapter
    {
        RoleVM ToRoleVM(RoleModel role);
        RoleModel ToRoleModel(RoleVM role);
        List<RoleModel> ToRoleModel(List<RoleVM> roles);
        List<RoleVM> ToRoleVM(List<RoleModel> roles);
    }
}
