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
using VidRental.Services.Dtos.Response.Address;
using VidRental.Services.Dtos.Response.Cartridge;

namespace VidRental.Services.Services
{
    public class CartridgeService : ICartridgeService
    {
        public CartridgeService(
            ICartridgeRepository cartridgeRepo,
            ICartridgeCopyRepository cartridgeCopyRepo,
            IMapper mapper, 
            IMovieService movieService,
            IShopUserRepository shopUserRepo,
            IRentalRepository rentalRepo, 
            IAddressRepository addressRepo)
        {
            CartridgeRepo = cartridgeRepo;
            CartridgeCopyRepo = cartridgeCopyRepo;
            Mapper = mapper;
            MovieService = movieService;
            ShopUserRepo = shopUserRepo;
            RentalRepo = rentalRepo;
            AddressRepo = addressRepo;
        }

        private ICartridgeRepository CartridgeRepo { get; }
        private ICartridgeCopyRepository CartridgeCopyRepo { get; }
        private IMapper Mapper { get; }
        private IMovieService MovieService { get; }
        private IShopUserRepository ShopUserRepo { get; }
        private IRentalRepository RentalRepo { get; }
        private IAddressRepository AddressRepo { get; }

        public async Task AddCartridge(CartridgeAddRequest request)
        {
            var cartridgeToAdd = Mapper.Map<Cartridge>(request);
            await CartridgeRepo.CreateAsync(cartridgeToAdd);
            await CartridgeRepo.SaveChangesAsync();

            var copiesAmount = request.AvaibleAmount + request.UnavaibleAmount;

            if (copiesAmount == 0)
                return;

            var copies = new List<CartridgeCopy>(copiesAmount);
            for (int i = 0; i < copiesAmount; ++i)
            {
                copies.Add(new CartridgeCopy
                {
                    CartridgeId = cartridgeToAdd.Id,
                    Avaible = i < request.AvaibleAmount
                });
            }

            await CartridgeCopyRepo.AddRangeAsync(copies);
            await CartridgeCopyRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<CartridgeForList>> GetForList()
        {
            var cartridgesDb = await CartridgeRepo
                .GetAll()
                .Include(c => c.Copies)
                .Include(c => c.Movie)
                .ToListAsync();

            var cartridges = cartridgesDb.Select(c =>
            {
                var cartridge = Mapper.Map<CartridgeForList>(c);
                cartridge.CopiesAvaible = c.Copies.Count(copy => copy.Avaible);
                cartridge.CopiesUnavaible = c.Copies.Count - cartridge.CopiesAvaible;
                return cartridge;
            });

            return cartridges;
        }

        public async Task<CartridgeDetails> GetCartridge(Guid id)
        {
            var cartridgeDb = await CartridgeRepo
                .GetAll()
                .Include(c => c.Copies)
                .Include(c => c.Movie)
                .FirstOrDefaultAsync(c => c.Id == id);

            var cartridge = Mapper.Map<CartridgeDetails>(cartridgeDb);
            cartridge.CopiesAvaible = cartridgeDb.Copies.Count(copy => copy.Avaible);
            cartridge.CopiesUnavaible = cartridgeDb.Copies.Count - cartridge.CopiesAvaible;
            return cartridge;
        }

        public async Task<bool> UpdateCartridge(CartridgeUpdateRequest request)
        {
            var cartridgeDb = Mapper.Map<Cartridge>(request);
            if (cartridgeDb == null)
                return false;

            Mapper.Map(request, cartridgeDb);

            CartridgeRepo.Update(cartridgeDb);
            await CartridgeRepo.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CartridgeForRentList>> CartridgesForRent()
        {
            var cartridgesDb = await CartridgeRepo
                .GetAll()
                .Include(c => c.Movie)
                .AsNoTracking()
                .ToListAsync();


            var result = new List<CartridgeForRentList>(cartridgesDb.Count);
            foreach (var c in cartridgesDb)
            {
                var cartridgeResult = Mapper.Map<CartridgeForRentList>(c);
                cartridgeResult.MovieCoverUrl = await MovieService.GetMovieCoverUrl(c.MovieId);
                cartridgeResult.CopiesAvaible = await CartridgeCopyRepo
                    .GetAll(cc => cc.CartridgeId == c.Id)
                    .Where(cc => cc.Avaible)
                    .CountAsync();
                cartridgeResult.Language = c.Language;
                result.Add(cartridgeResult);
            }

            return result;
        }

        public async Task<CartridgeForRent> CartridgeForRent(Guid id)
        {
            var cartridgeDb = await CartridgeRepo
                .GetAll()
                .Include(c => c.Movie)
                    .ThenInclude(m => m.Images)
                        .ThenInclude(i => i.Image)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cartridgeDb == null)
                return null;

            var result = Mapper.Map<CartridgeForRent>(cartridgeDb);

            result.Avaible = await CartridgeCopyRepo
                .GetAll(cc => cc.CartridgeId == result.Id)
                .Where(cc => cc.Avaible)
                .CountAsync();

            result.OtherLanguages = await CartridgeRepo
                .GetAll(c => c.MovieId == cartridgeDb.MovieId && c.Id != id)
                .Select(c => new LanguageLink { CartridgeId = c.Id, Language = c.Language })
                .ToListAsync();

            return result;
        }
    
        public async Task<CartridgeRental> CartridgeForRentForm(Guid cartridgeId, Guid userId)
        {
            var userDb = await ShopUserRepo
                .GetAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == userId.ToString());

            if (userDb == null)
                return null;

            var unsettledRentals = await RentalRepo
                .GetAll(r => r.UserId == userDb.Id)
                .Where(r => r.Returned == null)
                .ToListAsync();

            var result = new CartridgeRental();

            if (unsettledRentals.Count >= 3)
            {
                result.UserCanBorrow = false;
                return result;
            }

            if (unsettledRentals.Any(r => DateTime.UtcNow > r.Rented.AddDays(r.DaysToReturn)))
            {
                result.UserCanBorrow = false;
                return result;
            }

            result.UserCanBorrow = true;

            var cartridgeDb = await CartridgeRepo
                .GetAll()
                .AsNoTracking()
                    .Include(c => c.Movie)
                .FirstOrDefaultAsync(c => c.Id == cartridgeId);

            if (cartridgeDb == null)
                return null;

            var copiesAvaible = await CartridgeCopyRepo
                .GetAll(c => c.CartridgeId == cartridgeId)
                .CountAsync(c => c.Avaible);

            result.Avaible = copiesAvaible;
            if (copiesAvaible <= 0)
                return result;

            Mapper.Map(cartridgeDb, result);
            var userAddressesDb = await AddressRepo
                .GetAll(a => a.UserId == userId.ToString() && a.IsActive)
                .ToListAsync();

            result.Addresses = Mapper.Map<IEnumerable<AddressDto>>(userAddressesDb);

            return result;
        }
    }
}
