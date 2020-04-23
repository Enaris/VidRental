using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.Services.Dtos.Request
{
    public class EmployeeAddRequestFlat
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }

    public class EmployeeAddRequestFlatValidator : AbstractValidator<EmployeeAddRequestFlat>
    {
        public EmployeeAddRequestFlatValidator()
        {
            RuleFor(rr => rr.FirstName)
                .NotEmpty();
            RuleFor(rr => rr.LastName)
                .NotEmpty();
            RuleFor(rr => rr.PhoneNumber)
                .Matches(@"^\d{9}$")
                .NotEmpty();
            RuleFor(rr => rr.Email)
                .EmailAddress()
                .NotEmpty();
            RuleFor(rr => rr.Password)
                .NotEmpty();
        }
    }
}
