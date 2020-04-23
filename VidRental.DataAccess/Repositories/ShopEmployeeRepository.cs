using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidRental.DataAccess.DataContext;
using VidRental.DataAccess.DbModels;

namespace VidRental.DataAccess.Repositories
{
    public class ShopEmployeeRepository : BaseRepository<ShopEmployee>, IShopEmployeeRepository
    {
        public ShopEmployeeRepository(VidContext context) : base(context)
        {
        }

        public async Task<ShopEmployee> GetWithAspUser(Guid id)
        {
            var employeeDb = await _context
                .ShopEmployees
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.Id == id);

            return employeeDb;
        }

        public IQueryable<ShopEmployee> GetWithAspUser()
        {
            var employees = _context
                .ShopEmployees
                .Include(e => e.User);

            return employees;
        }
    }
}
