using Application.Dtos.AuthorDtos;
using MediatR;

namespace Application.Features.AuthorFeatures.Queries.GetByIdAuthor;

public class GetAuthorByIdQuery : IRequest<AuthorReadDto?>
{
    public int Id { get; set; }

    public GetAuthorByIdQuery(int id)
    {
        Id = id;
    }
}
