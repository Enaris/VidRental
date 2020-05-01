using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VidRental.DataAccess.DbModels;
using VidRental.Services.Dtos.Request;
using VidRental.Services.Dtos.Request.Images;
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
            IMovieService movieService, 
            IUploadService uploadService,
            IImageService imageService,
            IDeleteImagesService deleteImagesService)
        {
            MovieService = movieService;
            UploadService = uploadService;
            ImageService = imageService;
            DeleteImagesService = deleteImagesService;
        }

        private IMovieService MovieService { get; }
        private IUploadService UploadService { get; }
        private IImageService ImageService { get; }
        private IDeleteImagesService DeleteImagesService { get; }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var movies = await MovieService.GetMovies();

            return Ok(ApiResponse<IEnumerable<MovieForList>>.Success(movies));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var movie = await MovieService.GetMovie(id, true);
            if (movie == null)
                return NotFound(ApiResponse.Failure("Movie", $"Movie with id: {id} does not exist"));

            return Ok(ApiResponse<MovieDetails>.Success(movie));
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateMovie([FromBody] MovieAddRequest request)
        {
            var movieSameTitleAndDate = await MovieService.GetMovie(request.Title, request.ReleaseDate);
            if (movieSameTitleAndDate != null)
                return BadRequest(ApiResponse.Failure("Movie", $"Movie {request.Title} released: {request.ReleaseDate.Date} exists"));

            var addedMovie = await MovieService.AddMovie(request);

            return Ok(ApiResponse<MovieDetails>.Success(addedMovie));
        }

        [HttpPost("{id}/update")]
        public async Task<IActionResult> UpdateMovie([FromForm] MovieUpdateRequest request)
        {
            var movie = await MovieService.GetMovie(request.Id, true);
            if (movie == null)
                return NotFound(ApiResponse.Failure("Movie", $"Movie with id: {request.Id} does not exist"));

            if (request.RemovedImages?.Any() ?? false)
            {
                var imageUrls = await ImageService.RemoveMovieImages(request.RemovedImages);

                var removed = DeleteImagesService.DeleteImages(imageUrls);
            }

            var imagesToUpload = new List<ImageUpRequest>();
            if (request.NewImages?.Any() ?? false)
            {
                var imgsAdded = request.NewImages
                    .Select(i => new ImageUpRequest { Image = i, ImageType = MovieImageType.Image }).ToList();
                imagesToUpload.AddRange(imgsAdded);
            }
            if (request.NewCover?.Length > 0)
                imagesToUpload.Add(new ImageUpRequest { Image = request.NewCover, ImageType = MovieImageType.Cover });
            
            if (imagesToUpload.Any())
            {
                var uploadedImages = await UploadService.UploadImages(imagesToUpload);
                await ImageService.AddMovieImages(uploadedImages, movie.Id);
            }

            await MovieService.UpdateMovie(request);

            return Ok(ApiResponse.Success());
        }
    }
}