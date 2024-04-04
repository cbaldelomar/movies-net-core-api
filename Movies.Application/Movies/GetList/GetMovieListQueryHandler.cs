using Movies.Application.Abstractions;

namespace Movies.Application.Movies.GetList;

internal sealed class GetMovieListQueryHandler
    : IQueryHandler<GetMovieListQuery, IEnumerable<MovieResult>>
{
    private readonly IGetMovieListQueryService _queryService;

    public GetMovieListQueryHandler(IGetMovieListQueryService queryService)
    {
        _queryService = queryService;
    }

    public async Task<IEnumerable<MovieResult>> Handle(
        GetMovieListQuery request,
        CancellationToken cancellationToken)
    {
        IEnumerable<MovieResult> result = await _queryService.ListAsync(request.Genre, cancellationToken);

        return result;
    }
}
