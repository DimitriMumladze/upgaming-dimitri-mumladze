using Application.Dtos.BookDtos;
using Application.Features.BookFeatures.Commands.CreateBook;
using Application.Features.BookFeatures.Commands.DeleteBook;
using Application.Features.BookFeatures.Commands.UpdateBook;
using Application.Features.BookFeatures.Queries.GetAllBook;
using Application.Features.BookFeatures.Queries.GetBookById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace upgaming_dimitri_mumladze.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<BooksController> _logger;

    public BooksController(IMediator mediator, ILogger<BooksController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get all books with optional filtering and sorting
    /// </summary>
    /// <param name="publicationYear">Filter by publication year (optional)</param>
    /// <param name="sortBy">Sort by field: title, publicationyear, author (optional)</param>
    /// <returns>List of books with author names</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookReadDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BookReadDto>>> GetAll(
        [FromQuery] int? publicationYear = null,
        [FromQuery] string? sortBy = null)
    {
        var query = new GetAllBooksQuery
        {
            PublicationYear = publicationYear,
            SortBy = sortBy
        };

        var books = await _mediator.Send(query);
        return Ok(books);
    }

    /// <summary>
    /// Get a specific book by ID
    /// </summary>
    /// <param name="id">Book ID</param>
    /// <returns>Book details with author name</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BookReadDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookReadDto>> GetById(int id)
    {
        var book = await _mediator.Send(new GetBookByIdQuery(id));

        if (book == null)
        {
            return NotFound(new { Message = $"Book with ID {id} not found." });
        }

        return Ok(book);
    }

    /// <summary>
    /// Create a new book
    /// </summary>
    /// <param name="createDto">Book creation data</param>
    /// <returns>Created book with author name</returns>
    [HttpPost]
    [ProducesResponseType(typeof(BookReadDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BookReadDto>> Create([FromBody] BookCreateDto createDto)
    {
        // Validation: Title cannot be null or empty
        if (string.IsNullOrWhiteSpace(createDto.Title))
        {
            return BadRequest(new { Message = "Title cannot be null or empty." });
        }

        // Validation: PublicationYear cannot be in the future
        if (createDto.PublicationYear > DateTime.Now.Year)
        {
            return BadRequest(new { Message = "PublicationYear cannot be in the future." });
        }

        try
        {
            var command = new CreateBookCommand
            {
                Title = createDto.Title,
                AuthorId = createDto.AuthorId,
                PublicationYear = createDto.PublicationYear
            };

            var book = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }
        catch (KeyNotFoundException ex)
        {
            // Validation: AuthorID must correspond to an existing author
            return BadRequest(new { Message = ex.Message });
        }
    }

    /// <summary>
    /// Update an existing book (Bonus Option B)
    /// </summary>
    /// <param name="id">Book ID</param>
    /// <param name="updateDto">Updated book data</param>
    /// <returns>Updated book with author name</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(BookReadDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BookReadDto>> Update(int id, [FromBody] BookUpdateDto updateDto)
    {
        // Validation: Title cannot be null or empty
        if (string.IsNullOrWhiteSpace(updateDto.Title))
        {
            return BadRequest(new { Message = "Title cannot be null or empty." });
        }

        // Validation: PublicationYear cannot be in the future
        if (updateDto.PublicationYear > DateTime.Now.Year)
        {
            return BadRequest(new { Message = "PublicationYear cannot be in the future." });
        }

        try
        {
            var command = new UpdateBookCommand
            {
                Id = id,
                Title = updateDto.Title,
                AuthorId = updateDto.AuthorId,
                PublicationYear = updateDto.PublicationYear
            };

            var book = await _mediator.Send(command);

            if (book == null)
            {
                return NotFound(new { Message = $"Book with ID {id} not found." });
            }

            return Ok(book);
        }
        catch (KeyNotFoundException ex)
        {
            // Validation: AuthorID must correspond to an existing author
            return BadRequest(new { Message = ex.Message });
        }
    }

    /// <summary>
    /// Delete a book
    /// </summary>
    /// <param name="id">Book ID</param>
    /// <returns>No content on success</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteBookCommand(id));

        if (!result)
        {
            return NotFound(new { Message = $"Book with ID {id} not found." });
        }

        return NoContent();
    }
}
