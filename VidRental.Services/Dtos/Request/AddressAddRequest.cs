using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.Services.Dtos.Request
{
    public class AddressAddRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string UserId { get; set; }
    }
}
