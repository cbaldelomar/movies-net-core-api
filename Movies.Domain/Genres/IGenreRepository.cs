namespace Movies.Domain.Genres;

public interface IGenreRepository
{
    Task<Genre?> GetByIdAsync(GenreId id, CancellationToken cancellationToken = default);

    Task<IEnumerable<Genre>> GetListAsync(CancellationToken cancellationToken = default);

    void Add(Genre entity);

    void Remove(Genre entity);
}
