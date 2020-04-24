using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VidRental.DataAccess.DbModels
{
    public class Cartridge
    {
        public Guid Id { get; set; }
        [Required]
        public string Language { get; set; }
        public bool Avaible { get; set; }
        [Column(TypeName = "money")]
        [DataType(DataType.Currency)]
        public decimal RentPrice { get; set; }
        public int DaysToReturn { get; set; }
        public Image Image { get; set; }

        [Required]
        public string MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
