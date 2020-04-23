using System.Threading.Tasks;
using VidRental.Services.Dtos.Request;
using VidRental.Services.Dtos.Response.User;

namespace VidRental.Services.Services
{
    public interface IAdminService
    {
        Task<UserBaseInfo> AddEmployee(EmployeeAddRequestFlat request);
    }
}