using Application.Dtos.AuthorDtos;
using Application.Features.AuthorFeatures.Commands.UpdateAuthor;
using FluentValidation;

namespace Application.Validations.AuthorValidations;

public class AuthorUpdateValidator : AbstractValidator<UpdateAuthorCommand>
{
    public AuthorUpdateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(3, 100).WithMessage("Name should be between 3 and 100 characters.");
    }
}
