using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.Services.Dtos.Request
{
    public class MovieAddRequest
    {
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Director { get; set; }
        public string Description { get; set; }
    }

    public class MovieAddRequestValidator : AbstractValidator<MovieAddRequest>
    {
        public MovieAddRequestValidator()
        {
            RuleFor(r => r.Title)
                .NotEmpty();
            RuleFor(r => r.ReleaseDate)
                .LessThan(DateTime.UtcNow);
            RuleFor(r => r.Description)
                .MaximumLength(4096);
        }
    }
}
