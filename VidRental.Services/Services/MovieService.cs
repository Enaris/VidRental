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
            IMovieImageRepository imageMovieRepo,
            IMapper mapper)
        {
            MovieRepo = movieRepo;
            MovieImageRepo = imageMovieRepo;
            Mapper = mapper;
        }

        private IMovieRepository MovieRepo { get; }
        private IMovieImageRepository MovieImageRepo { get; }
        private IMapper Mapper { get; }

        public async Task<MovieDetails> AddMovie(MovieAddRequest request)
        {           
            var movieToAdd = Mapper.Map<Movie>(request);

            await MovieRepo.CreateAsync(movieToAdd);
            await MovieRepo.SaveChangesAsync();

            var addedMovie = Mapper.Map<MovieDetails>(movieToAdd);

            return addedMovie;
        }

        public async Task<bool> UpdateMovie(MovieUpdateRequest request)
        {
            var movie = await MovieRepo
                .GetAll()
                .FirstOrDefaultAsync(m => m.Id == request.Id);

            if (movie == null)
                return false;

            Mapper.Map(request, movie);

            MovieRepo.Update(movie);
            await MovieRepo.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MovieForList>> GetMovies()
        {
            var movies = await MovieRepo
                .GetAll()
                .ToListAsync();

            var result = Mapper.Map<IEnumerable<MovieForList>>(movies);

            foreach (var m in result)
            {
                var thumbnail = await MovieImageRepo
                    .GetAll(i => i.MovieId == m.Id && i.ImageType == MovieImageType.Thumbnail)
                    .Include(i => i.Image)
                    .FirstOrDefaultAsync();

                if (thumbnail != null)
                {
                    var thumbnailDto = Mapper.Map<MovieImageDto>(thumbnail);
                    m.Thumbnail = thumbnailDto;
                }
            }

            //result.ToList().ForEach(async m =>
            //{
            //    var thumbnail = await ImageMovieRepo
            //        .GetAll(i => i.MovieId == m.Id && i.ImageType == MovieImageType.Thumbnail)
            //        .Include(i => i.Image)
            //        .FirstOrDefaultAsync();

            //    if (thumbnail != null)
            //    {
            //        var thumbnailDto = Mapper.Map<MovieImageDto>(thumbnail);
            //        m.Thumbnail = thumbnailDto;
            //    }
            //});
            
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

        public async Task<MovieDetails> GetMovie(Guid id, bool noTracking = false)
        {
            var movies = MovieRepo
                .GetAll()
                .Include(m => m.Images)
                    .ThenInclude(i => i.Image);

            if (noTracking)
                movies.AsNoTracking();

            var movie = await movies.FirstOrDefaultAsync(m => m.Id == id);
            
            var result = Mapper.Map<MovieDetails>(movie);

            return result;
        }
        
    }
}
