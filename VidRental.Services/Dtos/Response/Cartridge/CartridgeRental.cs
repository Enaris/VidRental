using System;
using System.Collections.Generic;
using System.Text;
using VidRental.Services.Dtos.Response.Address;
using VidRental.Services.Dtos.Response.Rental;

namespace VidRental.Services.Dtos.Response.Cartridge
{
    public class CartridgeRental
    {
        public Guid Id { get; set; }
        
        public string MovieTitle { get; set; }
        public string Language { get; set; }
        public int Avaible { get; set; }
        public double RentPrice { get; set; }
        public int DaysToReturn { get; set; }
        public int MovieReleaseYear { get; set; }
        public IEnumerable<AddressDto> Addresses { get; set; }

        public bool UserCanBorrow { get; set; }
    }
}
