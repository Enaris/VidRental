using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.Services.Dtos.Response.Rental
{
    public class RentalForAdminList
    {
        public Guid Id { get; set; }

        public DateTime Rented { get; set; }
        public DateTime? Returned { get; set; }
        public bool? ReturnedOnTime { get; set; }

        public string MovieTitle { get; set; }
        public int MovieReleaseYear { get; set; }
        public string MovieLanguage { get; set; }
        public int DaysToReturn { get; set; }
        public decimal RentPrice { get; set; }

        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserPhone { get; set; }


        public Guid UserId { get; set; }
        public Guid CartridgeCopyId { get; set; }
    }
}
