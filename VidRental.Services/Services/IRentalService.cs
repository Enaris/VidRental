using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VidRental.Services.Dtos.Request;
using VidRental.Services.Dtos.Response.Rental;

namespace VidRental.Services.Services
{
    public interface IRentalService
    {
        Task<bool?> RentCartridge(Guid cartridgeId, Guid userId, CartridgeRentRequest request);
        Task<IEnumerable<RentalForAdminList>> GetAllRentals();
        Task<bool> UpdateReturnDate(Guid rentalId, RentalUpdateDateRequest request);
    }
}