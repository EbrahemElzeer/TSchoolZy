using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using FluentValidation;

namespace Application.validation
{
    public class TeamMemberDtoValidator : AbstractValidator<TeamMemberDto>
    {
        public TeamMemberDtoValidator()
        {
            RuleFor(x => x.Id)
             .GreaterThan(0)
            .When(x => x.IsUpdate)
                .WithMessage("Id must be greater than zero when updating.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Position)
                .NotEmpty().WithMessage("Position is required.")
                .MaximumLength(100).WithMessage("Position must not exceed 100 characters.");

            RuleFor(x => x.ImagePath)
                .NotEmpty().WithMessage("Image path is required.")
                .MaximumLength(200).WithMessage("Image path must not exceed 200 characters.");

            RuleFor(x => x.LinkedInLink)
                .Must(BeAValidUrl).When(x => !string.IsNullOrWhiteSpace(x.LinkedInLink))
                .WithMessage("LinkedIn link must be a valid URL starting with http or https.");
        }

        private bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _) &&
                   (url.StartsWith("http://") || url.StartsWith("https://"));
        }
    }
}
