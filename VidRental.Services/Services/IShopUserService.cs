using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VidRental.Services.Dtos.Request;
using VidRental.Services.Dtos.Response.Rental;

namespace VidRental.Services.Services
{
    public interface IShopUserService
    {
        Task AddShopUser(ShopUserAddRequest addRequest);
        Task<bool?> CanUserRent(Guid userId);
        Task<IEnumerable<RentalBaseInfo>> GetUserRentals(Guid userId);
    }
}