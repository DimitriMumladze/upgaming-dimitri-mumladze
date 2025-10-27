using Application.Dtos.AuthorDtos;
using MediatR;

namespace Application.Features.AuthorFeatures.Commands.CreateAuthor;

public class CreateAuthorCommand : IRequest<AuthorReadDto>
{
    public string Name { get; set; } = default!;
}
