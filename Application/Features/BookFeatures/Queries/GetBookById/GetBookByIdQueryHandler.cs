using Application.Dtos.BookDtos;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.BookFeatures.Queries.GetBookById;

public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookReadDto?>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetBookByIdQueryHandler> _logger;

    public GetBookByIdQueryHandler(
        IBookRepository bookRepository,
        IMapper mapper,
        ILogger<GetBookByIdQueryHandler> logger)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BookReadDto?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching book with ID {BookId}...", request.Id);

        var book = await _bookRepository.GetByIdWithAuthorAsync(request.Id, cancellationToken);

        if (book == null)
        {
            _logger.LogWarning("Book with ID {BookId} not found.", request.Id);
            return null;
        }

        var result = _mapper.Map<BookReadDto>(book);

        _logger.LogInformation("Found book '{BookTitle}' by author '{AuthorName}'.",
            book.Title, book.Author.Name);

        return result;
    }
}
