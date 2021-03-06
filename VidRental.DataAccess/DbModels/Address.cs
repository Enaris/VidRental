﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VidRental.DataAccess.DbModels
{
    public class Address
    {
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string City { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
