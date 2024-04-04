using FastEndpoints;

namespace Movies.Api.Endpoints.Movies;

public record GetMovieListRequest
{
    [QueryParam]
    public string? Genre { get; init; }
}
