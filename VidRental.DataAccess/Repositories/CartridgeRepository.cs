using System;
using System.Collections.Generic;
using System.Text;
using VidRental.DataAccess.DataContext;
using VidRental.DataAccess.DbModels;

namespace VidRental.DataAccess.Repositories
{
    public class CartridgeRepository : BaseRepository<Cartridge>, ICartridgeRepository
    {
        public CartridgeRepository(VidContext context) : base(context)
        {
        }
    }
}
