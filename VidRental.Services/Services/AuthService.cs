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

namespace VidRental.Services.Services
{
    public class AuthService : IAuthService
    {
        public AuthService(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IMapper mapper)
        {
            SignInManager = signInManager;
            UserManager = userManager;
            Mapper = mapper;
        }

        public SignInManager<User> SignInManager { get; }
        public UserManager<User> UserManager { get; }
        public IMapper Mapper { get; }

        public async Task<RegisterResult> Register(RegisterRequest request)
        {
            var userToCreate = Mapper.Map<RegisterRequest, User>(request);
            userToCreate.UserName = request.Email;

            var created = await UserManager.CreateAsync(userToCreate, request.Password);

            if (!created.Succeeded)
                return new RegisterResult { IdentityResult = created };

            var baseUserInfo = Mapper.Map<User, UserBaseInfo>(userToCreate);
            return new RegisterResult { NewUser = baseUserInfo, IdentityResult = created };
        }
    }
}
