using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.DataAccess.DbModels
{
    public class ShopEmployee
    {
        public string Id { get; set; }

        public bool IsActive { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
