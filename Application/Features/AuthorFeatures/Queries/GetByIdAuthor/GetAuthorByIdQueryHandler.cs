using Application.Dtos.AuthorDtos;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.AuthorFeatures.Queries.GetByIdAuthor;

public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, AuthorReadDto?>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAuthorByIdQueryHandler> _logger;

    public GetAuthorByIdQueryHandler(
        IAuthorRepository authorRepository,
        IMapper mapper,
        ILogger<GetAuthorByIdQueryHandler> logger)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<AuthorReadDto?> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching author with ID {AuthorId} with books", request.Id);

        var author = await _authorRepository.GetByIdWithBooksAsync(request.Id, cancellationToken);

        if (author == null)
        {
            _logger.LogWarning("Author with ID {AuthorId} not found.", request.Id);
            return null;
        }

        var result = _mapper.Map<AuthorReadDto>(author);

        _logger.LogInformation("Found author '{AuthorName}' with {BookCount} books",
            author.Name, author.Books?.Count ?? 0);

        return result;
    }
}
