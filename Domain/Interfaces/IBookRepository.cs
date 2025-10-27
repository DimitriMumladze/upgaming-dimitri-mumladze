using Domain.Entities;
using Domain.Interfaces.BaseInterface;

namespace Domain.Interfaces;

public interface IBookRepository : IGenericRepository<Book, int>
{
    // Get all books with author information 
    Task<IEnumerable<Book>> GetAllWithAuthorAsync(CancellationToken cancellationToken = default);

    // Get book by id with author information
    Task<Book?> GetByIdWithAuthorAsync(int id, CancellationToken cancellationToken = default);

    // Get books by author id 
    Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(int authorId, CancellationToken cancellationToken = default);

    // Get books with filtering and sorting 
    Task<IEnumerable<Book>> GetBooksWithFilteringAsync(
        int? publicationYear = null,
        string? sortBy = null,
        CancellationToken cancellationToken = default);

    // Check if author exists 
    Task<bool> AuthorExistsAsync(int authorId, CancellationToken cancellationToken = default);
}
