using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidRental.DataAccess.DbModels;
using VidRental.DataAccess.Repositories;
using VidRental.Services.Dtos.Request;
using VidRental.Services.Dtos.Response.Image;
using VidRental.Services.Dtos.Response.Movie;

namespace VidRental.Services.Services
{
    public class MovieService : IMovieService
    {
        public MovieService(IMovieRepository movieRepo,
            IImageRepository imageRepo,
            IMovieImageRepository imageMovieRepo,
            IMapper mapper)
        {
            MovieRepo = movieRepo;
            ImageRepo = imageRepo;
            ImageMovieRepo = imageMovieRepo;
            Mapper = mapper;
        }

        private IMovieRepository MovieRepo { get; }
        private IImageRepository ImageRepo { get; }
        private IMovieImageRepository ImageMovieRepo { get; }
        private IMapper Mapper { get; }

        public async Task<MovieDetails> AddMovie(MovieAddRequest request)
        {           
            var movieToAdd = Mapper.Map<Movie>(request);

            await MovieRepo.CreateAsync(movieToAdd);
            await MovieRepo.SaveChangesAsync();

            var addedMovie = Mapper.Map<MovieDetails>(movieToAdd);

            if (request.Images.Any())
                await AddMovieImages(request.Images, addedMovie.Id);

            if (!string.IsNullOrWhiteSpace(request.Thumbnail))
                await AddImage(request.Thumbnail, addedMovie.Id, MovieImageTypeEnum.Thumbnail);

            if (!string.IsNullOrWhiteSpace(request.CoverImage))
                await AddImage(request.CoverImage, addedMovie.Id, MovieImageTypeEnum.Cover);

            return addedMovie;
        }

        public async Task<IEnumerable<MovieForList>> GetMovies()
        {
            var movies = await MovieRepo
                .GetAll()
                .ToListAsync();

            var result = Mapper.Map<IEnumerable<MovieForList>>(movies);

            result.ToList().ForEach(async m =>
            {
                var thumbnail = await ImageMovieRepo
                    .GetAll(i => i.MovieId == m.Id && i.ImageType == MovieImageType.Thumbnail)
                    .Include(i => i.Image)
                    .FirstOrDefaultAsync();

                if (thumbnail != null)
                {
                    var thumbnailDto = Mapper.Map<MovieImageDto>(thumbnail);
                    m.Thumbnail = thumbnailDto;
                }
            });
            
            return result;
        }

        public async Task<MovieDetails> GetMovie(string title, DateTime date)
        {
            var movie = await MovieRepo
                .GetAll(m => m.Title == title && m.RealeaseDate == date)
                .FirstOrDefaultAsync();

            var result = Mapper.Map<MovieDetails>(movie);

            return result;
        }

        private async Task AddImage(string url, Guid movieId, MovieImageTypeEnum type)
        {
            var image = new Image { Url = url };
            await ImageRepo.CreateAsync(image);
            await ImageRepo.SaveChangesAsync();
            var movieImage = new MovieImage { ImageId = image.Id, MovieId = movieId, ImageType = MovieImageType.ToStr(type) };
            await ImageMovieRepo.CreateAsync(movieImage);
            await ImageMovieRepo.SaveChangesAsync();
        }

        private async Task AddMovieImages(IEnumerable<string> images, Guid movieId)
        {
            var imagesToAdd = images.Select(i => new Image { Url = i });
            await ImageRepo.AddRangeAsync(imagesToAdd);
            await ImageRepo.SaveChangesAsync();
            var movieImages = imagesToAdd.Select(i => new MovieImage { ImageId = i.Id, MovieId = movieId, ImageType = MovieImageType.Image });
            await ImageMovieRepo.AddRangeAsync(movieImages);
            await ImageMovieRepo.SaveChangesAsync();
        }
    }
}
