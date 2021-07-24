using Eventually.Entities;

using System.Collections.Generic;
using System.Security.Claims;

namespace Eventually.Utils
{
    public interface IClaimsProvider
    {
        List<Claim> GetUserClaims(User user);
    }
}
