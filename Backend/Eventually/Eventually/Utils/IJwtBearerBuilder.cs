using Eventually.DTOs;

using System.Collections.Generic;
using System.Security.Claims;

namespace Eventually.Utils
{
    public interface IJwtBearerBuilder
    {
        public TokenResponse GetToken(List<Claim> claims);
    }
}
