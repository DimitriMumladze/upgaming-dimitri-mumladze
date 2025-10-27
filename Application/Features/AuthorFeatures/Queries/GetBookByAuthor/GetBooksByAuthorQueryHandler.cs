using Application.Dtos.BookDtos;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.AuthorFeatures.Queries.GetBookByAuthor;

public class GetBooksByAuthorQueryHandler : IRequestHandler<GetBooksByAuthorQuery, IEnumerable<BookReadDto>?>
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetBooksByAuthorQueryHandler> _logger;

    public GetBooksByAuthorQueryHandler(
        IBookRepository bookRepository,
        IAuthorRepository authorRepository,
        IMapper mapper,
        ILogger<GetBooksByAuthorQueryHandler> logger)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<BookReadDto>?> Handle(GetBooksByAuthorQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching books for author with ID: {AuthorId}", request.AuthorId);

        var authorExists = await _authorRepository.GetByIdAsync(request.AuthorId, cancellationToken);

        if (authorExists == null)
        {
            _logger.LogWarning("Author with ID {AuthorId} not found.", request.AuthorId);
            return null;
        }

        var books = await _bookRepository.GetBooksByAuthorIdAsync(request.AuthorId, cancellationToken);

        var result = _mapper.Map<IEnumerable<BookReadDto>>(books);

        _logger.LogInformation("Found {BookCount} books for author with ID {AuthorId}.",
            result.Count(), request.AuthorId);

        return result;
    }
}
