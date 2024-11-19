using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movies.Application.Movies.Services;
using Movies.Domain.Abstractions;
using Movies.Domain.Genres;
using Movies.Domain.Movies;
using Movies.Infrastructure.Persistence;
using Movies.Infrastructure.Persistence.Queries;
using Movies.Infrastructure.Persistence.Repositories;

namespace Movies.Infrastructure;

public static class ConfigureServices
{
    private const string CONNECTION_NAME = "Movies";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySQL(configuration.GetConnectionString(CONNECTION_NAME)!));

        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IGetMovieByIdQueryService, GetMovieByIdQueryService>();
        services.AddScoped<IGetMovieListQueryService, GetMovieListQueryService>();
        services.AddScoped<ICheckMovieExistsQueryService, CheckMovieExistsQueryService>();

        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        //services.AddScoped<MySqlFunctions>();

        return services;
    }
}
