using System.Threading.Tasks;
using VidRental.DataAccess.DbModels;
using VidRental.Services.Dtos.Auth;

namespace VidRental.Services.Services
{
    public interface ITokenService
    {
        Task<string> GenerateJwtToken(User user);
        Task<RefreshTokenResult> RefreshToken(string token);
    }
}