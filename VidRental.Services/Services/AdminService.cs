using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VidRental.DataAccess.DbModels;
using VidRental.DataAccess.Roles;
using VidRental.Services.Dtos.Request;
using VidRental.Services.Dtos.Response.User;

namespace VidRental.Services.Services
{
    /// <summary>
    /// Class containg logic for admin actions
    /// </summary>
    public class AdminService : IAdminService
    {
        private IShopEmployeeService ShopEmployeeService { get; }
        private IMapper Mapper { get; }
        private IAuthService AuthService { get; }

        public AdminService(
            IShopEmployeeService shopEmployeeService,
            IMapper mapper,
            IAuthService authService)
        {
            ShopEmployeeService = shopEmployeeService;
            Mapper = mapper;
            AuthService = authService;
        }

        /// <summary>
        /// Adds new employe and asp user entity for it
        /// </summary>
        /// <param name="request">Emplyee add request</param>
        /// <returns>Created user</returns>
        public async Task<UserBaseInfo> AddEmployee(EmployeeAddRequestFlat request)
        {
            var userToCreate = Mapper.Map<EmployeeAddRequestFlat, User>(request);
            userToCreate.UserName = request.Email;

            var userBaseInfo = await AuthService.Register(userToCreate, request.Password, RolesEnum.Employee);

            await ShopEmployeeService.AddEmployee(new EmployeeAddRequest { IsActive = true, UserId = userBaseInfo.Id });

            return userBaseInfo;
        }

    }
}
