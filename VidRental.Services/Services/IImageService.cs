using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VidRental.DataAccess.DbModels;
using VidRental.Services.Dtos.Response.Images;

namespace VidRental.Services.Services
{
    public interface IImageService
    {
        Task AddMovieImage(string url, Guid movieId, MovieImageTypeEnum type);
        Task AddMovieImages(IEnumerable<string> images, Guid movieId);
        Task AddMovieImages(IEnumerable<UploadedImage> images, Guid movieId);
        Task<string> RemoveMovieImage(Guid movieImageId);
        Task<IEnumerable<string>> RemoveMovieImages(IEnumerable<Guid> movieImageIds);
    }
}