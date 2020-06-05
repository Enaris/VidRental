using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.Services.Dtos.Request
{
    public class CartridgeUpdateRequest
    {
        public int CopiesToMakeAva { get; set; }
        public int CopiesToMakeUnava { get; set; }
        public int CopiesToAddAva { get; set; }
        public int CopiesToAddUnava { get; set; }
        public string Language { get; set; }
        public int DaysToReturn { get; set; }
        public decimal RentPrice { get; set; }
    }
}
