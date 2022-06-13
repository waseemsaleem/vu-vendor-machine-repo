using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using VendorMachine.Core.Data;
using VendorMachine.Core.Helpers.JwtHelpers;
using VendorMachine.Core.Services.Interfaces;
using VendorMachine.Core.ViewModels;
using static VendorMachine.Core.Helpers.GlobalHelpers;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace VendorMachine.Core.Services
{
    public class JWTService : IJWTService
    {
        private readonly ApiDbContext _context;
        private readonly JwtSettings _jwtSettings;
        private const string c_userId = "UserId";

        public JWTService(ApiDbContext context, JwtSettings jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings;
        }

        public UserTokens GetUserTokens(UserLogins user)
        {
            UserTokens token = new UserTokens();
            var dbUser = _context.Users.FirstOrDefault(x => x.UserName.Equals(user.UserName) && x.Password.Equals(EncryptionHelpers.Encrypt(user.Password)));
            if (dbUser != null)
            {
                var roles = from ur in _context.UserRoles
                            join r in _context.Roles on ur.RoleId equals r.RoleId
                            where ur.UserId == dbUser.UserId
                            select r;


                var authClaims = new List<Claim>()
                    {
                        new Claim(c_userId, dbUser.UserId),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                foreach (var userRole in roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole.RoleName));
                }

                token = JwtHelpers.GenTokenkey(_jwtSettings, authClaims);
            }
            return token;
        }
        public string ValidateJwtToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.IssuerSigningKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == c_userId).Value;

                // return user id from JWT token if validation successful
                return userId;
            }
            catch(Exception ex)
            {
                // return null if validation fails
                return null;
            }
        }
    }
}
