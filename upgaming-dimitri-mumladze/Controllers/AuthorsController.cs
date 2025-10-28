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

    /// <summary>
    /// Retrieves all authors with their books.
    /// </summary>
    /// <returns>A list of all authors with nested book collections</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AuthorReadDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AuthorReadDto>>> GetAll()
    {
        var authors = await _mediator.Send(new GetAllAuthorsQuery());
        return Ok(authors);
    }

    /// <summary>
    /// Retrieves a specific author by ID with their books.
    /// </summary>
    /// <param name="id">The unique identifier of the author</param>
    /// <returns>Author details with nested list of books</returns>
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

    /// <summary>
    /// Retrieves all books written by a specific author.
    /// </summary>
    /// <param name="id">The unique identifier of the author</param>
    /// <returns>A list of books by the specified author</returns>
    [HttpGet("{id}/books")]
    [ProducesResponseType(typeof(IEnumerable<BookReadDtoForAuthor>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<BookReadDtoForAuthor>>> GetBooksByAuthor(int id)
    {
        var books = await _mediator.Send(new GetBooksByAuthorQuery(id));

        if (books == null)
        {
            return NotFound(new { Message = $"Author with ID {id} not found." });
        }

        return Ok(books);
    }

    /// <summary>
    /// Creates a new author.
    /// </summary>
    /// <param name="createDto">The author creation data</param>
    /// <returns>The newly created author</returns>
    [HttpPost]
    [ProducesResponseType(typeof(AuthorReadDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthorReadDto>> Create([FromBody] AuthorCreateDto createDto)
    {
        var command = new CreateAuthorCommand { Name = createDto.Name };
        var author = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);
    }

    /// <summary>
    /// Updates an existing author's details.
    /// </summary>
    /// <param name="id">The unique identifier of the author to update</param>
    /// <param name="updateDto">The updated author data</param>
    /// <returns>The updated author details</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AuthorReadDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthorReadDto>> Update(int id, [FromBody] AuthorUpdateDto updateDto)
    {
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

    /// <summary>
    /// Deletes an author and all associated books.
    /// </summary>
    /// <param name="id">The unique identifier of the author to delete</param>
    /// <returns>No content on successful deletion</returns>
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