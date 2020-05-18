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
using VidRental.Services.Dtos.Response.Rental;
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
            IAdminService adminService, 
            IRentalService rentalService)
        {
            ShopEmployeeService = shopEmployeeService;
            Mapper = mapper;
            UserManager = userManager;
            AdminService = adminService;
            RentalService = rentalService;
        }

        public IShopEmployeeService ShopEmployeeService { get; }
        public IMapper Mapper { get; }
        public UserManager<User> UserManager { get; }
        private IAdminService AdminService { get; }
        private IRentalService RentalService { get; }

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

        [HttpGet("allRentals")]
        public async Task<IActionResult> GetAllRentals()
        {
            var rentals = await RentalService.GetAllRentals();

            return Ok(ApiResponse<IEnumerable<RentalForAdminList>>.Success(rentals));
        }

        [HttpPost("employee/{employeeId}/fire")]
        public async Task<IActionResult> FireEmployee(Guid employeeId)
        {
            var result = await ShopEmployeeService.Deactivate(employeeId);

            if (!result)
                return BadRequest(ApiResponse.Failure("Fire", "Employee does not seem to exist"));

            return Ok(ApiResponse.Success());
        }

        [HttpPost("employee/{employeeId}/activate")]
        public async Task<IActionResult> ActivateEmployee(Guid employeeId)
        {
            var result = await ShopEmployeeService.Activate(employeeId);

            if (!result)
                return BadRequest(ApiResponse.Failure("Activate", "Employee does not seem to exist"));

            return Ok(ApiResponse.Success());
        }
    }
}