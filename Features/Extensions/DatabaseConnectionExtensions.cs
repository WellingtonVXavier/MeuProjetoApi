using Microsoft.EntityFrameworkCore;
using MeuProjetoApi.Data;

namespace MeuProjetoApi.Features.Extensions;

public static class DatabaseConnectionExtensions
{
    public static IServiceCollection AddDatabaseConnection(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "A connection string 'DefaultConnection' não foi configurada.");

        services.AddDbContext<AppDbContexts>(options =>
            options.UseSqlite(connectionString));

        return services;
    }
}
