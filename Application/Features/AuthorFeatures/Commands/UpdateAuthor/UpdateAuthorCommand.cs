using Application.Dtos.AuthorDtos;
using MediatR;

namespace Application.Features.AuthorFeatures.Commands.UpdateAuthor;

public class UpdateAuthorCommand : IRequest<AuthorReadDto?>
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}
