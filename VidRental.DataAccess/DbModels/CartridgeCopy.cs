using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.DataAccess.DbModels
{
    public class CartridgeCopy
    {
        public Guid Id { get; set; }

        public Cartridge Cartridge { get; set; }
        public Guid CartridgeId { get; set; }

        public bool Avaible { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
