using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VidRental.DataAccess.DbModels;
using VidRental.DataAccess.Repositories;
using VidRental.Services.Dtos.Request;

namespace VidRental.Services.Services
{
    public class ShopUserService : IShopUserService
    {
        public ShopUserService(
            UserManager<User> userManager,
            IShopUserRepository shopUserRepo,
            IMapper mapper
            )
        {
            UserManager = userManager;
            ShopUserRepo = shopUserRepo;
            Mapper = mapper;
        }

        public UserManager<User> UserManager { get; }
        public IShopUserRepository ShopUserRepo { get; }
        public IMapper Mapper { get; }

        public async Task AddShopUser(ShopUserAddRequest addRequest)
        {
            var newShopUser = Mapper.Map<ShopUser>(addRequest);
            await ShopUserRepo.CreateAsync(newShopUser);
            await ShopUserRepo.SaveChangesAsync();
        }
    }
}
