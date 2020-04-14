using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.DataAccess.DbModels
{
    public class ShopUser
    {
        public string Id { get; set; }

        public bool CanBorrow { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
