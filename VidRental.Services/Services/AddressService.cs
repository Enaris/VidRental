using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VidRental.DataAccess.DbModels;
using VidRental.DataAccess.Repositories;
using VidRental.Services.Dtos.Request;
using VidRental.Services.Dtos.Response.Address;

namespace VidRental.Services.Services
{
    /// <summary>
    /// Class containg logic for managing Address entity
    /// </summary>
    public class AddressService : IAddressService
    {
        /// <summary>
        /// Constructor relying on DI
        /// </summary>
        public AddressService(
            IAddressRepository addressRepo,
            IMapper mapper)
        {
            AddressRepo = addressRepo;
            Mapper = mapper;
        }

        public IAddressRepository AddressRepo { get; }
        public IMapper Mapper { get; }

        /// <summary>
        /// Method allowing to add address
        /// </summary>
        /// <param name="request">Add address request model</param>
        /// <returns>Added address</returns>
        public async Task<Address> CreateAddress(AddressAddRequest request)
        {
            var addressToAdd = Mapper.Map<AddressAddRequest, Address>(request);
            addressToAdd.IsActive = true;
            await AddressRepo.CreateAsync(addressToAdd);
            await AddressRepo.SaveChangesAsync();
            return addressToAdd;
        }

        /// <summary>
        /// Gets addresses by user
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>List of addresses</returns>
        public async Task<IEnumerable<AddressDto>> GetUserAddresses(Guid userId)
        {
            var addressesDb = await AddressRepo
                .GetAll(a => a.UserId == userId.ToString() && a.IsActive)
                .ToListAsync();

            return Mapper.Map<IEnumerable<AddressDto>>(addressesDb);
        }

        /// <summary>
        /// Deactivates address
        /// </summary>
        /// <param name="addressId">address id</param>
        /// <returns>True if operation was successful</returns>
        public async Task<bool> DeactivateAddress(Guid addressId)
        {
            return await SetAddressIsActive(addressId, false);
        }

        /// <summary>
        /// Sets given address isActive to given value
        /// </summary>
        /// <param name="addressId">Address id</param>
        /// <param name="isActive">Value to set</param>
        /// <returns>True if operation was successful</returns>
        private async Task<bool> SetAddressIsActive(Guid addressId, bool isActive)
        {
            var addressDb = await AddressRepo
                .GetAll()
                .FirstOrDefaultAsync(a => a.Id == addressId);

            if (addressDb == null)
                return false;

            addressDb.IsActive = isActive;

            AddressRepo.Update(addressDb);
            await AddressRepo.SaveChangesAsync();
            return true;
        }
    }
}
