using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.Services.Dtos.Response.Image
{
    public class MovieImageDto
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }

        public Guid MovieId { get; set; }
        public Guid ImageId { get; set; }
    }
}
