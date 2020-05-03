using System;
using System.Collections.Generic;
using System.Text;
using VidRental.DataAccess.DataContext;
using VidRental.DataAccess.DbModels;

namespace VidRental.DataAccess.Repositories
{
    public class CartridgeCopyRepository : BaseRepository<CartridgeCopy>, ICartridgeCopyRepository
    {
        public CartridgeCopyRepository(VidContext context) : base(context)
        {
        }
    }
}
