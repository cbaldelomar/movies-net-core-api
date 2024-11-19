using FastEndpoints;
using MediatR;
using Movies.Application.Movies;
using Movies.Application.Movies.Queries.GetList;

namespace Movies.Api.Endpoints.Movies;

public class GetMovieList(ISender sender) : Endpoint<GetMovieListRequest, GetMovieListResponse>
{
    private readonly ISender _sender = sender;

    public override void Configure()
    {
        Get("movies");
        AllowAnonymous();

        Summary(s =>
        {
            s.Summary = "Get movie list.";
            s.ResponseExamples[StatusCodes.Status200OK] =
                new List<MovieResponse> { MovieResponse.Example };

            Description(d => d
                .ClearDefaultProduces(StatusCodes.Status400BadRequest));
        });
    }

    public override async Task HandleAsync(GetMovieListRequest req, CancellationToken ct)
    {
        var query = new GetMovieListQuery(req.Genre);

        IEnumerable<MovieResult> result = await _sender.Send(query, ct);

        var list = result.Select(r => new MovieResponse(r)).ToList();

        //await SendOkAsync(new GetMovieListResponse(list), ct);
        Response = new GetMovieListResponse(list);
    }
}
