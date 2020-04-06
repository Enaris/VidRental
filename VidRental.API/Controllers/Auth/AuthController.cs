using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VidRental.API.Controllers.Users;
using VidRental.DataAccess.DbModels;
using VidRental.Services.Dtos.Auth;
using VidRental.Services.Dtos.Request;
using VidRental.Services.Dtos.Response;
using VidRental.Services.Dtos.Response.User;
using VidRental.Services.Services;

namespace VidRental.API.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(
            IAuthService authService,
            IUsersService usersService,
            UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            IConfiguration configuration, 
            IMapper mapper)
        {
            AuthService = authService;
            UsersService = usersService;
            UserManager = userManager;
            SignInManager = signInManager;
            Configuration = configuration;
            Mapper = mapper;
        }

        public IAuthService AuthService { get; }
        public IUsersService UsersService { get; }
        public UserManager<User> UserManager { get; }
        public SignInManager<User> SignInManager { get; }
        public IConfiguration Configuration { get; }
        public IMapper Mapper { get; }

        [HttpPost("register", Name = AuthNames.Register)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var registerResult = await AuthService.Register(request);

            if (!registerResult.Succeeded)
                return BadRequest(registerResult.Errros);

            return CreatedAtRoute(UsersNames.GetUserBaseInfo, 
                new { id = registerResult.NewUser.Id, controller = UsersNames.Controller }, 
                registerResult.NewUser);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var userDb = await UserManager.FindByEmailAsync(request.Email);

            if (userDb == null)
                return BadRequest("Login or password is invalid");

            var loginResult = await SignInManager.CheckPasswordSignInAsync(userDb, request.Password, false);
            
            if (!loginResult.Succeeded)
                return BadRequest("Login or password is invalid");

            var token = GenerateJwtToken(userDb);
            var userBaseInfo = Mapper.Map<User, UserBaseInfo>(userDb);

            return Ok(new LoginResult { Token = token, User = userBaseInfo });
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(Configuration["JwtExpiryInDays"]));

            var token = new JwtSecurityToken(
                Configuration["JwtIssuer"],
                Configuration["JwtAudience"],
                claims,
                expires: expiry,
                signingCredentials: creds
            );

            var tokenHanlder = new JwtSecurityTokenHandler();

            return tokenHanlder.WriteToken(token);
        }
    }
}