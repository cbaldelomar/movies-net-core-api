namespace Movies.Application.Movies.GetList;

public interface IListMovieQueryService
{
    Task<IEnumerable<MovieResult>> ListAsync(string? genre, CancellationToken cancellationToken = default);
}
