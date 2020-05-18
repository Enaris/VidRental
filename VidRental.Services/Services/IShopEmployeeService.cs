using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VidRental.Services.Dtos.Request;
using VidRental.Services.Dtos.Response.Employee;

namespace VidRental.Services.Services
{
    public interface IShopEmployeeService
    {
        Task AddEmployee(EmployeeAddRequest addRequest);
        Task<IEnumerable<EmployeeForListFlat>> GetAll();
        Task<bool> Activate(Guid employeeId);
        Task<bool> Deactivate(Guid employeeId);
        Task<bool> SetIsActive(Guid employeeId, bool isActive);
        Task<EmployeeForListFlat> GetEmployee(Guid id);
        Task<EmployeeForListFlat> GetEmployeeByAspUserId(string userId);
    }
}