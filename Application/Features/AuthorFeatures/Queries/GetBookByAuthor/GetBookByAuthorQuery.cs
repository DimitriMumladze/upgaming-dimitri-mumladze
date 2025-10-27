using Application.Dtos.BookDtos;
using MediatR;

namespace Application.Features.AuthorFeatures.Queries.GetBookByAuthor;

public class GetBooksByAuthorQuery : IRequest<IEnumerable<BookReadDto>?>
{
    public int AuthorId { get; set; }

    public GetBooksByAuthorQuery(int authorId)
    {
        AuthorId = authorId;
    }
}
