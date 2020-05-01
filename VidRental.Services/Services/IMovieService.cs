using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VidRental.Services.Dtos.Request;
using VidRental.Services.Dtos.Response.Movie;

namespace VidRental.Services.Services
{
    public interface IMovieService
    {
        Task<MovieDetails> AddMovie(MovieAddRequest request);
        Task<IEnumerable<MovieForList>> GetMovies();
        Task<MovieDetails> GetMovie(string title, DateTime date);
        Task<MovieDetails> GetMovie(Guid id, bool noTracking = false);
        Task<bool> UpdateMovie(MovieUpdateRequest request);
    }
}