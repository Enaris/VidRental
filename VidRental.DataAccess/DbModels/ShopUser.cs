using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VidRental.DataAccess.DbModels
{
    public class ShopUser
    {
        public Guid Id { get; set; }

        public bool CanBorrow { get; set; }
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
