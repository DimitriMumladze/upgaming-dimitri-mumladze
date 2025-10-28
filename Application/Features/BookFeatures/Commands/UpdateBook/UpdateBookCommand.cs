using Application.Dtos.BookDtos;
using MediatR;

namespace Application.Features.BookFeatures.Commands.UpdateBook;

public class UpdateBookCommand : IRequest<BookReadDto?>
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public int AuthorId { get; set; }
    public int PublicationYear { get; set; }
}
