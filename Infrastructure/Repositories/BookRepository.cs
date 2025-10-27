using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BookRepository : GenericRepository<Book, int>, IBookRepository
{
    public BookRepository(UpgamingDbContext context) : base(context)
    {
    }
    public async Task<IEnumerable<Book>> GetAllWithAuthorAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(b => b.Author)
            .ToListAsync(cancellationToken);
    }

    public async Task<Book?> GetByIdWithAuthorAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid id", nameof(id));

        return await _dbSet
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(int authorId, CancellationToken cancellationToken = default)
    {
        if (authorId <= 0)
            throw new ArgumentException("Invalid author id", nameof(authorId));

        return await _dbSet
            .Include(b => b.Author)
            .Where(b => b.AuthorId == authorId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Book>> GetBooksWithFilteringAsync(
        int? publicationYear = null,
        string? sortBy = null,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet.Include(b => b.Author).AsQueryable();

        if (publicationYear.HasValue)
        {
            query = query.Where(b => b.PublicationYear == publicationYear.Value);
        }

        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            query = sortBy.ToLower() switch
            {
                "title" => query.OrderBy(b => b.Title),
                "publicationyear" => query.OrderBy(b => b.PublicationYear),
                "author" => query.OrderBy(b => b.Author.Name),
                _ => query.OrderBy(b => b.Id) 
            };
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<bool> AuthorExistsAsync(int authorId, CancellationToken cancellationToken = default)
    {
        var context = _context as UpgamingDbContext;
        if (context == null)
            throw new InvalidOperationException("Invalid DbContext type");

        return await context.Authors
            .AnyAsync(a => a.Id == authorId, cancellationToken);
    }
}