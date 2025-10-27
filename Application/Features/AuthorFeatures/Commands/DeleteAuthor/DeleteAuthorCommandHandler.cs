using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.AuthorFeatures.Commands.DeleteAuthor;

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, bool>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly ILogger<DeleteAuthorCommandHandler> _logger;

    public DeleteAuthorCommandHandler(
        IAuthorRepository authorRepository,
        ILogger<DeleteAuthorCommandHandler> logger)
    {
        _authorRepository = authorRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to delete author with ID: {AuthorId}", request.Id);

        var existingAuthor = await _authorRepository.GetByIdAsync(request.Id, cancellationToken);

        if (existingAuthor == null)
        {
            _logger.LogWarning("Author with ID {AuthorId} not found for deletion", request.Id);
            return false;
        }

        await _authorRepository.DeleteAsync(request.Id, cancellationToken);

        _logger.LogInformation("Author with ID {AuthorId} deleted successfully", request.Id);

        return true;
    }
}
