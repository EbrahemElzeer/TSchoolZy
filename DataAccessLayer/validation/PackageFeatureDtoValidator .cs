using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using FluentValidation;

namespace Application.validation
{
    public class PackageFeatureDtoValidator : AbstractValidator<PackageFeatureDto>
    {
        //public PackageFeatureDtoValidator()
        //{
        //    RuleFor(x => x.FeatureText)
        //        .NotEmpty().WithMessage("Feature name is required.")
        //        .MaximumLength(35).WithMessage("Feature name must not exceed 100 characters.");
        //}
    }
}