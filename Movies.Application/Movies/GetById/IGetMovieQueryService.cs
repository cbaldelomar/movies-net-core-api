using Movies.Domain.Movies;

namespace Movies.Application.Movies.GetById;

public interface IGetMovieQueryService
{
    Task<MovieResult?> GetAsync(MovieId id, CancellationToken cancellationToken = default);
}
