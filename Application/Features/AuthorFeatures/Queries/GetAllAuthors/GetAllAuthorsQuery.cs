using Application.Dtos.AuthorDtos;
using MediatR;

namespace Application.Features.AuthorFeatures.Queries.GetAllAuthors;

public record GetAllAuthorsQuery : IRequest<IEnumerable<AuthorReadDto>>;
