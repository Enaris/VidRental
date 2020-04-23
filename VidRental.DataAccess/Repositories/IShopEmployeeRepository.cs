using System;
using System.Linq;
using System.Threading.Tasks;
using VidRental.DataAccess.DbModels;

namespace VidRental.DataAccess.Repositories
{
    public interface IShopEmployeeRepository : IBaseRepository<ShopEmployee>
    {
        Task<ShopEmployee> GetWithAspUser(Guid id);
        IQueryable<ShopEmployee> GetWithAspUser();
    }
}