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
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: api/Role
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            List<RoleVM> roles = await _roleService.GetRoles();

            if (roles == null)
            {
                return NotFound();
            }
            return Ok(roles);
        }

        // GET: api/Role/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(string id)
        {
            var roleModel = await _roleService.GetRole(id);

            if (roleModel == null)
            {
                return NotFound();
            }

            return Ok(roleModel);
        }

        // PUT: api/Role/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(string id, RoleVM roleModel)
        {
            if (id != roleModel.RoleId)
            {
                return BadRequest();
            }
            RoleVM result = await _roleService.UpdateRole(id, roleModel);
            return Ok(result);
        }

        // POST: api/Role
        [HttpPost]
        public async Task<IActionResult> PostRole(RoleVM roleModel)
        {
            roleModel.RoleId = Guid.NewGuid().ToString();
            RoleVM result = await _roleService.AddRole(roleModel);
            return Ok(result);
        }

        // DELETE: api/Role/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            RoleVM result = await _roleService.DeleteRole(id);
            return Ok(result);
        }
    }
}
