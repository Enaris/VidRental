using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.Services.Dtos.Request
{
    public class RegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public bool AddressAdded { get; set; }

        public AddressAddRequest Address { get; set; }
    }
}
