using System;
using System.Collections.Generic;
using System.Text;
using VidRental.DataAccess.DataContext;
using VidRental.DataAccess.DbModels;

namespace VidRental.DataAccess.Repositories
{
    public class ShopUserRepository : BaseRepository<ShopUser>, IShopUserRepository
    {
        public ShopUserRepository(VidContext context) : base(context)
        {
        }
    }
}
