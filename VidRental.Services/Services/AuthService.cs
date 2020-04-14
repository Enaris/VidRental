using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VidRental.DataAccess.DbModels;
using VidRental.Services.Dtos.Request;
using VidRental.Services.Dtos.Auth;
using VidRental.Services.Dtos.Response.User;
using System.Security.Claims;
using VidRental.DataAccess.Repositories;
using VidRental.DataAccess.Roles;

namespace VidRental.Services.Services
{
    public class AuthService : IAuthService
    {
        public AuthService(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IMapper mapper, 
            IUsersService usersService)
        {
            SignInManager = signInManager;
            UserManager = userManager;
            Mapper = mapper;
            UsersService = usersService;
        }

        public SignInManager<User> SignInManager { get; }
        public UserManager<User> UserManager { get; }
        public IMapper Mapper { get; }
        public IUsersService UsersService { get; }

        public async Task<RegisterResult> Register(RegisterRequest request)
        {
            var userToCreate = Mapper.Map<RegisterRequest, User>(request);
            userToCreate.UserName = request.Email;

            var created = await UserManager.CreateAsync(userToCreate, request.Password);

            if (!created.Succeeded)
                return new RegisterResult { IdentityResult = created };

            var roleAdded = await UserManager.AddToRoleAsync(userToCreate, ApiRoles.User);

            if (!roleAdded.Succeeded)
            {
                await UserManager.DeleteAsync(userToCreate);
                return new RegisterResult { IdentityResult = roleAdded };
            }

            var baseUserInfo = await UsersService.GetUserBaseInfo(userToCreate.Id);
            return new RegisterResult { NewUser = baseUserInfo, IdentityResult = created };
        }

    }
}
