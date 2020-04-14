using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
using VidRental.Services.ResponseWrapper;
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
            IAddressService addressService,
            UserManager<User> userManager, 
            SignInManager<User> signInManager,
            IConfiguration configuration, 
            IMapper mapper,
            ITokenService tokenService)
        {
            AuthService = authService;
            UsersService = usersService;
            AddressService = addressService;
            UserManager = userManager;
            SignInManager = signInManager;
            Configuration = configuration;
            Mapper = mapper;
            TokenService = tokenService;
        }

        public IAuthService AuthService { get; }
        public IUsersService UsersService { get; }
        public IAddressService AddressService { get; }
        public UserManager<User> UserManager { get; }
        public SignInManager<User> SignInManager { get; }
        public IConfiguration Configuration { get; }
        public IMapper Mapper { get; }
        public ITokenService TokenService { get; }

        [HttpPost("register", Name = AuthNames.Register)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var userWithRequestedEmail = await UserManager.FindByEmailAsync(request.Email);
            if (userWithRequestedEmail != null)
                return BadRequest(ApiResponse.Failure("Email", this.UserExistsMessage(request.Email)));

            var registerResult = await AuthService.Register(request);

            if (!registerResult.Succeeded)
                return BadRequest(ApiResponse.Failure("Register", "Something went wrong"));

            if (request.AddressAdded)
            {
                var addressToAdd = request.Address;
                addressToAdd.UserId = registerResult.NewUser.Id;
                await AddressService.CreateAddress(addressToAdd);
            }

            return CreatedAtRoute(UsersNames.GetUserBaseInfo, 
                new { id = registerResult.NewUser.Id, controller = UsersNames.Controller }, 
                ApiResponse<UserBaseInfo>.Success(registerResult.NewUser));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var userDb = await UserManager.FindByEmailAsync(request.Email);

            if (userDb == null)
                return BadRequest(ApiResponse.Failure("Login", this.BadLoginMessage()));

            var loginResult = await SignInManager.CheckPasswordSignInAsync(userDb, request.Password, false);
            
            if (!loginResult.Succeeded)
                return BadRequest(ApiResponse.Failure("Login", this.BadLoginMessage()));

            var userBaseInfo = await UsersService.GetUserBaseInfo(userDb.Id);

            var responseData = new LoginResult
            {
                Token = await TokenService.GenerateJwtToken(userDb),
                User = userBaseInfo
            };

            return Ok(ApiResponse<LoginResult>.Success(responseData));
        }

        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest request)
        {
            var result = await TokenService.RefreshToken(request.Token);
            if (result == null)
                return BadRequest(ApiResponse.Failure("Auth", "Bad token"));

            return Ok(ApiResponse<RefreshTokenResult>.Success(result));
        }
    }
}