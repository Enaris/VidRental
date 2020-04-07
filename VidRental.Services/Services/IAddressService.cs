using System.Threading.Tasks;
using VidRental.DataAccess.DbModels;
using VidRental.Services.Dtos.Request;

namespace VidRental.Services.Services
{
    public interface IAddressService
    {
        Task<Address> CreateAddress(AddressAddRequest request);
    }
}