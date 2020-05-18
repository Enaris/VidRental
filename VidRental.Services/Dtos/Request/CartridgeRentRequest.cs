using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.Services.Dtos.Request
{
    public class CartridgeRentRequest
    {
        public bool AddAddress { get; set; }
        public AddressAddRequest NewAddress { get; set; }
        public string AddressId { get; set; }
        public string Delivery { get; set; }
    }
}
