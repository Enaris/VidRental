using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.Services.Dtos.Response.Rental
{
    public class RentalBaseInfo
    {
        public Guid Id { get; set; }

        public DateTime Rented { get; set; }
        public DateTime? Returned { get; set; }
        public bool? ReturnedOnTime { get; set; }

        public string MovieTitle { get; set; }
        public int MovieReleaseYear { get; set; }
        public string MovieLanguage { get; set; }

        public Guid UserId { get; set; }
        public Guid CartridgeCopyId { get; set; }
    }
}
