using Domain.Entities;
using Domain.Interfaces.BaseInterface;

namespace Domain.Interfaces;

public interface IAuthorRepository : IGenericRepository<Author, int>
{
    // Get author with their books
    Task<Author?> GetByIdWithBooksAsync(int id, CancellationToken cancellationToken = default);

    // Get all authors with their books
    Task<IEnumerable<Author>> GetAllWithBooksAsync(CancellationToken cancellationToken = default);
}
