using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VidRental.Services.Dtos.Request;
using VidRental.Services.Dtos.Response.Movie;
using VidRental.Services.ResponseWrapper;
using VidRental.Services.Services;

namespace VidRental.API.Controllers.Movie
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        public MovieController(
            IMovieService movieService)
        {
            MovieService = movieService;
        }

        private IMovieService MovieService { get; }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var movies = await MovieService.GetMovies();

            return Ok(ApiResponse<IEnumerable<MovieForList>>.Success(movies));
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] MovieAddRequest request)
        {
            var movieSameTitleAndDate = await MovieService.GetMovie(request.Title, request.ReleaseDate);
            if (movieSameTitleAndDate != null)
                return BadRequest(ApiResponse.Failure("Movie", $"Movie {request.Title} released: {request.ReleaseDate.Date} exists"));

            var addedMovie = await MovieService.AddMovie(request);

            return Ok(ApiResponse<MovieDetails>.Success(addedMovie));
        }
    }
}