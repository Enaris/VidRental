using System.Threading.Tasks;
using VidRental.Services.Dtos.Request;

namespace VidRental.Services.Services
{
    public interface IShopUserService
    {
        Task AddShopUser(ShopUserAddRequest addRequest);
    }
}