using Application.Features.BookFeatures.Commands.UpdateBook;
using FluentValidation;

namespace Application.Validations.BookValidatos;

public class BookUpdateValidator : AbstractValidator<UpdateBookCommand>
{
    public BookUpdateValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title cannot be null or empty.")
            .MaximumLength(200)
            .WithMessage("Title cannot exceed 200 characters.");

        RuleFor(x => x.AuthorId)
            .GreaterThan(0)
            .WithMessage("AuthorId must be greater than 0.");

        RuleFor(x => x.PublicationYear)
            .GreaterThan(1000)
            .WithMessage("PublicationYear must be a valid year.")
            .LessThanOrEqualTo(DateTime.Now.Year)
            .WithMessage("PublicationYear cannot be in the future.");
    }
}
