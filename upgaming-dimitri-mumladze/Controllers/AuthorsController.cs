using Application.Dtos.AuthorDtos;
using Application.Features.AuthorFeatures.Queries.GetAllAuthors;
using Application.Features.AuthorFeatures.Queries.GetByIdAuthor;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace upgaming_dimitri_mumladze.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/authors
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorReadDto>>> GetAll()
    {
        var authors = await _mediator.Send(new GetAllAuthorsQuery());
        return Ok(authors);
    }

    // GET: api/authors/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorReadDto>> GetById(int id)
    {
        var author = await _mediator.Send(new GetAuthorByIdQuery(id));

        if (author == null)
        {
            return NotFound(new { Message = $"Author with ID {id} not found." });
        }

        return Ok(author);
    }
}

