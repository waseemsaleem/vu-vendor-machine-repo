using System.Collections.Generic;
using System.Threading.Tasks;
using VendorMachine.Core.ViewModels;

namespace VendorMachine.Core.Services.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleVM>> GetRoles();
        Task<RoleVM> GetRole(string id);
        Task<RoleVM> UpdateRole(string id, RoleVM roleModel);
        Task<RoleVM> AddRole(RoleVM role);
        Task<RoleVM> DeleteRole(string id);
    }
}
