using Application.Dtos.AuthorDtos;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.AuthorFeatures.Queries.GetAllAuthors;

public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, IEnumerable<AuthorReadDto>>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllAuthorsQueryHandler> _logger;

    public GetAllAuthorsQueryHandler(
        IAuthorRepository authorRepository,
        IMapper mapper,
        ILogger<GetAllAuthorsQueryHandler> logger)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<AuthorReadDto>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching all authors with their books from repository");

        var authors = await _authorRepository.GetAllWithBooksAsync(cancellationToken);

        if (!authors.Any())
            _logger.LogWarning("No authors found in the database");

        var result = _mapper.Map<IEnumerable<AuthorReadDto>>(authors);
        
        _logger.LogInformation("Fetched {Count} authors with their books", result.Count());

        return result;
    }
}
