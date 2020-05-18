using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
    public class ShopUserService : IShopUserService
    {
        public ShopUserService(
            UserManager<User> userManager,
            IShopUserRepository shopUserRepo,
            IMapper mapper,
            IRentalRepository rentalRepo
            )
        {
            UserManager = userManager;
            ShopUserRepo = shopUserRepo;
            Mapper = mapper;
            RentalRepo = rentalRepo;
        }

        private UserManager<User> UserManager { get; }
        private IShopUserRepository ShopUserRepo { get; }
        private IMapper Mapper { get; }
        private IRentalRepository RentalRepo { get; }

        public async Task AddShopUser(ShopUserAddRequest addRequest)
        {
            var newShopUser = Mapper.Map<ShopUser>(addRequest);
            await ShopUserRepo.CreateAsync(newShopUser);
            await ShopUserRepo.SaveChangesAsync();
        }

        public async Task<bool?> CanUserRent(Guid userId)
        {
            var userDb = await ShopUserRepo
                .GetAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == userId.ToString());

            if (userDb == null)
                return null;

            var unsettledRentals = await RentalRepo
                .GetAll(r => r.ShopUserId == userDb.Id)
                .Where(r => r.Returned == null)
                .ToListAsync();

            if (unsettledRentals.Count >= 3)
                return false;

            if (unsettledRentals.Any(r => DateTime.UtcNow > r.Rented.AddDays(r.DaysToReturn)))
                return false;

            return true;
        }
    
        public async Task<IEnumerable<RentalBaseInfo>> GetUserRentals(Guid userId)
        {
            var shopUserDb = await ShopUserRepo
                .GetAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == userId.ToString());

            if (shopUserDb == null)
                return null;

            var rentals = await RentalRepo
                .GetAll(r => r.ShopUserId == shopUserDb.Id)
                .Include(r => r.CartridgeCopy)
                    .ThenInclude(cc => cc.Cartridge)
                        .ThenInclude(c => c.Movie)
                .ToListAsync();

            if (rentals.Count == 0)
                return new List<RentalBaseInfo>();

            var result = rentals.Select(r =>
            {
                var rentalBaseInfo = Mapper.Map<RentalBaseInfo>(r);
                rentalBaseInfo.MovieLanguage = r.CartridgeCopy.Cartridge.Language;
                rentalBaseInfo.MovieTitle = r.CartridgeCopy.Cartridge.Movie.Title;
                rentalBaseInfo.MovieReleaseYear = r.CartridgeCopy.Cartridge.Movie.ReleaseDate.Year;
                return rentalBaseInfo;
            });

            return result;
        }
    }
}
