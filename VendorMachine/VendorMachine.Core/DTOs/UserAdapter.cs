using System.Collections.Generic;
using VendorMachine.Core.Models;
using VendorMachine.Core.Services.Interfaces;
using VendorMachine.Core.ViewModels;

namespace VendorMachine.Core.DTOs
{
    public class UserAdapter : IUserAdapter
    {
        public static readonly UserAdapter Instance = new UserAdapter();
        public UserAdapter() { }

        public UserModel ToUserModel(UserVM user)
        {
            return new UserModel
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Password = user.Password,
                IsLoggedIn = user.IsLoggedIn,
                Deposit = user.Deposit,
            };
        }

        public List<UserModel> ToUserModel(List<UserVM> users)
        {
            List<UserModel> result = new List<UserModel>();
            foreach (UserVM user in users)
            {
                result.Add(ToUserModel(user));
            }
            return result;
        }

        public UserVM ToUserVM(UserModel user)
        {
            return new UserVM
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Password = user.Password,
                IsLoggedIn = user.IsLoggedIn,
                Deposit = user.Deposit,
            };
        }

        public List<UserVM> ToUserVM(List<UserModel> users)
        {
            List<UserVM> result = new List<UserVM>();
            foreach (UserModel user in users)
            {
                result.Add(ToUserVM(user));
            }
            return result;
        }
    }
}
