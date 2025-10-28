using Application.Dtos.BookDtos;
using MediatR;

namespace Application.Features.BookFeatures.Commands.CreateBook;

public class CreateBookCommand : IRequest<BookReadDto>
{
    public string Title { get; set; } = default!;
    public int AuthorId { get; set; }
    public int PublicationYear { get; set; }
}
