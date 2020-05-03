using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VidRental.Services.Dtos.Request;
using VidRental.Services.Dtos.Response.Cartridge;

namespace VidRental.Services.Services
{
    public interface ICartridgeService
    {
        Task AddCartridge(CartridgeAddRequest request);
        Task<CartridgeDetails> GetCartridge(Guid id);
        Task<IEnumerable<CartridgeForList>> GetForList();
        Task<bool> UpdateCartridge(CartridgeUpdateRequest request);
    }
}