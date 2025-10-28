using Application.Dtos.BookDtos;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.BookFeatures.Commands.UpdateBook;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, BookReadDto?>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateBookCommandHandler> _logger;

    public UpdateBookCommandHandler(
        IBookRepository bookRepository,
        IMapper mapper,
        ILogger<UpdateBookCommandHandler> logger)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BookReadDto?> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating book with Id: {BookId}", request.Id);

        var existingBook = await _bookRepository.GetByIdAsync(request.Id, cancellationToken);
        if (existingBook == null)
        {
            _logger.LogWarning("Book with Id {BookId} not found for update.", request.Id);
            return null;
        }

        var authorExists = await _bookRepository.AuthorExistsAsync(request.AuthorId, cancellationToken);
        if (!authorExists)
        {
            _logger.LogWarning("Author with Id {AuthorId} not found.", request.AuthorId);
            throw new KeyNotFoundException($"Author with ID {request.AuthorId} does not exist.");
        }

        existingBook.Title = request.Title;
        existingBook.AuthorId = request.AuthorId;
        existingBook.PublicationYear = request.PublicationYear;

        await _bookRepository.UpdateAsync(existingBook, cancellationToken);

        _logger.LogInformation("Book with Id {BookId} updated successfully.", request.Id);

        var updatedBook = await _bookRepository.GetByIdWithAuthorAsync(request.Id, cancellationToken);

        return _mapper.Map<BookReadDto>(updatedBook);
    }
}
