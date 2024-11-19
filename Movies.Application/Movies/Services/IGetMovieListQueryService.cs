namespace Movies.Application.Movies.Services;

public interface IGetMovieListQueryService
{
    Task<IEnumerable<MovieResult>> ListAsync(string? genre, CancellationToken cancellationToken = default);
}
