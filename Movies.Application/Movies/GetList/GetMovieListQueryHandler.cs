using Movies.Application.Abstractions;

namespace Movies.Application.Movies.GetList;

internal sealed class GetMovieListQueryHandler
    : IQueryHandler<GetMovieListQuery, IEnumerable<MovieResult>>
{
    private readonly IListMovieQueryService _queryService;

    public GetMovieListQueryHandler(IListMovieQueryService queryService)
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
