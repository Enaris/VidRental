using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using VidRental.Services.Dtos.Request.Images;
using VidRental.Services.Dtos.Response.Image;

namespace VidRental.Services.Dtos.Request
{
    public class MovieUpdateRequest
    {
        public IEnumerable<Guid> RemovedImages { get; set; }
        public IEnumerable<IFormFile> NewImages { get; set; }
        public IFormFile NewCover { get; set; }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Description { get; set; }
    }
}
