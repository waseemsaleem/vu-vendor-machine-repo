using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using VendorMachine.Core.ViewModels;

namespace VendorMachine.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<GenericResponse> GetUsers();
        Task<GenericResponse> GetUser(string id);
        Task<GenericResponse> GetUserRole(string id);
        Task<GenericResponse> UpdateUser(string id, UserVM userModel);
        Task<GenericResponse> AddUser(UserVM user);
        Task<GenericResponse> DeleteUser(string id);
        Task<GenericResponse> Deposit(int amount, IEnumerable<Claim> claims);
        Task<GenericResponse> Reset(IEnumerable<Claim> claims);
    }
}
