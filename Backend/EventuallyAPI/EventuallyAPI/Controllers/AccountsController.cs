using EventuallyAPI.Core.DTOs;
using EventuallyAPI.Core.Entities;
using EventuallyAPI.Infraestructure.Utils;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventuallyAPI.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtProvider _jwtProvider;

        public AccountsController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            JwtProvider jwtProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtProvider = jwtProvider;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Jwt>> Register(RegisterDTO registerDTO)
        {
            var user = new User()
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email
            };

            var creationResult = await 
                _userManager.CreateAsync(user, registerDTO.Password);

            if (creationResult.Succeeded)
            {
                return GenerateJwt(user);
            }

            return Ok(creationResult.Errors);
        }

        [HttpPost("login")]
        public async Task<ActionResult<Jwt>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user != null)
            {
                var sigInResult = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, true);

                if (sigInResult.Succeeded)
                {
                    return GenerateJwt(user);
                }
            }

            ModelState.AddModelError("invalid-credentials", 
                "Credenciales invalidas");

            return BadRequest(ModelState);

        }

        private Jwt GenerateJwt(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email)
            };

            return _jwtProvider.GetJwt(claims);
        }
    }
}
