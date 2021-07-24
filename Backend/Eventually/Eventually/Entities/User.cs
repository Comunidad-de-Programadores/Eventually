using Microsoft.AspNetCore.Identity;

using System.Collections.Generic;

namespace Eventually.Entities
{
    public class User : IdentityUser

    {
        public List<UserArea> UserAreas { get; set; }
    }
}
