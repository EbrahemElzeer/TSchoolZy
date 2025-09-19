using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using FluentValidation;

namespace Application.validation
{
    public class ContactFormDtoValidator : AbstractValidator<ContactFormDto>
    {
        public ContactFormDtoValidator()
        {
            RuleFor(x => x.Id)
             .GreaterThan(0)
            .When(x => x.IsUpdate)
                .WithMessage("Id must be greater than zero when updating.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.");

            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Subject is required.")
                .MaximumLength(200).WithMessage("Subject must not exceed 200 characters.");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Message is required.");

        }
    }
 }
