using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidRental.DataAccess.DbModels;
using VidRental.DataAccess.Repositories;
using VidRental.Services.Dtos.Request;
using VidRental.Services.Dtos.Response.Employee;

namespace VidRental.Services.Services
{
    /// <summary>
    /// Contains logic that shop employee uses
    /// </summary>
    public class ShopEmployeeService : IShopEmployeeService
    {
        public ShopEmployeeService(
            IShopEmployeeRepository shopEmployeeRepo,
            IMapper mapper)
        {
            ShopEmployeeRepo = shopEmployeeRepo;
            Mapper = mapper;
        }

        public IShopEmployeeRepository ShopEmployeeRepo { get; }
        public IMapper Mapper { get; }

        /// <summary>
        /// Gets employee by id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Emplyee</returns>
        public async Task<EmployeeForListFlat> GetEmployee(Guid id)
        {
            var employeeDb = await ShopEmployeeRepo
                .GetAll()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employeeDb == null)
                return null;

            return Mapper.Map<EmployeeForListFlat>(employeeDb);
        }

        /// <summary>
        /// Gets employee bu asp user Id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>User data</returns>
        public async Task<EmployeeForListFlat> GetEmployeeByAspUserId(string userId)
        {
            var employeeDb = await ShopEmployeeRepo
                .GetAll()
                .FirstOrDefaultAsync(e => e.UserId == userId);

            if (employeeDb == null)
                return null;

            return Mapper.Map<EmployeeForListFlat>(employeeDb);
        }

        /// <summary>
        /// Adds emplyee to database
        /// </summary>
        /// <param name="addRequest">Employee add request</param>
        public async Task AddEmployee(EmployeeAddRequest addRequest)
        {
            var newShopUser = Mapper.Map<ShopEmployee>(addRequest);
            await ShopEmployeeRepo.CreateAsync(newShopUser);
            await ShopEmployeeRepo.SaveChangesAsync();
        }

        /// <summary>
        /// Activates or deactivates employee
        /// </summary>
        /// <param name="employeeId">Emplyee id</param>
        /// <param name="isActive">True to activate</param>
        /// <returns>True if successful</returns>
        public async Task<bool> SetIsActive(Guid employeeId, bool isActive)
        {
            var employeeDb = await ShopEmployeeRepo
                .GetAll()
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employeeDb == null)
                return false;

            employeeDb.IsActive = isActive;

            ShopEmployeeRepo.Update(employeeDb);

            await ShopEmployeeRepo.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Deactivates employee
        /// </summary>
        /// <param name="employeeId">Employee id</param>
        /// <returns>True if successful</returns>
        public async Task<bool> Deactivate(Guid employeeId)
        {
            return await SetIsActive(employeeId, false);
        }

        /// <summary>
        /// Activates employee
        /// </summary>
        /// <param name="employeeId">Employee id</param>
        /// <returns>True if successfull</returns>
        public async Task<bool> Activate(Guid employeeId)
        {
            return await SetIsActive(employeeId, true);
        }

        /// <summary>
        /// Gets all employees
        /// </summary>
        /// <returns>List of employees</returns>
        public async Task<IEnumerable<EmployeeForListFlat>> GetAll()
        {
            var employees = await ShopEmployeeRepo
                .GetWithAspUser()
                .Where(e => e.User.UserName != "admin@vidRental.com")
                .ToListAsync();

            var result = Mapper.Map<IEnumerable<EmployeeForListFlat>>(employees);

            return result;
        }
    }
}
