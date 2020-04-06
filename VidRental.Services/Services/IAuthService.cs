using System.Threading.Tasks;
using VidRental.Services.Dtos.Request;
using VidRental.Services.Dtos.Auth;

namespace VidRental.Services.Services
{
    public interface IAuthService
    {
        Task<RegisterResult> Register(RegisterRequest request);
    }
}