using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.Services.Dtos.Response.Address
{
    public class AddressDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }

        public string UserId { get; set; }
    }
}
