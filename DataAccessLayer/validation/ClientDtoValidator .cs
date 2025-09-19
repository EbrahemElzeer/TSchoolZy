using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using FluentValidation;

namespace Application.validation
{
   
        public class ClientDtoValidator : AbstractValidator<ClientDto>
        {
            public ClientDtoValidator()
            {
            RuleFor(x => x.Id)
             .GreaterThan(0)
            .When(x => x.IsUpdate)
                .WithMessage("Id must be greater than zero when updating.");

            RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Name is required.")
                    .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

                RuleFor(x => x.ImagePath)
                    .NotEmpty().WithMessage("Image path is required.")
                    .MaximumLength(200).WithMessage("Image path must not exceed 200 characters.");
            }
        }
    
}
