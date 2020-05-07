using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VidRental.DataAccess.DbModels
{
    public class Movie
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Director { get; set; }
        public string Description { get; set; }

        public virtual ICollection<MovieImage> Images { get; set; }
        public virtual IEnumerable<Cartridge> Cartridges { get; set; }
    }
}
