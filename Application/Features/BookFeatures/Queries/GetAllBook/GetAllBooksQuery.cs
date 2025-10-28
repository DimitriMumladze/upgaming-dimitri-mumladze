using Application.Dtos.BookDtos;
using MediatR;

namespace Application.Features.BookFeatures.Queries.GetAllBook;

public class GetAllBooksQuery : IRequest<IEnumerable<BookReadDto>>
{
    public int? PublicationYear { get; set; }
    public string? SortBy { get; set; }
}

