using Microsoft.EntityFrameworkCore;
using Movies.Application.Movies;
using Movies.Application.Movies.Services;
using Movies.Domain.Movies;

namespace Movies.Infrastructure.Persistence.Queries;

internal sealed class GetMovieListQueryService(ApplicationDbContext context) : IGetMovieListQueryService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<MovieResult>> ListAsync(
        string? genre,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Movie> query = _context.Movies.AsQueryable();

        if (!string.IsNullOrWhiteSpace(genre))
        {
            query = query.Where(e => e.Genres.Any(e => e.Name == genre));
        }

        return await query
            .Select(e => new MovieResult(
                e.Id.Value,
                e.Title.Value,
                e.Year.Value,
                e.Director.Value,
                e.Duration.Value,
                e.Poster == null ? null : e.Poster.Value,
                e.Rate == null ? null : e.Rate.Value,
                e.Genres.Select(g => g.Name)))
            .ToListAsync(cancellationToken);
    }
}
