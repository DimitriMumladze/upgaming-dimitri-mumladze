using Application.Dtos.AuthorDtos;
using FluentValidation;

namespace Application.Validations.AuthorValidations;

public class AuthorCreateValidator : AbstractValidator<AuthorCreateDto>
{
    public AuthorCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(3, 100).WithMessage("Name should be between 3 and 100 characters.");
    }
}
