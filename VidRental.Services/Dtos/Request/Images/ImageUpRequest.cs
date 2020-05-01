using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.Services.Dtos.Request.Images
{
    public class ImageUpRequest
    {
        public IFormFile Image { get; set; }
        public string ImageType { get; set; }
    }

    public class ImageUpRequestValidator : AbstractValidator<ImageUpRequest>
    {
        public ImageUpRequestValidator()
        {
            RuleFor(r => r.Image)
                .NotNull()
                .Must(f => f.Length < KbHelper.Mb(2)).WithMessage("Image is too large");
            RuleFor(r => r.ImageType)
                .NotEmpty().WithMessage("Image type not provided");
        }
    }

    public static class KbHelper
    {
        /// <summary>
        /// Converts mb to b
        /// </summary>
        /// <param name="mb">Desired mb</param>
        /// <returns>Equivalent in kb</returns>
        public static int Mb(int mb) => mb * 1024 * 1024;
    }
}
