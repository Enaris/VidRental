using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VidRental.DataAccess.DbModels;
using VidRental.Services.Dtos.Request;
using VidRental.Services.Dtos.Response.Address;

namespace VidRental.Services.Services
{
    public interface IAddressService
    {
        Task<Address> CreateAddress(AddressAddRequest request);
        Task<IEnumerable<AddressDto>> GetUserAddresses(Guid userId);
        Task<bool> DeactivateAddress(Guid addressId);
    }
}