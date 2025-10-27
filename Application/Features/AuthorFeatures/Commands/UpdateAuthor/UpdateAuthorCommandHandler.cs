using Application.Dtos.AuthorDtos;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.AuthorFeatures.Commands.UpdateAuthor;

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, AuthorReadDto?>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateAuthorCommandHandler> _logger;

    public UpdateAuthorCommandHandler(
        IAuthorRepository authorRepository,
        IMapper mapper,
        ILogger<UpdateAuthorCommandHandler> logger)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<AuthorReadDto?> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating author with ID: {AuthorId}", request.Id);

        var existingAuthor = await _authorRepository.GetByIdAsync(request.Id, cancellationToken);

        if (existingAuthor == null)
        {
            _logger.LogWarning("Author with ID {AuthorId} not found for update.", request.Id);
            return null;
        }

        existingAuthor.Name = request.Name;

        await _authorRepository.UpdateAsync(existingAuthor, cancellationToken);

        _logger.LogInformation("Author with ID {AuthorId} updated successfully.", request.Id);

        var updatedAuthor = await _authorRepository.GetByIdWithBooksAsync(request.Id, cancellationToken);

        return _mapper.Map<AuthorReadDto>(updatedAuthor);
    }
}
