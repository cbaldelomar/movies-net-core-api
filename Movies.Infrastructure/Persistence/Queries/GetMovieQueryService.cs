using Microsoft.EntityFrameworkCore;
using Movies.Application.Movies.GetById;
using Movies.Domain.Movies;
using Movies.Application.Movies;

namespace Movies.Infrastructure.Persistence.Queries;

internal sealed class GetMovieQueryService(ApplicationDbContext context) : IGetMovieQueryService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<MovieResult?> GetAsync(
        MovieId id, CancellationToken cancellationToken = default)
    {
        return await _context.Movies
            .Where(e => e.UUID == id)
            .Select(e => new MovieResult(
                e.UUID.Value,
                e.Title.Value,
                e.Year.Value,
                e.Director.Value,
                e.Duration.Value,
                e.Poster == null ? null : e.Poster.Value,
                e.Rate == null ? null : e.Rate.Value,
                e.Genres.Select(g => g.Name)))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
