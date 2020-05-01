using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using VidRental.Services.Dtos.Request.Images;
using VidRental.Services.Dtos.Response.Images;

namespace VidRental.Services.Services
{
    public interface IUploadService
    {
        Task<UploadedImage> UploadImage(ImageUpRequest file);
        Task<IEnumerable<UploadedImage>> UploadImages(IEnumerable<ImageUpRequest> files);
    }
}