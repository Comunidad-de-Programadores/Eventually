using Eventually.Entities;

using System.Collections.Generic;
using System.Security.Claims;

namespace Eventually.Utils
{
    public class ClaimsProvider : IClaimsProvider
    {
        public List<Claim> GetUserClaims(User user)
        {
            return new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };
        }
    }
}
