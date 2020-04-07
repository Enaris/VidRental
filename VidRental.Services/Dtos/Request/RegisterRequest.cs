using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.Services.Dtos.Request
{
    public class RegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public bool AddressAdded { get; set; }

        public AddressAddRequest Address { get; set; }
    }

    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(rr => rr.FirstName)
                .NotEmpty();
            RuleFor(rr => rr.LastName)
                .NotEmpty();
            RuleFor(rr => rr.PhoneNumber)
                .Matches(@"^\d{9}$")
                .NotEmpty();
            RuleFor(rr => rr.FirstName)
                .NotEmpty();
            RuleFor(rr => rr.Email)
                .EmailAddress()
                .NotEmpty();
            RuleFor(rr => rr.Password)
                .NotEmpty();

            RuleFor(rr => rr.Address)
                .NotNull()
                .When(rr => rr.AddressAdded);
        }
    }
}
