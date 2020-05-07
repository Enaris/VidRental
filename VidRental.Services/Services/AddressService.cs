using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VidRental.DataAccess.DbModels;
using VidRental.DataAccess.Repositories;
using VidRental.Services.Dtos.Request;

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
    }
}
