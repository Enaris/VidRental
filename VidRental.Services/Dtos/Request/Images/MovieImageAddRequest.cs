using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.Services.Dtos.Request.Images
{
    public class MovieImageAddRequest
    {
        public string Url { get; set; }
        public string ImageType { get; set; }
    }
}
