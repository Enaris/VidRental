using System;
using System.Collections.Generic;
using System.Text;
using VidRental.Services.Dtos.Response.Image;

namespace VidRental.Services.Dtos.Response.Movie
{
    public class MovieForList
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime RealeaseDate { get; set; }
        public string Director { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Description { get; set; }
    }
}
