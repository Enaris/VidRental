using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.Services.Dtos.Request
{
    public class EmployeeAddRequest
    {
        public bool IsActive { get; set; }
        public string UserId { get; set; }
    }

    public class EmployeeAddRequestValidator : AbstractValidator<EmployeeAddRequest>
    {
        public EmployeeAddRequestValidator()
        {
            RuleFor(rr => rr.UserId)
                .NotEmpty();
        }
    }
}
