using AutoMapper;
using LoginProject.Api.Models;
using LoginProject.Application.Dtos.Request;
using LoginProject.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoginProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IAuthenticateService _authenticationService;
        private readonly IUserManagerService _userManagerService;

        public AuthController(IMapper mapper, IConfiguration configuration, IAuthenticateService authenticationService, IUserManagerService userManagerService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            _userManagerService = userManagerService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        [HttpPost("CreateUser")]
        [Authorize]
        public async Task<ActionResult> CreateUser([FromBody] RegisterModel registerModel)
        {
            var result = await _userManagerService.RegisterUserAsync(_mapper.Map<UserRegisterDto>(registerModel), registerModel.Password);

            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
                return BadRequest(ModelState);
            }

            return Ok($"User {registerModel.Email} was created successfully");
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel userInfo)
        {
            var result = await _authenticationService.Authenticate(userInfo.Email, userInfo.Password);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
                return BadRequest(ModelState);
            }

            return GenerateToken(userInfo);
        }

        private UserToken GenerateToken(LoginModel userInfo)
        {
            var claims = new[]
            {
                new Claim("email", userInfo.Email),
                new Claim("meuvalor", "oque voce quiser"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var privateKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(10);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
                );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
