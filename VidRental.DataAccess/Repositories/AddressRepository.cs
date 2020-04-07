using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VidRental.DataAccess.DataContext;
using VidRental.DataAccess.DbModels;

namespace VidRental.DataAccess.Repositories
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(VidContext context) : base(context) { }

        public async override Task CreateAsync(Address newItem)
        {
            await base.CreateAsync(newItem);
        }
    }
}
