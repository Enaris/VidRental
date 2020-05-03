using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.DataAccess.DbModels
{
    public class Rental
    {
        public Guid Id { get; set; }

        public DateTime Rented { get; set; }
        public DateTime? Returned { get; set; }
        
        public CartridgeCopy CartridgeCopy { get; set; }
        public Guid CartridgeCopyId { get; set; }
        public ShopUser User { get; set; }
        public Guid UserId { get; set; }
    }
}
