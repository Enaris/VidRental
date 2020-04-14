using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidRental.DataAccess.DbModels;
using VidRental.Services.Dtos.Response;
using VidRental.Services.Dtos.Response.Role;
using VidRental.Services.Dtos.Response.User;

namespace VidRental.Services.Services
{
    public class UsersService : IUsersService
    {
        public UsersService(UserManager<User> userManager, IMapper mapper)
        {
            UserManager = userManager;
            Mapper = mapper;
        }

        public UserManager<User> UserManager { get; }
        public IMapper Mapper { get; }

        public async Task<UserBaseInfo> GetUserBaseInfo(string id)
        {
            var userDb = await UserManager.Users.FirstOrDefaultAsync(user => user.Id == id);

            if (userDb == null)
                return null;

            var userBaseInfo = Mapper.Map<User, UserBaseInfo>(userDb);
            var userRoles = await UserManager.GetRolesAsync(userDb);
            userBaseInfo.Roles = userRoles.Select(r => new RoleDto { Name = r });

            return userBaseInfo;
        }
    }
}
