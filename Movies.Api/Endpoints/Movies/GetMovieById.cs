using FastEndpoints;
using MediatR;
using Movies.Application.Movies;
using Movies.Application.Movies.GetById;

namespace Movies.Api.Endpoints.Movies;

public class GetMovieById(ISender sender) : Endpoint<GetMovieByIdRequest, MovieResponse>
{
    public const string Route = "movies/{id}";

    private readonly ISender _sender = sender;

    public override void Configure()
    {
        Get(Route);
        AllowAnonymous();

        Summary(s =>
        {
            s.Summary = "Get a movie by id.";
            s.ResponseExamples[StatusCodes.Status200OK] = MovieResponse.Example;

            Description(d => d
                .Produces(StatusCodes.Status404NotFound)
                .ClearDefaultProduces(StatusCodes.Status400BadRequest));
        });
    }

    public override async Task HandleAsync(GetMovieByIdRequest req, CancellationToken ct)
    {
        var query = new GetMovieByIdQuery(req.Id);

        MovieResult result = await _sender.Send(query, ct);

        if (result is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        Response = new MovieResponse(result);
    }
}
