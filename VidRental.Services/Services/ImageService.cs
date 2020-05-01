using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidRental.DataAccess.DbModels;
using VidRental.DataAccess.Repositories;
using VidRental.Services.Dtos.Response.Images;

namespace VidRental.Services.Services
{
    public class ImageService : IImageService
    {
        public ImageService(IImageRepository imageRepo,
            IMovieImageRepository movieImageRepo)
        {
            ImageRepo = imageRepo;
            MovieImageRepo = movieImageRepo;
        }

        private IImageRepository ImageRepo { get; }
        private IMovieImageRepository MovieImageRepo { get; }

        public async Task AddMovieImage(string url, Guid movieId, MovieImageTypeEnum type)
        {
            var image = new Image { Url = url };
            await ImageRepo.CreateAsync(image);
            await ImageRepo.SaveChangesAsync();
            var movieImage = new MovieImage { ImageId = image.Id, MovieId = movieId, ImageType = MovieImageType.ToStr(type) };
            await MovieImageRepo.CreateAsync(movieImage);
            await MovieImageRepo.SaveChangesAsync();
        }

        public async Task AddMovieImages(IEnumerable<string> images, Guid movieId)
        {
            var imagesToAdd = images.Select(i => new Image { Url = i });
            await ImageRepo.AddRangeAsync(imagesToAdd);
            await ImageRepo.SaveChangesAsync();
            var movieImages = imagesToAdd.Select(i => new MovieImage { ImageId = i.Id, MovieId = movieId, ImageType = MovieImageType.Image });
            await MovieImageRepo.AddRangeAsync(movieImages);
            await MovieImageRepo.SaveChangesAsync();
        }

        public async Task AddMovieImages(IEnumerable<UploadedImage> images, Guid movieId)
        {
            var imagesToAdd = images.Select(i => new Image { Url = i.Filepath }).ToList();
            for (int i = 0; i < imagesToAdd.Count; ++i)
            {
                await ImageRepo.CreateAsync(imagesToAdd[i]);
                await ImageRepo.SaveChangesAsync();
            }

            var movieImages = new List<MovieImage>(imagesToAdd.Count());
            foreach (var img in imagesToAdd)
            {
                var type = images.FirstOrDefault(i => i.Filepath == img.Url)?.ImageType;

                var movieImage = new MovieImage
                {
                    MovieId = movieId,
                    ImageType = type,
                    ImageId = img.Id
                };

                movieImages.Add(movieImage);
            }

            await MovieImageRepo.AddRangeAsync(movieImages);
            await MovieImageRepo.SaveChangesAsync();
        }

        /// <summary>
        /// Removes movie image entity and image entity
        /// and if there are no other image entities 
        /// that points to the deleted image entity 
        /// method returns underlying image url 
        /// otherwise null is returned
        /// </summary>
        /// <param name="movieImageId">MovieImage entity id</param>
        /// <returns>Underlying image url or null</returns>
        public async Task<string> RemoveMovieImage(Guid movieImageId)
        {
            var movieImage = await MovieImageRepo
                .GetAll()
                .FirstOrDefaultAsync(mi => mi.Id == movieImageId);
            if (movieImage == null)
                return null;
            var image = await ImageRepo
                .GetAll()
                .FirstOrDefaultAsync(i => i.Id == movieImage.ImageId);
            var otherImagesWithSameUrl = await ImageRepo
                .GetAll(i => i.Url == image.Url && i.Id != image.Id)
                .ToListAsync();

            MovieImageRepo.Delete(movieImage);
            await MovieImageRepo.SaveChangesAsync();
            ImageRepo.Delete(image);
            await ImageRepo.SaveChangesAsync();
            return otherImagesWithSameUrl.Count > 0 ? null : image.Url;
        }

        /// <summary>
        /// Removes movie image entities with underlying image entities
        /// and if there are no other image entities 
        /// that points to the deleted image entity 
        /// method returns underlying image urls
        /// </summary>
        /// <param name="movieImageId">MovieImage entity ids</param>
        /// <returns>List of underlying image url</returns>
        public async Task<IEnumerable<string>> RemoveMovieImages(IEnumerable<Guid> movieImageIds)
        {
            var result = new List<string>();
            foreach (var miId in movieImageIds)
            {
                var url = await RemoveMovieImage(miId);
                if (url == null)
                    continue;
                result.Add(url);
            }
            return result;
        }
    }
}
