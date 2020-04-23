using System.Threading.Tasks;
using VidRental.Services.Dtos.Request;
using VidRental.Services.Dtos.Auth;
using VidRental.Services.Dtos.Response.User;
using VidRental.DataAccess.Roles;
using VidRental.DataAccess.DbModels;

namespace VidRental.Services.Services
{
    public interface IAuthService
    {
        Task<UserBaseInfo> Register(RegisterRequest request, RolesEnum role);
        Task<UserBaseInfo> Register(User user, string password, RolesEnum role);
    }
}