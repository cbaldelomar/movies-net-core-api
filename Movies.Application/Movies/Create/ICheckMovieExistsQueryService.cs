using Movies.Domain.Movies;

namespace Movies.Application.Movies.Create;

public interface ICheckMovieExistsQueryService
{
    Task<bool> CheckAsync(
        Title title,
        Year year,
        Director director,
        MovieId id,
        CancellationToken cancellationToken = default);
}
