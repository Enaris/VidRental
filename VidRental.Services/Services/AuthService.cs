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
    /// <summary>
    /// Contains logic for authorization and authenctication
    /// </summary>
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

        /// <summary>
        /// Allows to register new user
        /// </summary>
        /// <param name="request">New user add request</param>
        /// <param name="role">Role new user will have</param>
        /// <returns>Created user base info</returns>
        public async Task<UserBaseInfo> Register(RegisterRequest request, RolesEnum role)
        {
            var userToCreate = Mapper.Map<RegisterRequest, User>(request);
            userToCreate.UserName = request.Email;

            return await Register(userToCreate, request.Password, role);
        }

        /// <summary>
        /// Allows to register new user
        /// </summary>
        /// <param name="user">New User (Entity)</param>
        /// <param name="password">New User password</param>
        /// <param name="role">Role new user will have</param>
        /// <returns></returns>
        public async Task<UserBaseInfo> Register(User user, string password, RolesEnum role)
        {
            var created = await UserManager.CreateAsync(user, password);

            if (!created.Succeeded)
                return null;

            var roleAdded = await UserManager.AddToRoleAsync(user, role.ToString());

            if (!roleAdded.Succeeded)
            {
                await UserManager.DeleteAsync(user);
                return null;
            }

            var baseUserInfo = await UsersService.GetUserBaseInfo(user.Id);
            return baseUserInfo;
        }

    }
}
