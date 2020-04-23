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

        public async Task AddEmployee(EmployeeAddRequest addRequest)
        {
            var newShopUser = Mapper.Map<ShopEmployee>(addRequest);
            await ShopEmployeeRepo.CreateAsync(newShopUser);
            await ShopEmployeeRepo.SaveChangesAsync();
        }

        public async Task<bool> SetIsActive(Guid employeeId, bool isActive)
        {
            var employeeDb = await ShopEmployeeRepo
                .GetAll()
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employeeId == null)
                return false;

            employeeDb.IsActive = isActive;

            ShopEmployeeRepo.Update(employeeDb);

            await ShopEmployeeRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Deactivate(Guid employeeId)
        {
            return await SetIsActive(employeeId, false);
        }

        public async Task<bool> Activate(Guid employeeId)
        {
            return await SetIsActive(employeeId, true);
        }

        public async Task<IEnumerable<EmployeeForListFlat>> GetAll()
        {
            var employees = await ShopEmployeeRepo.GetWithAspUser().ToListAsync();

            var result = Mapper.Map<IEnumerable<EmployeeForListFlat>>(employees);

            return result;
        }
    }
}
