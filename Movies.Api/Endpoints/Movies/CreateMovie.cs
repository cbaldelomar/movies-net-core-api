using ErrorOr;
using FastEndpoints;
using MediatR;
using Movies.Api.Extensions;
using Movies.Application.Movies;
using Movies.Application.Movies.Commands.Create;

namespace Movies.Api.Endpoints.Movies;

public class CreateMovie(ISender sender) : Endpoint<CreateMovieRequest, MovieResponse>
{
    private readonly ISender _sender = sender;

    public override void Configure()
    {
        Post("movies");
        AllowAnonymous();

        Summary(s =>
        {
            s.Summary = "Create a new movie";
            s.ResponseExamples[StatusCodes.Status201Created] = MovieResponse.Example;

            Description(d => d
                .Produces<MovieResponse>(StatusCodes.Status201Created)
                .ProducesProblemFE()
                //.ProducesProblemDetails()
                .ClearDefaultProduces(StatusCodes.Status200OK));
        });
    }

    public override async Task HandleAsync(CreateMovieRequest req, CancellationToken ct)
    {
        var command = new CreateMovieCommand(
            req.Title,
            req.Year,
            req.Director,
            req.Duration,
            req.Poster,
            req.Rate,
            req.Genres);

        ErrorOr<MovieResult> result = await _sender.Send(command, ct);

        if (result.IsError)
        {
            await SendResultAsync(result.ToProblemDetails());
            return;
        }

        var response = new MovieResponse(result.Value);

        await SendCreatedAtAsync(GetMovieById.Route, new { id = result.Value.Id }, response, cancellation: ct);
    }
}
