using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.BookFeatures.Commands.DeleteBook;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
{
    private readonly IBookRepository _bookRepository;
    private readonly ILogger<DeleteBookCommandHandler> _logger;

    public DeleteBookCommandHandler(
        IBookRepository bookRepository,
        ILogger<DeleteBookCommandHandler> logger)
    {
        _bookRepository = bookRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to delete book with ID: {BookId}", request.Id);

        var existingBook = await _bookRepository.GetByIdAsync(request.Id, cancellationToken);
        if (existingBook == null)
        {
            _logger.LogWarning("Book with ID {BookId} not found for deletion.", request.Id);
            return false;
        }

        await _bookRepository.DeleteAsync(request.Id, cancellationToken);

        _logger.LogInformation("Book with ID {BookId} deleted successfully.", request.Id);

        return true;
    }
}
