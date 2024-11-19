using FastEndpoints;
using MediatR;
using Movies.Application.Movies.Update;

namespace Movies.Api.Endpoints.Movies;

public class UpdateMovie(ISender sender) : Endpoint<UpdateMovieRequest>
{
    public const string Route = "movies/{id}";

    private readonly ISender _sender = sender;

    public override void Configure()
    {
        Patch(Route);
        AllowAnonymous();

        Summary(s =>
        {
            s.Summary = "Update a movie.";
            s.ResponseExamples[StatusCodes.Status204NoContent] = MovieResponse.Example;

            Description(d => d
                .Produces(StatusCodes.Status404NotFound));
        });
    }

    public override async Task HandleAsync(UpdateMovieRequest req, CancellationToken ct)
    {
        var command = new UpdateMovieCommand(
            req.Id,
            req.Title,
            req.Year,
            req.Director,
            req.Duration,
            req.Poster,
            req.Rate,
            req.Genres);

        bool result = await _sender.Send(command, ct);

        if (!result)
        {
            await SendResultAsync(Results.BadRequest());
            return;
        }

        await SendNoContentAsync(ct);
    }
}
