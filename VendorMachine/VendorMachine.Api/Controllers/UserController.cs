using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VendorMachine.Core.Services.Interfaces;
using VendorMachine.Core.ViewModels;
using static VendorMachine.Core.Helpers.GlobalHelpers;

namespace VendorMachine.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            var response = await _userService.GetUsers();
            return Ok(response);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(string id)
        {
            var userModel = await _userService.GetUser(id);

            if (userModel == null)
            {
                return NotFound();
            }

            return Ok(userModel);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, UserVM userModel)
        {
            if (id != userModel.UserId)
            {
                return BadRequest();
            }

            var result = await _userService.UpdateUser(id, userModel);
            return Ok(result);
        }

        // POST: api/User
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> PostUser(UserVM userModel)
        {

            var result = await _userService.AddUser(userModel);
            return Ok(result);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteUser(id);
            return Ok(result);
        }

        [HttpPost("deposit")]
        [Authorize(Roles = "buyer")]
        public async Task<IActionResult> Deposit(int amount)
        {

            if (!Constants.amounts.Any(x => x.Equals(amount)))
            {
                return BadRequest();
            }

            var result = await _userService.Deposit(amount, User.Claims);
            return Ok(result);

        }

        [HttpPost("reset")]
        [Authorize(Roles = "buyer")]
        public async Task<IActionResult> Reset()
        {
            var result = await _userService.Reset(User.Claims);
            return Ok(result);
        }
    }
}
