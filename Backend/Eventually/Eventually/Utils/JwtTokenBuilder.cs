using Eventually.DTOs;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Eventually.Utils
{
    public class JwtTokenBuilder : IJwtBearerBuilder
    {
        private readonly IConfiguration _configuration;

        public JwtTokenBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public TokenResponse GetToken(List<Claim> claims)
        { 
            var siginKey = 
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]));
            var tokenSigInCredentials = 
                new SigningCredentials(siginKey, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(60);

            JwtSecurityToken token = new(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: tokenSigInCredentials);

            return new TokenResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };

        }
    }
}
