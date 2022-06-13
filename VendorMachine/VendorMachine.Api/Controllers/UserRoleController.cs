using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VendorMachine.Core.Services.Interfaces;
using VendorMachine.Core.ViewModels;

namespace VendorMachine.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRole _userRole;

        public UserRoleController(IUserRole userRole)
        {
            _userRole = userRole;
        }

        // GET: api/UserRole
        [HttpGet]
        public async Task<ActionResult<List<UserRoleVM>>> GetUserRoles()
        {
            List<UserRoleVM> userRoles = await _userRole.GetUserRoles();

            if (userRoles == null)
            {
                return NotFound();
            }
            return userRoles;
        }

        // GET: api/UserRole/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRoleVM>> GetUserRole(string id)
        {
            var userRoleModel = await _userRole.GetUserRole(id);

            if (userRoleModel == null)
            {
                return NotFound();
            }

            return userRoleModel;
        }

        // PUT: api/UserRole/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserRole(string id, UserRoleVM userRoleModel)
        {
            if (id != userRoleModel.RoleId && id != userRoleModel.UserId)
            {
                return BadRequest();
            }
            bool result = await _userRole.UpdateUserRole(id, userRoleModel);
            return Ok(result);
        }

        // POST: api/UserRole
        [HttpPost]
        public async Task<ActionResult<UserRoleVM>> PostUserRole(UserRoleVM userRoleModel)
        {
            bool result = await _userRole.AddUserRole(userRoleModel);
            return Ok(result);
        }

        // DELETE: api/UserRole/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserRole(string id)
        {
            bool result = await _userRole.DeleteUserRole(id);
            return Ok(result);
        }
    }
}
