using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using FluentValidation;

namespace Application.validation
{

    public class SettingDtoValidator : AbstractValidator<SettingDto>
    {
        public SettingDtoValidator()
        {
            RuleFor(x => x.Id)
             .GreaterThan(0)
            .When(x => x.IsUpdate)
                .WithMessage("Id must be greater than zero when updating.");

            RuleFor(x => x.LocationText)
                .NotEmpty().WithMessage("Location text is required.")
                .MaximumLength(200).WithMessage("Location text must not exceed 200 characters.");

            RuleFor(x => x.PinLocation)
                .NotEmpty().WithMessage("Pin location is required.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Phone number must be valid.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.");

            RuleFor(x => x.FacebookLink)
                .Must(BeAValidUrl).When(x => !string.IsNullOrWhiteSpace(x.FacebookLink))
                .WithMessage("Facebook link must be a valid URL.");

            RuleFor(x => x.TwitterLink)
                .Must(BeAValidUrl).When(x => !string.IsNullOrWhiteSpace(x.TwitterLink))
                .WithMessage("Twitter link must be a valid URL.");

            RuleFor(x => x.LinkedInLink)
                .Must(BeAValidUrl).When(x => !string.IsNullOrWhiteSpace(x.LinkedInLink))
                .WithMessage("LinkedIn link must be a valid URL.");

            RuleFor(x => x.InstagramLink)
                .Must(BeAValidUrl).When(x => !string.IsNullOrWhiteSpace(x.InstagramLink))
                .WithMessage("Instagram link must be a valid URL.");

            RuleFor(x => x.YoutubeLink)
                .Must(BeAValidUrl).When(x => !string.IsNullOrWhiteSpace(x.YoutubeLink))
                .WithMessage("YouTube link must be a valid URL.");
        }

        private bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _) &&
                   (url.StartsWith("http://") || url.StartsWith("https://"));
        }
    }
}