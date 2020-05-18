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
using VidRental.DataAccess.Roles;
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
            ITokenService tokenService,
            IShopUserService shopUserService, 
            IShopEmployeeService shopEmployeeService)
        {
            AuthService = authService;
            UsersService = usersService;
            AddressService = addressService;
            UserManager = userManager;
            SignInManager = signInManager;
            Configuration = configuration;
            Mapper = mapper;
            TokenService = tokenService;
            ShopUserService = shopUserService;
            ShopEmployeeService = shopEmployeeService;
        }

        private IAuthService AuthService { get; }
        private IUsersService UsersService { get; }
        private IAddressService AddressService { get; }
        private UserManager<User> UserManager { get; }
        private SignInManager<User> SignInManager { get; }
        private IConfiguration Configuration { get; }
        private IMapper Mapper { get; }
        private ITokenService TokenService { get; }
        private IShopUserService ShopUserService { get; }
        private IShopEmployeeService ShopEmployeeService { get; }

        [HttpPost("register", Name = AuthNames.Register)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var userWithRequestedEmail = await UserManager.FindByEmailAsync(request.Email);
            if (userWithRequestedEmail != null)
                return BadRequest(ApiResponse.Failure("Email", this.UserExistsMessage(request.Email)));

            var registerResult = await AuthService.Register(request, RolesEnum.User);

            if (registerResult == null)
                return BadRequest(ApiResponse.Failure("Register", "Something went wrong"));

            if (request.AddressAdded)
            {
                var addressToAdd = request.Address;
                addressToAdd.UserId = registerResult.Id;
                await AddressService.CreateAddress(addressToAdd);
            }

            await ShopUserService.AddShopUser(new ShopUserAddRequest { CanBorrow = true, UserId = registerResult.Id });

            return CreatedAtRoute(UsersNames.GetUserBaseInfo, 
                new { id = registerResult.Id, controller = UsersNames.Controller }, 
                ApiResponse<UserBaseInfo>.Success(registerResult));
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

            var employee = await ShopEmployeeService.GetEmployeeByAspUserId(userDb.Id);
            if (employee != null && !employee.IsActive)
                return BadRequest(ApiResponse.Failure("Employee", "Your account is deactivated. You cannot login."));

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