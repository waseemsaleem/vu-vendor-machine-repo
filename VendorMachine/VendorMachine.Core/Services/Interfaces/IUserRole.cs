using System.Collections.Generic;
using System.Threading.Tasks;
using VendorMachine.Core.ViewModels;

namespace VendorMachine.Core.Services.Interfaces
{
    public interface IUserRole
    {
        Task<List<UserRoleVM>> GetUserRoles();
        Task<UserRoleVM> GetUserRole(string id);
        Task<bool> UpdateUserRole(string id, UserRoleVM roleModel);
        Task<bool> AddUserRole(UserRoleVM role);
        Task<bool> DeleteUserRole(string id);
    }
}
