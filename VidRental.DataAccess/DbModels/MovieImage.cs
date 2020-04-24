using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VidRental.DataAccess.DbModels
{
    public class MovieImage
    {
        public Guid Id { get; set; }

        [Required]
        public string ImageType { get; set; }

        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
        public Guid ImageId { get; set; }
        public Image Image { get; set; }
    }
}
