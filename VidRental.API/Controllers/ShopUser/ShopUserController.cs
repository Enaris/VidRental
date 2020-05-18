using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VidRental.DataAccess.Repositories;
using VidRental.Services.Dtos.Response.Rental;
using VidRental.Services.ResponseWrapper;
using VidRental.Services.Services;

namespace VidRental.API.Controllers.ShopUser
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopUserController : ControllerBase
    {
        public ShopUserController(IShopUserService shopUserService)
        {
            ShopUserService = shopUserService;
        }

        private IShopUserService ShopUserService { get; }

        [HttpGet("{userId}/rentals")]
        public async Task<IActionResult> GetUserRentals(Guid userId)
        {
            var result = await ShopUserService.GetUserRentals(userId);

            if (result == null)
                return BadRequest(ApiResponse.Failure("User", "User does not seem to exist"));

            return Ok(ApiResponse<IEnumerable<RentalBaseInfo>>.Success(result));
        }

    }
}