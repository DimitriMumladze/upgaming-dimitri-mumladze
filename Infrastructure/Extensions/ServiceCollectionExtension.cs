using Domain.Interfaces.BaseInterface;
using Infrastructure.Persistence;
using Infrastructure.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UpgamingDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Service Registration
        services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

        return services;
    }
}
