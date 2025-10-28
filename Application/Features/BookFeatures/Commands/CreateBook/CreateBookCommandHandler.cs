using Application.Dtos.BookDtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.BookFeatures.Commands.CreateBook;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, BookReadDto>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateBookCommandHandler> _logger;

    public CreateBookCommandHandler(
        IBookRepository bookRepository,
        IMapper mapper,
        ILogger<CreateBookCommandHandler> logger)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BookReadDto> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new book: {Title}", request.Title);

        var authorExists = await _bookRepository.AuthorExistsAsync(request.AuthorId, cancellationToken);
        if (!authorExists)
        {
            _logger.LogWarning("Author with Id {AuthorId} not found.", request.AuthorId);
            throw new KeyNotFoundException($"Author with Id {request.AuthorId} does not exist.");
        }

        var book = new Book
        {
            Title = request.Title,
            AuthorId = request.AuthorId,
            PublicationYear = request.PublicationYear
        };

        var bookId = await _bookRepository.CreateAsync(book, cancellationToken);

        _logger.LogInformation("Book created successfully with Id: {BookId}", bookId);

        var createdBook = await _bookRepository.GetByIdWithAuthorAsync(bookId, cancellationToken);

        return _mapper.Map<BookReadDto>(createdBook);
    }
}
