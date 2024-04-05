using Microsoft.EntityFrameworkCore;
using Movies.Domain.Genres;

namespace Movies.Infrastructure.Persistence.Repositories;

internal sealed class GenreRepository : IGenreRepository
{
    private readonly DbSet<Genre> _genres;

    public GenreRepository(ApplicationDbContext context)
    {
        _genres = context.Genres;

    }

    public async Task<Genre?> GetByIdAsync(
        GenreId id, CancellationToken cancellationToken = default)
    {
        return await _genres
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Genre>> GetListAsync(
        CancellationToken cancellationToken = default)
    {
        return await _genres
            .ToListAsync(cancellationToken);
    }

    public void Add(Genre entity)
    {
        _genres.Add(entity);
    }

    public void Remove(Genre entity)
    {
        _genres.Remove(entity);
    }
}
