#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VendorMachine.Core.Data;
using VendorMachine.Core.Services.Interfaces;
using VendorMachine.Core.ViewModels;
using static VendorMachine.Core.Helpers.GlobalHelpers;

namespace VendorMachine.Core.Services
{
    public class UserService : IUserService
    {
        private readonly ApiDbContext _context;
        private readonly IUserAdapter _userAdapter;

        public UserService(ApiDbContext context, IUserAdapter userAdapter)
        {
            _context = context;
            _userAdapter = userAdapter;
        }


        public async Task<GenericResponse> AddUser(UserVM user)
        {

            try
            {
                user.UserId = Guid.NewGuid().ToString();
                user.Password = EncryptionHelpers.Encrypt(user.Password);
                _context.Users.Add(_userAdapter.ToUserModel(user));
                await _context.SaveChangesAsync();
                return ResponseHelper.SuccessResponse("User saved successfully", true);
            }
            catch (Exception ex)
            {
                // log the ex
                return ResponseHelper.FailResponse(ex.Message, null);

            }
        }

        public async Task<GenericResponse> DeleteUser(string id)
        {
            try
            {
                var userModel = await _context.Users.FindAsync(id);
                if (userModel == null)
                {
                    throw new Exception("Selected userService does not exists.");
                }

                _context.Users.Remove(userModel);
                await _context.SaveChangesAsync();
                return ResponseHelper.SuccessResponse("User saved successfully", true);
            }
            catch (Exception ex)
            {
                // log the ex
                return ResponseHelper.FailResponse(ex.Message, false);

            }
        }

        public async Task<GenericResponse> GetUser(string id)
        {
            try
            {
                var userModel = await _context.Users.FindAsync(id);

                if (userModel == null)
                {
                    return null;
                }

                return ResponseHelper.SuccessResponse("Get User successfully", _userAdapter.ToUserVM(userModel));
            }
            catch (Exception ex)
            {
                // log the ex
                return ResponseHelper.FailResponse(ex.Message, null);

            }
        }
        public async Task<GenericResponse> GetUserRole(string id)
        {
            try
            {
                var userModel = (from ur in _context.UserRoles
                                 join r in _context.Roles on ur.RoleId equals r.RoleId
                                 where ur.UserId == id
                                 select new UserRoleNameVM()
                                 {
                                     UserId = ur.UserId,
                                     RoleId = ur.RoleId,
                                     Id = ur.Id,
                                     RoleName = r.RoleName
                                 }).FirstOrDefault();

                if (userModel == null)
                {
                    return null;
                }

                return ResponseHelper.SuccessResponse("Get User successfully", userModel);
            }
            catch (Exception ex)
            {
                // log the ex
                return ResponseHelper.FailResponse(ex.Message, null);

            }
        }

        public async Task<GenericResponse> GetUsers()
        {
            try
            {
                if (_context.Users == null)
                {
                    return null;
                }
                return ResponseHelper.SuccessResponse("Get Users successfully", _userAdapter.ToUserVM(await _context.Users.ToListAsync()));
            }
            catch (Exception ex)
            {
                // log the ex
                return ResponseHelper.FailResponse(ex.Message, null);

            }
        }

        public async Task<GenericResponse> UpdateUser(string id, UserVM userModel)
        {
            var dbUser = await _context.Users.FindAsync(id);
            if (dbUser == null)
            {
                throw new Exception("User not exist in the database");
            }

            try
            {
                // Do not change password. Password Change should have separate endpoint.
                dbUser.Deposit = userModel.Deposit;
                await _context.SaveChangesAsync();
                return ResponseHelper.SuccessResponse("User updated successfully", true);
            }
            catch (Exception ex)
            {
                // log the ex
                return ResponseHelper.FailResponse(ex.Message, false);

            }
        }

        public async Task<GenericResponse> Deposit(int amount, IEnumerable<Claim> claims)
        {
            try
            {
                var user = (UserVM)(await GetUser(claims.First(x => x.Type.Equals(Constants.c_userId)).Value)).Reponse;
                user.Deposit = amount;
                await UpdateUser(user.UserId, user);
                return ResponseHelper.SuccessResponse("User saved successfully", true);
            }
            catch (Exception ex)
            {
                // log the ex
                return ResponseHelper.FailResponse(ex.Message, false);

            }
        }

        public async Task<GenericResponse> Reset(IEnumerable<Claim> claims)
        {
            try
            {
                var user = (UserVM)(await GetUser(claims.First(x => x.Type.Equals(Constants.c_userId)).Value)).Reponse;
                user.Deposit = 0;
                await UpdateUser(user.UserId, user);
                return ResponseHelper.SuccessResponse("User saved successfully", true);
            }
            catch (Exception ex)
            {
                // log the ex
                return ResponseHelper.FailResponse(ex.Message, false);

            }
        }
    }
}
