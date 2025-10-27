using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.BaseRepository;

namespace Infrastructure.Repositories;

public class BookRepository : GenericRepository<Book, int>
{
    public BookRepository(UpgamingDbContext context) : base(context)
    {
    }
}