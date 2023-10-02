using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

// to make the program.cs a little tidier
public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });
        // to add Cors so that the app can set the response headers
        services.AddCors();
        // AddScoped so that the service lives as long as the class, where its injected (in its scope)
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
