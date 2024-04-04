using FastEndpoints;
using MediatR;
using Movies.Application.Movies;
using Movies.Application.Movies.GetById;

namespace Movies.Api.Endpoints.Movies;

public class GetMovieById(ISender sender) : Endpoint<GetMovieByIdRequest, MovieResponse>
{
    private readonly ISender _sender = sender;

    public override void Configure()
    {
        Get("movies/{id}");
        AllowAnonymous();

        var responseExample = new MovieResponse
        {
            Id = Guid.Empty,
            Title = "My Movie",
            Year = DateTime.Now.Year,
            Director = "John Doe",
            Duration = 123,
            Poster = "example.com/poster.jpg",
            Rate = 9.5m,
            Genres = new string[] { "Drama", "Action" },
        };

        Summary(s =>
        {
            s.Summary = "Get a movie by id.";
            s.ResponseExamples[200] = responseExample;
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
