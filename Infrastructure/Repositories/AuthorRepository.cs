using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.BaseRepository;

namespace Infrastructure.Repositories;

public class AuthorRepository : GenericRepository<Author, int>
{
    public AuthorRepository(UpgamingDbContext context) : base(context)
    {
    }
}   

