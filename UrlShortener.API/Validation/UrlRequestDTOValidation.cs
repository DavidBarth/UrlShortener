using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Shared.DTO.API;

namespace UrlShortener.API.Validation
{
    public class UrlRequestDTOValidation : AbstractValidator<UrlRequestDTO>
    {
        public UrlRequestDTOValidation()
        {
            RuleFor(x => x.LongUrl)
                .NotEmpty().WithMessage("URL is required")
                .Must(url => UrlShortener.Shared.Utils.FormatUrl.ValidUri(url.ToString())).WithMessage("Enter a valid URL");

            RuleFor(x => x.Expiration)
                .GreaterThan(DateTime.Now).WithMessage("Expiration date must be higher than today");
        }

    }
}
