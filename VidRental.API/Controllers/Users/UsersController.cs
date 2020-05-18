using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VidRental.Services.Dtos.Request;
using VidRental.Services.Dtos.Response.Address;
using VidRental.Services.ResponseWrapper;
using VidRental.Services.Services;

namespace VidRental.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public UsersController(IUsersService usersService,
            IAddressService addressService)
        {
            UsersService = usersService;
            AddressService = addressService;
        }

        private IUsersService UsersService { get; }
        private IAddressService AddressService { get; }

        [HttpGet("{id}/BaseInfo", Name = UsersNames.GetUserBaseInfo)]
        public async Task<IActionResult> GetUserBaseInfo(string id)
        {
            var userBaseInfo = await UsersService.GetUserBaseInfo(id);

            if (userBaseInfo == null)
                return NotFound(ApiResponse.Failure("User", this.NotFoundMessage(id)));

            return Ok(userBaseInfo);
        }

        [HttpGet("{id}/addresses")]
        public async Task<IActionResult> GetUserAddresses(Guid id)
        {
            var userBaseInfo = await UsersService.GetUserBaseInfo(id.ToString());
            if (userBaseInfo == null)
                return BadRequest(ApiResponse.Failure("user", "user does not exist"));

            var addresses = await AddressService.GetUserAddresses(id);

            return Ok(ApiResponse<IEnumerable<AddressDto>>.Success(addresses));
        }

        [HttpPost("deactiveAddress/{addressId}")]
        public async Task<IActionResult> DeactiveAddress(Guid addressId)
        {
            var result = await AddressService.DeactivateAddress(addressId);

            if (!result)
                return BadRequest(ApiResponse.Failure("address", "address does not exist"));

            return Ok(ApiResponse.Success());
        }

        [HttpPost("addAddress")]
        public async Task<IActionResult> AddAddress(AddressAddRequest request)
        {
            var user = await UsersService.GetUserBaseInfo(request.UserId);

            if (user == null)
                return BadRequest(ApiResponse.Failure("user", "user does not exist"));

            await AddressService.CreateAddress(request);

            return Ok(ApiResponse.Success());
        }
    }
}