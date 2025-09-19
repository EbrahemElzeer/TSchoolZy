using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using FluentValidation;

namespace Application.validation
{
    public class PackageDtoValidator : AbstractValidator<PackageDto>
    {
        public PackageDtoValidator()
        {
            RuleFor(x => x.Id)
            .GreaterThan(0)
           .When(x => x.IsUpdate)
               .WithMessage("Id must be greater than zero when updating.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.IconPath)
                .NotEmpty().WithMessage("Icon path is required.")
                .MaximumLength(200).WithMessage("Icon path must not exceed 200 characters.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be zero or greater.");

            RuleFor(x => x.Features)
                .NotNull().WithMessage("Features list cannot be null.")
                .Must(features => features.Count > 0).WithMessage("Features list must contain at least one feature.");

            RuleForEach(x => x.Features).SetValidator(new PackageFeatureDtoValidator());
        }
    }
}