using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Models;
using API.services;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IOptions<JwtOptions> _jwtOptions;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="configuration"></param>
        public AuthController(UserService userService, IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions ?? throw new ArgumentNullException(nameof(jwtOptions));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User model)
        {
            var user = _userService.Authenticate(model.Username, model.PasswordHash);

            if (user == null)
            {
                return Unauthorized();
            }
            

            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            try
            {
                User user = new User
                {
                    Username = model.Username,
                    PasswordHash = model.Password
                };

                var isSuccess = _userService.Register(user);

                if (isSuccess)
                {
                    var token = GenerateJwtToken(user);
                    return Ok(new { Token = token });
                }
                else
                {
                    return BadRequest("Registration failed. User with the same name already exists.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }


        private string GenerateJwtToken(User user)
        {
            var jwtOptions = _jwtOptions.Value;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                // claims
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(10000);

            var token = new JwtSecurityToken(
                null,
                null,
                claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }

}
