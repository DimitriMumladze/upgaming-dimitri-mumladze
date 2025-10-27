using Application.Dtos.AuthorDtos;
using Application.Dtos.BookDtos;
using Application.Features.AuthorFeatures.Commands.CreateAuthor;
using Application.Features.AuthorFeatures.Commands.DeleteAuthor;
using Application.Features.AuthorFeatures.Commands.UpdateAuthor;
using Application.Features.AuthorFeatures.Queries.GetAllAuthors;
using Application.Features.AuthorFeatures.Queries.GetBookByAuthor;
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
    [ProducesResponseType(typeof(IEnumerable<AuthorReadDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AuthorReadDto>>> GetAll()
    {
        var authors = await _mediator.Send(new GetAllAuthorsQuery());
        return Ok(authors);
    }

    // GET: api/authors/{id}
    // Returns author details with nested list of books
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AuthorReadDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AuthorReadDto>> GetById(int id)
    {
        var author = await _mediator.Send(new GetAuthorByIdQuery(id));

        if (author == null)
        {
            return NotFound(new { Message = $"Author with ID {id} not found." });
        }

        return Ok(author);
    }

    // GET: api/authors/{id}/books
    // Returns list of books by specific author
    [HttpGet("{id}/books")]
    [ProducesResponseType(typeof(IEnumerable<BookReadDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<BookReadDto>>> GetBooksByAuthor(int id)
    {
        var books = await _mediator.Send(new GetBooksByAuthorQuery(id));

        if (books == null)
        {
            return NotFound(new { Message = $"Author with ID {id} not found." });
        }

        return Ok(books);
    }

    // POST: api/authors
    [HttpPost]
    [ProducesResponseType(typeof(AuthorReadDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthorReadDto>> Create([FromBody] AuthorCreateDto createDto)
    {
        if (string.IsNullOrWhiteSpace(createDto.Name))
        {
            return BadRequest(new { Message = "Author name is required." });
        }

        var command = new CreateAuthorCommand { Name = createDto.Name };
        var author = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);
    }

    // PUT: api/authors/{id}
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AuthorReadDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthorReadDto>> Update(int id, [FromBody] AuthorUpdateDto updateDto)
    {
        if (string.IsNullOrWhiteSpace(updateDto.Name))
        {
            return BadRequest(new { Message = "Author name is required." });
        }

        var command = new UpdateAuthorCommand
        {
            Id = id,
            Name = updateDto.Name
        };

        var author = await _mediator.Send(command);

        if (author == null)
        {
            return NotFound(new { Message = $"Author with ID {id} not found." });
        }

        return Ok(author);
    }

    // DELETE: api/authors/{id}
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteAuthorCommand(id));

        if (!result)
        {
            return NotFound(new { Message = $"Author with ID {id} not found." });
        }

        return NoContent();
    }
}

