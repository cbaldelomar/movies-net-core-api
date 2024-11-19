using FastEndpoints;
using MediatR;
using Movies.Application.Movies.Delete;

namespace Movies.Api.Endpoints.Movies;

public class DeleteMovie(ISender sender) : Endpoint<DeleteMovieRequest>
{
    public const string Route = "movies/{id}";

    private readonly ISender _sender = sender;

    public override void Configure()
    {
        Delete(Route);
        AllowAnonymous();

        Summary(s =>
        {
            s.Summary = "Delete a movie.";

            Description(d => d
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .ClearDefaultProduces(StatusCodes.Status200OK));
        });
    }

    public override async Task HandleAsync(DeleteMovieRequest req, CancellationToken ct)
    {
        var command = new DeleteMovieCommand(req.Id);

        bool result = await _sender.Send(command, ct);

        if (!result)
        {
            await SendResultAsync(Results.BadRequest());
            return;
        }

        await SendNoContentAsync(ct);
    }
}
