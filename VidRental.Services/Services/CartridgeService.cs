using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidRental.DataAccess.DbModels;
using VidRental.DataAccess.Repositories;
using VidRental.Services.Dtos.Request;
using VidRental.Services.Dtos.Response.Cartridge;

namespace VidRental.Services.Services
{
    public class CartridgeService : ICartridgeService
    {
        public CartridgeService(
            ICartridgeRepository cartridgeRepo,
            ICartridgeCopyRepository cartridgeCopyRepo,
            IMapper mapper)
        {
            CartridgeRepo = cartridgeRepo;
            CartridgeCopyRepo = cartridgeCopyRepo;
            Mapper = mapper;
        }

        private ICartridgeRepository CartridgeRepo { get; }
        private ICartridgeCopyRepository CartridgeCopyRepo { get; }
        private IMapper Mapper { get; }

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
    }
}
