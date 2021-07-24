using Eventually.DTOs;
using Eventually.Entities;
using Eventually.Utils;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Linq;
using System.Threading.Tasks;

namespace Eventually.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtBearerBuilder _jwtBearerBuilder;
        private readonly IClaimsProvider _claimsProvider;

        public AccountsController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IJwtBearerBuilder jwtBearerBuilder,
            IClaimsProvider claimsProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtBearerBuilder = jwtBearerBuilder;
            _claimsProvider = claimsProvider;
        }
        [HttpPost("login")]
        public async Task<ActionResult<TokenResponse>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user != null)
            {
                var signInResult = await _signInManager
                    .CheckPasswordSignInAsync(user, loginDTO.Password, true);

                if (signInResult.Succeeded)
                {
                    return GetTokenResponse(user);
                }

            }
            return BadRequest(
                 new { msg = "invalid credentials" });
        }

        [HttpPost("register")]
        public async Task<ActionResult<TokenResponse>> Register(RegisterDTO registerDTO)
        {
            var user = new User
            {
                Email=registerDTO.Email,
                UserName = registerDTO.UserName
            };

            var creationResult =
                await _userManager.CreateAsync(user, registerDTO.Password);

            if (creationResult.Succeeded)
            {
                return GetTokenResponse(user);
            }

            var errors =
                creationResult
                .Errors
                .ToList();

            for(int i = 0; i < errors.Count; i++)
            {
                ModelState.AddModelError(errors[i].Code,
                    errors[i].Description);
            }
            
            return BadRequest(ModelState);

        }
        private TokenResponse GetTokenResponse(User user)
        {
            var claims = _claimsProvider.GetUserClaims(user);
            var token = _jwtBearerBuilder.GetToken(claims);

            return token;
        }

    }
}
