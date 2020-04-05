using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.DataAccess.DbModels
{
    public class DeliveryAddress
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }

        public User User { get; set; }
    }
}
