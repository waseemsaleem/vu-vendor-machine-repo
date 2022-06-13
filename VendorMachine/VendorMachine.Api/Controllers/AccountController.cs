using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VendorMachine.Core.Services.Interfaces;
using VendorMachine.Core.ViewModels;

namespace VendorMachine.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IJWTService _jwtService;

        public AccountController(IJWTService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult GetToken([FromBody] UserLogins user)
        {
            try
            {
                var token = _jwtService.GetUserTokens(user);
                if (string.IsNullOrEmpty(token.Token))
                {
                    return BadRequest("Wrong credentials");
                }

                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
