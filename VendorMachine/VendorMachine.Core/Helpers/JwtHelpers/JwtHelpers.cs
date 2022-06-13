#nullable disable
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using VendorMachine.Core.ViewModels;

namespace VendorMachine.Core.Helpers.JwtHelpers
{
    public static class JwtHelpers
    {
        public static UserTokens GenTokenkey(JwtSettings jwtSettings, List<Claim> claims)
        {
            try
            {
                var UserToken = new UserTokens();
                // Get secret key
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
                Guid Id = Guid.Empty;
                DateTime expireTime = DateTime.UtcNow.AddDays(7);
                var token = new JwtSecurityToken(issuer: jwtSettings.ValidIssuer, audience: jwtSettings.ValidAudience, claims: claims, notBefore: new DateTimeOffset(DateTime.UtcNow).DateTime, expires: new DateTimeOffset(expireTime).DateTime, signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
                UserToken.Token = new JwtSecurityTokenHandler().WriteToken(token);
                UserToken.ValidFrom = token.ValidFrom;
                UserToken.ValidTo = token.ValidTo;
                return UserToken;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
