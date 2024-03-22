using Microsoft.EntityFrameworkCore;
using Movies.Domain.Movies;

namespace Movies.Infrastructure.Persistence.Repositories;

internal sealed class MovieRepository : IMovieRepository
{
    private readonly DbSet<Movie> _movies;

    public MovieRepository(ApplicationDbContext context)
    {
        _movies = context.Movies;

    }

    public async Task<Movie?> GetByIdAsync(
        MovieId id, CancellationToken cancellationToken = default)
    {
        return await _movies
            .Include(m => m.Genres)
            .FirstOrDefaultAsync(e => e.UUID == id, cancellationToken);
    }

    public async Task<IEnumerable<Movie>> GetListAsync(
        CancellationToken cancellationToken = default)
    {
        return await _movies
            .Include(m => m.Genres)
            .ToListAsync(cancellationToken);
    }

    public void Add(Movie movie)
    {
        _movies.Add(movie);
    }

    public void Remove(Movie movie)
    {
        _movies.Remove(movie);
    }
}
