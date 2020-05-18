﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidRental.DataAccess.DbModels;
using VidRental.DataAccess.Repositories;
using VidRental.Services.Dtos.Request;
using VidRental.Services.Dtos.Response.Rental;

namespace VidRental.Services.Services
{
    public class RentalService : IRentalService
    {
        public RentalService(
            ICartridgeRepository cartridgeRepo,
            IRentalRepository rentalRepo,
            ICartridgeCopyRepository cartridgeCopyRepo,
            IAddressService addressService,
            IAddressRepository addressRepo,
            IShopUserRepository shopUserRepo,
            IMapper mapper)
        {
            CartridgeRepo = cartridgeRepo;
            RentalRepo = rentalRepo;
            CartridgeCopyRepo = cartridgeCopyRepo;
            AddressService = addressService;
            AddressRepo = addressRepo;
            ShopUserRepo = shopUserRepo;
            Mapper = mapper;
        }

        private ICartridgeRepository CartridgeRepo { get; }
        private IRentalRepository RentalRepo { get; }
        private ICartridgeCopyRepository CartridgeCopyRepo { get; }
        private IAddressService AddressService { get; }
        private IAddressRepository AddressRepo { get; }
        private IShopUserRepository ShopUserRepo { get; }
        private IMapper Mapper { get; }

        private const string collectionInPerson = "CollectionInPerson";

        public async Task<bool?> RentCartridge(Guid cartridgeId, Guid userId, CartridgeRentRequest request)
        {
            var cartridge = await CartridgeRepo
                .GetAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == cartridgeId);

            if (cartridge == null)
                return null;

            var copy = await CartridgeCopyRepo
                .GetAll()
                .FirstOrDefaultAsync(c => c.CartridgeId == cartridgeId && c.Avaible);

            if (copy == null)
                return false;

            var address = new Address();

            if (request.Delivery == collectionInPerson)
                address = null;
            else if (request.AddAddress)
                address = await AddressService.CreateAddress(request.NewAddress);
            else
            {
                address = await AddressRepo
                    .GetAll()
                    .FirstOrDefaultAsync(a => a.Id == new Guid(request.AddressId));

                if (address == null)
                    return null;
            }

            var shopUser = await ShopUserRepo
                .GetAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(su => su.UserId == userId.ToString());

            var rental = new Rental
            {
                DaysToReturn = cartridge.DaysToReturn,
                RentPrice = cartridge.RentPrice,
                Rented = DateTime.UtcNow,
                AddressId = address?.Id,
                CartridgeCopyId = copy.Id,
                Delivery = request.Delivery,
                ShopUserId = shopUser.Id,
            };

            copy.Avaible = false;
            CartridgeCopyRepo.Update(copy);
            await RentalRepo.CreateAsync(rental);
            await RentalRepo.SaveChangesAsync();
            return true;
        }
    
        public async Task<IEnumerable<RentalForAdminList>> GetAllRentals()
        {
            var rentals = await RentalRepo
                .GetAll()
                .Include(r => r.CartridgeCopy)
                    .ThenInclude(cc => cc.Cartridge)
                        .ThenInclude(c => c.Movie)
                .Include(r => r.ShopUser)
                    .ThenInclude(su => su.User)
                .ToListAsync();

            if (rentals.Count == 0)
                return new List<RentalForAdminList>();

            var result = rentals.Select(r =>
            {
                var rentalBaseInfo = Mapper.Map<RentalForAdminList>(r);
                rentalBaseInfo.MovieLanguage = r.CartridgeCopy.Cartridge.Language;
                rentalBaseInfo.MovieTitle = r.CartridgeCopy.Cartridge.Movie.Title;
                rentalBaseInfo.MovieReleaseYear = r.CartridgeCopy.Cartridge.Movie.ReleaseDate.Year;
                rentalBaseInfo.UserFirstName = r.ShopUser.User.FirstName;
                rentalBaseInfo.UserLastName = r.ShopUser.User.LastName;
                rentalBaseInfo.UserPhone = r.ShopUser.User.PhoneNumber;
                return rentalBaseInfo;
            });

            return result;
        }
    
        public async Task<bool> UpdateReturnDate(Guid rentalId, RentalUpdateDateRequest request)
        {
            var rental = await RentalRepo
                .GetAll()
                .FirstOrDefaultAsync(r => r.Id == rentalId);

            if (rental == null)
                return false;

            rental.Returned = request.Date;
            await RentalRepo.SaveChangesAsync();

            return true;
        }
    }
}
