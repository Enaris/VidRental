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
    public class AddressService : IAddressService
    {
        public AddressService(
            IAddressRepository addressRepo,
            IMapper mapper)
        {
            AddressRepo = addressRepo;
            Mapper = mapper;
        }

        public IAddressRepository AddressRepo { get; }
        public IMapper Mapper { get; }

        public async Task<Address> CreateAddress(AddressAddRequest request)
        {
            var addressToAdd = Mapper.Map<AddressAddRequest, Address>(request);
            addressToAdd.IsActive = true;
            await AddressRepo.CreateAsync(addressToAdd);
            await AddressRepo.SaveChangesAsync();
            return addressToAdd;
        }

        public async Task<IEnumerable<AddressDto>> GetUserAddresses(Guid userId)
        {
            var addressesDb = await AddressRepo
                .GetAll(a => a.UserId == userId.ToString() && a.IsActive)
                .ToListAsync();

            return Mapper.Map<IEnumerable<AddressDto>>(addressesDb);
        }

        public async Task<bool> DeactivateAddress(Guid addressId)
        {
            return await SetAddressIsActive(addressId, false);
        }

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
