using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VidRental.Services.Dtos.Request;
using VidRental.Services.ResponseWrapper;
using VidRental.Services.Services;

namespace VidRental.API.Controllers.Rental
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        public RentalController(IRentalService rentalService)
        {
            RentalService = rentalService;
        }

        private IRentalService RentalService { get; }

        [HttpPost("{rentalId}/updatereturn")]
        public async Task<IActionResult> UpdateDate(Guid rentalId, RentalUpdateDateRequest request)
        {
            var result = await RentalService.UpdateReturnDate(rentalId, request);

            if (!result)
                return BadRequest(ApiResponse.Failure("Return", "Rental does not exist"));

            return Ok(ApiResponse.Success());
        }
    }
}