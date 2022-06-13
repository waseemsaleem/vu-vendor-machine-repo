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
    public class RoleServiceService : IRoleService
    {
        private readonly ApiDbContext _context;
        private readonly IRoleAdapter _roleAdapter;

        public RoleServiceService(ApiDbContext context, IRoleAdapter roleAdapter)
        {
            _context = context;
            _roleAdapter = roleAdapter;
        }


        public async Task<RoleVM> AddRole(RoleVM role)
        {
            if(_context.Roles.Any(x=>x.RoleName.ToLower() == role.RoleName.ToLower()))
            {
                throw new InvalidOperationException("Role already exists");
            }
            _context.Roles.Add(_roleAdapter.ToRoleModel(role));
            try
            {
                await _context.SaveChangesAsync();
                return role;
            }
            catch (DbUpdateException)
            {
                if (RoleExists(role.RoleId))
                {
                    throw new Exception("There is a conflict while adding the roleService entity");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<RoleVM> DeleteRole(string id)
        {
            var roleModel = await _context.Roles.FindAsync(id);
            if (roleModel == null)
            {
                throw new Exception("Selected roleService does not exists.");
            }

            _context.Roles.Remove(roleModel);
            await _context.SaveChangesAsync();
            return _roleAdapter.ToRoleVM(roleModel);
        }

        public async Task<RoleVM> GetRole(string id)
        {
            var roleModel = await _context.Roles.FindAsync(id);

            if (roleModel == null)
            {
                return null;
            }

            return _roleAdapter.ToRoleVM(roleModel);
        }

        public async Task<List<RoleVM>> GetRoles()
        {
            if (_context.Roles == null)
            {
                return null;
            }
            return _roleAdapter.ToRoleVM(await _context.Roles.ToListAsync());
        }

        public async Task<RoleVM> UpdateRole(string id, RoleVM roleModel)
        {
            _context.Entry(_roleAdapter.ToRoleModel(roleModel)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return roleModel;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
                {
                    throw new Exception("Role not exist in the database");
                }
                else
                {
                    throw;
                }
            }
        }

        private bool RoleExists(string id)
        {
            return (_context.Roles?.Any(e => e.RoleId == id)).GetValueOrDefault();
        }
    }
}
