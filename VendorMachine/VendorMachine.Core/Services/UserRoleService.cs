#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VendorMachine.Core.Data;
using VendorMachine.Core.Services.Interfaces;
using VendorMachine.Core.ViewModels;

namespace VendorMachine.Core.Services
{
    public class UserRoleService : IUserRole
    {
        private readonly ApiDbContext _context;
        private readonly IUserRoleAdapter _userRoleAdapter;

        public UserRoleService(ApiDbContext context, IUserRoleAdapter userRoleAdapter)
        {
            _context = context;
            _userRoleAdapter = userRoleAdapter;
        }


        public async Task<bool> AddUserRole(UserRoleVM userRole)
        {
            if(_context.UserRoles.Any(x=>x.RoleId==userRole.RoleId&&x.UserId == userRole.UserId))
            {
                throw new Exception("Role already assigned to userService");
            }
            _context.UserRoles.Add(_userRoleAdapter.ToUserRoleModel(userRole));
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                if (UserRoleExists(userRole))
                {
                    throw new Exception("There is a conflict while adding the userRole entity");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteUserRole(string id)
        {
            var userRoleModel = await _context.UserRoles.FindAsync(id);
            if (userRoleModel == null)
            {
                throw new Exception("Selected userRole does not exists.");
            }

            _context.UserRoles.Remove(userRoleModel);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserRoleVM> GetUserRole(string id)
        {
            var userRoleModel = await _context.UserRoles.FindAsync(id);

            if (userRoleModel == null)
            {
                return null;
            }

            return _userRoleAdapter.ToUserRoleVM(userRoleModel);
        }

        public async Task<List<UserRoleVM>> GetUserRoles()
        {
            if (_context.UserRoles == null)
            {
                return null;
            }
            return _userRoleAdapter.ToUserRoleVM(await _context.UserRoles.ToListAsync());
        }

        public async Task<bool> UpdateUserRole(string id, UserRoleVM userRoleModel)
        {
            _context.Entry(_userRoleAdapter.ToUserRoleModel(userRoleModel)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRoleExists(userRoleModel))
                {
                    throw new Exception("UserRole not exist in the database");
                }
                else
                {
                    throw;
                }
            }
        }

        private bool UserRoleExists(UserRoleVM userRole)
        {
            return (_context.UserRoles?.Any(e => e.RoleId == userRole.RoleId && e.UserId == userRole.UserId)).GetValueOrDefault();
        }
    }
}
