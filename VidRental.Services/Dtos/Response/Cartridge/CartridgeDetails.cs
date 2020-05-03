using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.Services.Dtos.Response.Cartridge
{
    public class CartridgeDetails
    {
        public Guid Id { get; set; }
        public Guid MovieId { get; set; }
        public string MovieTitle { get; set; }
        public string Language { get; set; }
        public int DaysToReturn { get; set; }
        public int CopiesAvaible { get; set; }
        public int CopiesUnavaible { get; set; }
        public int CopiesRented { get; set; }
        public decimal RentPrice { get; set; }
    }
}
