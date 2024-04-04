using Movies.Application.Abstractions;
using Movies.Domain.Movies;

namespace Movies.Application.Movies.GetById;

public class GetMovieByIdQueryHandler(IGetMovieByIdQueryService queryService)
    : IQueryHandler<GetMovieByIdQuery, MovieResult?>
{
    private readonly IGetMovieByIdQueryService _queryService = queryService;

    public async Task<MovieResult?> Handle(
        GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        MovieId movieId = new(request.Id);

        return await _queryService.GetAsync(movieId, cancellationToken);
    }
}
