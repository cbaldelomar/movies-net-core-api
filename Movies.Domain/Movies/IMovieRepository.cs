namespace Movies.Domain.Movies;

public interface IMovieRepository
{
    Task<Movie?> GetByIdAsync(MovieId id, CancellationToken cancellationToken = default);

    Task<IEnumerable<Movie>> GetListAsync(CancellationToken cancellationToken = default);

    void Add(Movie movie);

    void Remove(Movie movie);
}
