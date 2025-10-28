using Application.Dtos.BookDtos;
using MediatR;

namespace Application.Features.BookFeatures.Queries.GetBookById;

public class GetBookByIdQuery : IRequest<BookReadDto?>
{
    public int Id { get; set; }

    public GetBookByIdQuery(int id)
    {
        Id = id;
    }
}
