using Application.Dtos.BookDtos;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.BookFeatures.Queries.GetAllBook;

public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<BookReadDto>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllBooksQueryHandler> _logger;

    public GetAllBooksQueryHandler(
        IBookRepository bookRepository,
        IMapper mapper,
        ILogger<GetAllBooksQueryHandler> logger)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<BookReadDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching all books from repository...");

        IEnumerable<Domain.Entities.Book> books;

        if (request.PublicationYear.HasValue || !string.IsNullOrWhiteSpace(request.SortBy))
        {
            _logger.LogInformation("Applying filters - PublicationYear: {Year}, SortBy: {Sort}",
                request.PublicationYear, request.SortBy);

            books = await _bookRepository.GetBooksWithFilteringAsync(
                request.PublicationYear,
                request.SortBy,
                cancellationToken);
        }
        else
        {
            books = await _bookRepository.GetAllWithAuthorAsync(cancellationToken);
        }

        if (!books.Any())
        {
            _logger.LogWarning("No books found in the database.");
        }

        var result = _mapper.Map<IEnumerable<BookReadDto>>(books);

        _logger.LogInformation("Fetched {Count} books.", result.Count());

        return result;
    }
}
