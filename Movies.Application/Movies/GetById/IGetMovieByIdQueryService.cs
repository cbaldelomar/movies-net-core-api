using Movies.Domain.Movies;

namespace Movies.Application.Movies.GetById;

public interface IGetMovieByIdQueryService
{
    Task<MovieResult?> GetAsync(MovieId id, CancellationToken cancellationToken = default);
}
