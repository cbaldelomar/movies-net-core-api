namespace Movies.Application.Movies.GetList;

public interface IGetMovieListQueryService
{
    Task<IEnumerable<MovieResult>> ListAsync(string? genre, CancellationToken cancellationToken = default);
}
