using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VidRental.DataAccess.DbModels
{
    public class Rental
    {
        public Guid Id { get; set; }

        public DateTime Rented { get; set; }
        public DateTime? Returned { get; set; }
        
        [Required]
        public string Delivery { get; set; }
        [Column(TypeName = "money")]
        [DataType(DataType.Currency)]
        public decimal RentPrice { get; set; }
        public bool? ReturnedOnTime { get; set; }
        public int DaysToReturn { get; set; }

        public CartridgeCopy CartridgeCopy { get; set; }
        public Guid CartridgeCopyId { get; set; }
        public ShopUser User { get; set; }
        public Guid UserId { get; set; }
        public Address Address { get; set; }
        public Guid? AddressId { get; set; }
    }
}
