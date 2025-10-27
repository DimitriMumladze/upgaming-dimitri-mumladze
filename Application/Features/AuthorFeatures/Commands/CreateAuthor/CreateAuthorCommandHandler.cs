using Application.Dtos.AuthorDtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.AuthorFeatures.Commands.CreateAuthor;

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, AuthorReadDto>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateAuthorCommandHandler> _logger;

    public CreateAuthorCommandHandler(
        IAuthorRepository authorRepository,
        IMapper mapper,
        ILogger<CreateAuthorCommandHandler> logger)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<AuthorReadDto> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new author with name: {Name}", request.Name);

        var author = new Author
        {
            Name = request.Name,
            Books = new List<Book>()
        };

        var authorId = await _authorRepository.CreateAsync(author, cancellationToken);

        _logger.LogInformation("Author created successfully with ID: {AuthorId}", authorId);

        var createdAuthor = await _authorRepository.GetByIdWithBooksAsync(authorId, cancellationToken);

        return _mapper.Map<AuthorReadDto>(createdAuthor);
    }
}
