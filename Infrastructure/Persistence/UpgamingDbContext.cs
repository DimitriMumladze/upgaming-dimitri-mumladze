using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class UpgamingDbContext : DbContext
{
    public UpgamingDbContext(DbContextOptions<UpgamingDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }
}
