using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AuthorRepository : GenericRepository<Author, int>, IAuthorRepository
{
    public AuthorRepository(UpgamingDbContext context) : base(context)
    {
    }
    public async Task<Author?> GetByIdWithBooksAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid id", nameof(id));

        return await _dbSet
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Author>> GetAllWithBooksAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(a => a.Books)
            .ToListAsync(cancellationToken);
    }
}   

