using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application.UseCases.Product.Commands.Update;

public class UpdateProductCommandValidator :AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(255)
            .WithMessage("Please enter valid Name");

        RuleFor(p => p.Description)
            .NotNull()
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(3000)
            .WithMessage("Please enter valid Description");

        RuleFor(p => p.Price)
            .NotNull()
            .NotEmpty()
            .WithMessage("Please enter valid Price");
    }
}
