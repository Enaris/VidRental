using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.Services.Dtos.Request
{
    public class CartridgeAddRequest
    {
        public Guid MovieId { get; set; }        
        public string Language { get; set; }
        public decimal RentPrice { get; set; }
        public int DaysToReturn { get; set; }
        public int AvaibleAmount { get; set; }
        public int UnavaibleAmount { get; set; }
    }
}
