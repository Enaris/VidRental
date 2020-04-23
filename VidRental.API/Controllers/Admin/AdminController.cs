using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VidRental.DataAccess.DbModels;
using VidRental.DataAccess.Roles;
using VidRental.Services.Dtos.Request;
using VidRental.Services.Dtos.Response.Employee;
using VidRental.Services.Dtos.Response.User;
using VidRental.Services.ResponseWrapper;
using VidRental.Services.Services;

namespace VidRental.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public AdminController(
            IShopEmployeeService shopEmployeeService,
            IMapper mapper, 
            UserManager<User> userManager,
            IAdminService adminService)
        {
            ShopEmployeeService = shopEmployeeService;
            Mapper = mapper;
            UserManager = userManager;
            AdminService = adminService;
        }

        public IShopEmployeeService ShopEmployeeService { get; }
        public IMapper Mapper { get; }
        public UserManager<User> UserManager { get; }
        public IAdminService AdminService { get; }

        [HttpGet("employees")]
        public async Task<IActionResult> Get()
        {
            var employees = await ShopEmployeeService.GetAll();

            return Ok(ApiResponse<IEnumerable<EmployeeForListFlat>>.Success(employees));
        }

        [HttpPost("employees/add")]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeAddRequestFlat request)
        {
            var userWithRequestedEmail = await UserManager.FindByEmailAsync(request.Email);
            if (userWithRequestedEmail != null)
                return BadRequest(ApiResponse.Failure("Email", "User with requested email already exists"));

            var result = await AdminService.AddEmployee(request);

            if (result == null)
                return BadRequest(ApiResponse.Failure("Api", "Something went wrong"));

            return Ok(ApiResponse<UserBaseInfo>.Success(result));
        }
    }
}