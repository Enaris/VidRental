using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VidRental.DataAccess.DbModels
{
    public class ShopEmployee
    {
        public Guid Id { get; set; }

        public bool IsActive { get; set; }
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
