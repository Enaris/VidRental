using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VidRental.Services.Dtos.Request;
using VidRental.Services.Dtos.Response.Cartridge;
using VidRental.Services.ResponseWrapper;
using VidRental.Services.Services;

namespace VidRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartridgeController : ControllerBase
    {
        public CartridgeController(
            ICartridgeService cartridgeService,
            IMovieService movieService, 
            IShopUserService shopUserService, 
            IRentalService rentalService)
        {
            CartridgeService = cartridgeService;
            MovieService = movieService;
            ShopUserService = shopUserService;
            RentalService = rentalService;
        }

        private ICartridgeService CartridgeService { get; }
        private IMovieService MovieService { get; }
        private IShopUserService ShopUserService { get; }
        private IRentalService RentalService { get; }

        [HttpPost]
        public async Task<IActionResult> Create(CartridgeAddRequest request)
        {
            var movieDb = MovieService.GetMovie(request.MovieId, true);
            if (movieDb == null)
                return BadRequest(ApiResponse.Failure("Movie", $"Movie with id: {request.MovieId} does not exist"));

            await CartridgeService.AddCartridge(request);
            return Ok(ApiResponse.Success());
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await CartridgeService.GetForList();

            return Ok(ApiResponse<IEnumerable<CartridgeForList>>.Success(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await CartridgeService.GetEditDetails(id);

            if (result == null)
                return NotFound(ApiResponse.Failure("Cartridge", "Not Found"));

            return Ok(ApiResponse<CartridgeEditDetails>.Success(result));
        }

        [HttpPost("{id}/update")]
        public async Task<IActionResult> UpdateCartridge(Guid id, CartridgeUpdateRequest request)
        {
            var result = await CartridgeService.UpdateCartridge(id, request);

            if (!result)
                return NotFound(ApiResponse.Failure("Update", $"Cartridge with id ${id} does not exist"));

            return Ok(ApiResponse.Success());
        }

        [HttpGet("rentList")]
        public async Task<IActionResult> CartridgesForRent()
        {
            var result = await CartridgeService.CartridgesForRent();

            return Ok(ApiResponse<IEnumerable<CartridgeForRentList>>.Success(result));
        }

        [HttpGet("{id}/forRent")]
        public async Task<IActionResult> CartridgeForRent(Guid id)
        {
            var result = await CartridgeService.CartridgeForRent(id);

            if (result == null)
                return BadRequest(ApiResponse.Failure("Cartridge", "Not found"));

            return Ok(ApiResponse<CartridgeForRent>.Success(result));
        }

        [HttpGet("{cartridgeId}/rentFormFor/{userId}")]
        public async Task<IActionResult> CartridgeRentForm(Guid cartridgeId, Guid userId)
        {
            var result = await CartridgeService.CartridgeForRentForm(cartridgeId, userId);

            if (result == null)
                return NotFound(ApiResponse.Failure("Rental", "Cartride does not seem to exist"));

            if (!result.UserCanBorrow)
                return BadRequest(ApiResponse.Failure("User", "User cannot borrow this cartridge"));

            if (result.Avaible == 0)
                return BadRequest(ApiResponse.Failure("NoItems", "No avaible cartridge copise"));

            return Ok(ApiResponse<CartridgeRental>.Success(result));
        }

        [HttpPost("{cartridgeId}/rent/{userId}")]
        public async Task<IActionResult> RentCartridge(Guid cartridgeId, Guid userId, [FromBody] CartridgeRentRequest request)
        {
            var canUserRent = await ShopUserService.CanUserRent(userId);

            if (canUserRent == null)
                return BadRequest(ApiResponse.Failure("Null", "User does not seem to exist"));

            if (!canUserRent.Value)
                return BadRequest(ApiResponse.Failure("CantRent", "User cannot rent"));

            var rentalResult = await RentalService.RentCartridge(cartridgeId, userId, request);

            if (rentalResult == null)
                return BadRequest(ApiResponse.Failure("Null", "Cartridge or selected address does not seemt to exist"));

            if (!rentalResult.Value)
                return BadRequest(ApiResponse.Failure("NoCopies", "No copies avaible"));

            return Ok(ApiResponse.Success());
        }
    }
}