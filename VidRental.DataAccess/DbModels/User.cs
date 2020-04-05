using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.DataAccess.DbModels
{
    public class User : IdentityUser
    {
        public virtual ICollection<DeliveryAddress> Addresses { get; set; }
    }
}
