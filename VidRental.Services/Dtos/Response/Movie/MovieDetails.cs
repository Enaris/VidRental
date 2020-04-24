using System;
using System.Collections.Generic;
using System.Text;
using VidRental.Services.Dtos.Response.Image;

namespace VidRental.Services.Dtos.Response.Movie
{
    public class MovieDetails
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime RealeaseDate { get; set; }
        public string Director { get; set; }
        public IEnumerable<MovieImageDto> Images { get; set; }
        public string Description { get; set; }
    }
}
