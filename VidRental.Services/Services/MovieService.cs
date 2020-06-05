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
                    .GetAll(i => i.MovieId == m.Id && i.ImageType == MovieImageType.Cover)
                    .Include(i => i.Image)
                    .FirstOrDefaultAsync();

                if (thumbnail != null)
                    m.ThumbnailUrl = thumbnail.Image.Url;
            }
            
            return result;
        }

        public async Task<IEnumerable<MovieForDropdown>> GetForDropdown()
        {
            var movies = await MovieRepo
                .GetAll()
                .AsNoTracking()
                .ToListAsync();

            return Mapper.Map<IEnumerable<MovieForDropdown>>(movies);
        }

        public async Task<MovieDetails> GetMovie(string title, DateTime date)
        {
            var movie = await MovieRepo
                .GetAll(m => m.Title == title && m.ReleaseDate == date)
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
        
        public async Task<MovieImageDto> GetMovieCover(Guid movieId)
        {
            var cover = await MovieImageRepo
                .GetAll()
                .AsNoTracking()
                .Include(i => i.Image)
                .FirstOrDefaultAsync(i => i.MovieId == movieId && i.ImageType == MovieImageType.Cover);

            if (cover == null)
                return null;

            return Mapper.Map<MovieImageDto>(cover);
        }

        public async Task<string> GetMovieCoverUrl(Guid movieId)
        {
            var cover = await GetMovieCover(movieId);

            return cover?.Url;
        }
    }
}
