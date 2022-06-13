using System.Collections.Generic;
using VendorMachine.Core.Models;
using VendorMachine.Core.ViewModels;

namespace VendorMachine.Core.Services.Interfaces
{
    public interface IUserAdapter
    {
        UserVM ToUserVM(UserModel user);
        UserModel ToUserModel(UserVM user);
        List<UserModel> ToUserModel(List<UserVM> users);
        List<UserVM> ToUserVM(List<UserModel> users);
    }
}
