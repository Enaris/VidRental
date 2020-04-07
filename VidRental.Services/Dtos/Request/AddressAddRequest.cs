using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.Services.Dtos.Request
{
    public class AddressAddRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string UserId { get; set; }
    }
    public class AddressAddRequestValidator : AbstractValidator<AddressAddRequest>
    {
        public AddressAddRequestValidator()
        {
            RuleFor(ar => ar.FirstName)
                .NotEmpty();
            RuleFor(ar => ar.LastName)
                .NotEmpty();
            RuleFor(ar => ar.City)
                .NotEmpty();
            RuleFor(ar => ar.Street)
                .NotEmpty();
            RuleFor(ar => ar.PhoneNumber)
                .NotEmpty()
                .Matches(@"^\d{9}$").WithMessage("Invalid phone number");
            RuleFor(ar => ar.ZipCode)
                .NotEmpty()
                .Matches(@"^\d{5}$").WithMessage("Invalid zip code");
        
        }
    }
}
