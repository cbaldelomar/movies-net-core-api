using Microsoft.EntityFrameworkCore;
using Movies.Application.Movies.Services;
using Movies.Domain.Movies;

namespace Movies.Infrastructure.Persistence.Queries;

internal sealed class CheckMovieExistsQueryService(ApplicationDbContext context) : ICheckMovieExistsQueryService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> CheckAsync(
        Title title,
        Year year,
        Director director,
        MovieId id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Movies
            .Where(m => m.Title == title
                && m.Year == year
                && m.Director == director
                && m.Id != id)
            .AnyAsync(cancellationToken);
    }
}
