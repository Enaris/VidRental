using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.Services.Dtos.Request
{
    public class ShopUserAddRequest
    {
        public bool CanBorrow { get; set; }
        public string UserId { get; set; }
    }

    public class ShopUserAddRequestValidator : AbstractValidator<ShopUserAddRequest>
    {
        public ShopUserAddRequestValidator()
        {
            RuleFor(rr => rr.UserId)
                .NotEmpty();
        }
    }
}
