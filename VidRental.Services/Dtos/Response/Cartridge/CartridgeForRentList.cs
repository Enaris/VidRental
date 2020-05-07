using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.Services.Dtos.Response.Cartridge
{
    public class CartridgeForRentList
    {
        public string MovieTitle { get; set; }
        public string MovieDescription { get; set; }
        public string Language { get; set; }
        public int CopiesAvaible { get; set; }
        public string MovieCoverUrl { get; set; }
        public decimal RentPrice { get; set; }

        public Guid Id { get; set; }
    }
}
