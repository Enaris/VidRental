using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.Services.Dtos.Response.Movie
{
    public class MovieForDropdown
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
    }
}
