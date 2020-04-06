using System.Threading.Tasks;
using VidRental.Services.Dtos.Response.User;

namespace VidRental.Services.Services
{
    public interface IUsersService
    {
        Task<UserBaseInfo> GetUserBaseInfo(string id);
    }
}